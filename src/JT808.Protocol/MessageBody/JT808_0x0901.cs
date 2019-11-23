using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据压缩上报
    /// 0x0901
    /// </summary>
    public class JT808_0x0901 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0901>
    {
        public override ushort MsgId { get; } = 0x0901;
        /// <summary>
        /// 未压缩消息长度 
        /// </summary>
        public uint UnCompressMessageLength { get; set; }
        /// <summary>
        /// 未压缩消息体
        /// 压缩消息体为需要压缩的消息经过 GZIP 压缩算法后的消息
        /// 可实现 <see cref="JT808.Protocol.IJT808ICompress"/>自定义压缩算法
        /// </summary>
        public byte[] UnCompressMessage { get; set; }
        public JT808_0x0901 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0901 jT808_0X0901 = new JT808_0x0901();
            var compressMessageLength = reader.ReadUInt32();
            var data = reader.ReadArray((int)compressMessageLength);
            jT808_0X0901.UnCompressMessage = config.Compress.Decompress(data.ToArray());
            jT808_0X0901.UnCompressMessageLength = (uint)jT808_0X0901.UnCompressMessage.Length;
            return jT808_0X0901;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0901 value, IJT808Config config)
        {
            var data = config.Compress.Compress(value.UnCompressMessage);
            writer.WriteUInt32((uint)data.Length);
            writer.WriteArray(data);
        }
    }
}
