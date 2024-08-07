using System.Collections.Concurrent;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Internal
{
    /// <summary>
    /// 默认分包合并实现
    /// </summary>
    public class DefaultMerger : IMerger, IDisposable
    {
        /// <summary>
        /// 分包数据缓存
        /// <para>key为sim卡号，value为字典，key为消息id,value为元组，结构为：(分包索引,分包元数据)</para>
        /// </summary>
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<ushort, List<(ushort index, byte[] data)>>> splitPackageDictionary = new();

        private readonly ConcurrentDictionary<string, DateTime> timeoutDictionary = new ConcurrentDictionary<string, DateTime>();
        private readonly TimeSpan cleanInterval = TimeSpan.FromSeconds(60);
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        private bool disposed;
        public DefaultMerger()
        {
            Task.Run(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    timeoutDictionary.ToList().ForEach(x =>
                    {
                        var key = x.Key;
                        var datetime = x.Value;
                        if (datetime < DateTime.Now && TryParseKey(key, out var phoneNumber, out var messageId) && splitPackageDictionary.TryGetValue(phoneNumber, out var value) && value.TryRemove(messageId, out var caches) && value.Count == 0 && splitPackageDictionary.TryRemove(phoneNumber, out _))
                        {
                            timeoutDictionary.TryRemove(key, out _);
                        }
                    });
                    await Task.Delay(cleanInterval);
                }
            }, cts.Token);
        }
        /// <inheritdoc/>
        public bool TryMerge(JT808Header header, byte[] data, IJT808Config config, out JT808Bodies body)
        {
            body = null;
            var timeoutKey = GenerateKey(header.TerminalPhoneNo, header.MsgId);
            if (!CheckTimeout(timeoutKey)) return false;
            var timeout = DateTime.Now.AddSeconds(config.AutoMergeTimeoutSecond);
            if (timeoutDictionary.TryAdd(timeoutKey, timeout))
                timeoutDictionary.TryUpdate(timeoutKey, timeout, timeout);
            if (splitPackageDictionary.TryGetValue(header.TerminalPhoneNo, out var item) && item.TryGetValue(header.MsgId, out var packages))
            {
                packages.Add((header.PackageIndex, data));
                if (packages.Count != header.PackgeCount)
                {
                    return false;
                }
                item.TryRemove(header.MsgId, out _);
                splitPackageDictionary.TryRemove(header.TerminalPhoneNo, out _);

                var mateData = packages.OrderBy(x => x.index).SelectMany(x => x.data).Concat(data).ToArray();

                byte[] buffer = JT808ArrayPool.Rent(mateData.Length);
                try
                {
                    var reader = new JT808MessagePackReader(mateData, (Enums.JT808Version)header.ProtocolVersion);
                    if (config.MsgIdFactory.TryGetValue(header.MsgId, out var value) && value is JT808Bodies instance)
                    {
                        body = instance.DeserializeExt<JT808Bodies>(ref reader, config);
                        header.MessageBodyProperty.IsMerged = true;
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
                splitPackageDictionary.AddOrUpdate(header.TerminalPhoneNo, new ConcurrentDictionary<ushort, List<(ushort, byte[])>>() { [header.MsgId] = new List<(ushort, byte[])> { (header.PackageIndex, data) } }, (_, value) =>
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
        private bool CheckTimeout(string key) => !timeoutDictionary.TryGetValue(key, out var dateTime) || dateTime >= DateTime.Now;

        private const char keyJoiner = '-';
        private const string keyJoinerNET7 = "-";

        private string GenerateKey(string phoneNumber, ushort messageId) => string.Join(keyJoinerNET7, new[] { phoneNumber, messageId.ToString() });

        private bool TryParseKey(string key, out string phoneNumber, out ushort messageId)
        {
            phoneNumber = null;
            messageId = 0;
            var tmp = key.Split(keyJoiner);
            if (tmp.Length == 2 && ushort.TryParse(tmp[1], out messageId))
            {
                phoneNumber = tmp[0];
                return true;
            }
            return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    cts.Cancel();
                    cts.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DefaultMerger()
        {
            Dispose(false);
        }
    }
}