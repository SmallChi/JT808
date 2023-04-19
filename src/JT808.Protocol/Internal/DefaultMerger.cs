using System.Collections.Concurrent;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Internal
{
    /// <summary>
    /// 默认分包合并实现
    /// </summary>
    public class DefaultMerger : IMerger
    {
        /// <summary>
        /// 分包数据缓存
        /// <para>key为sim卡号，value为字典，key为消息id,value为元组，结构为：(分包索引,分包元数据)</para>
        /// </summary>
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<ushort, List<(ushort index, byte[] data)>>> SplitPackages = new();

        /// <inheritdoc/>
        public bool TryMerge(JT808Header header, byte[] data, IJT808Config config, out JT808Bodies body)
        {
            // TODO: 添加SplitPackages缓存超时，达到阈值时移除该项缓存
            body = null;

            if (SplitPackages.TryGetValue(header.TerminalPhoneNo, out var item) && item.TryGetValue(header.MsgId, out var packages))
            {
                packages.Add((header.PackageIndex, data));
                if (packages.Count != header.PackgeCount)
                {
                    return false;
                }
                item.TryRemove(header.MsgId, out _);
                SplitPackages.TryRemove(header.TerminalPhoneNo, out _);

                var mateData = packages.OrderBy(x => x.index).SelectMany(x => x.data).Concat(data).ToArray();

                byte[] buffer = JT808ArrayPool.Rent(mateData.Length);
                try
                {
                    var reader = new JT808MessagePackReader(mateData, (Enums.JT808Version)header.ProtocolVersion);
                    if (config.MsgIdFactory.TryGetValue(header.MsgId, out var value) && value is JT808Bodies instance)
                    {
                        body = instance.DeserializeExt<JT808Bodies>(ref reader, config);
                        return true;
                    }
                }
                finally
                {
                    JT808ArrayPool.Return(buffer);
                }
            }
            else
            {
                SplitPackages.AddOrUpdate(header.TerminalPhoneNo, new ConcurrentDictionary<ushort, List<(ushort, byte[])>>() { [header.MsgId] = new List<(ushort, byte[])> { (header.PackageIndex, data) } }, (_, value) =>
                {
                    value.AddOrUpdate(header.MsgId, new List<(ushort, byte[])> { (header.PackageIndex, data) }, (_, item) =>
                    {
                        item.Add((header.PackageIndex, data));
                        return item;
                    });
                    return value;
                });
            }
            return false;
        }
    }
}