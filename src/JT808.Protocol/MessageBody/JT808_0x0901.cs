
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据压缩上报
    /// 0x0901
    /// </summary>
    public class JT808_0x0901 : JT808MessagePackFormatter<JT808_0x0901>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x0901
        /// </summary>
        public ushort MsgId  => 0x0901;
        /// <summary>
        /// 数据压缩上报
        /// </summary>
        public string Description => "数据压缩上报";
        /// <summary>
        /// 未压缩消息长度 
        /// </summary>
        public uint UnCompressMessageLength { get; set; }
        /// <summary>
        /// 未压缩消息体
        /// 压缩消息体为需要压缩的消息经过 GZIP 压缩算法后的消息
        /// 可实现 refJT808.Protocol.IJT808ICompress 自定义压缩算法
        /// </summary>
        public byte[] UnCompressMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0901 value = new JT808_0x0901();
            var compressMessageLength = reader.ReadUInt32();
            writer.WriteNumber($"[{compressMessageLength.ReadNumber()}]压缩消息长度", compressMessageLength);
            var data = reader.ReadArray((int)compressMessageLength);
            writer.WriteString("压缩消息体", data.ToArray().ToHexString());
            value.UnCompressMessage = config.Compress.Decompress(data.ToArray());
            value.UnCompressMessageLength = (uint)value.UnCompressMessage.Length;
            writer.WriteNumber($"[{value.UnCompressMessageLength.ReadNumber()}]未压缩消息长度", value.UnCompressMessageLength);
            writer.WriteString("未压缩消息体", value.UnCompressMessage.ToHexString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0901 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0901 value = new JT808_0x0901();
            var compressMessageLength = reader.ReadUInt32();
            var data = reader.ReadArray((int)compressMessageLength);
            value.UnCompressMessage = config.Compress.Decompress(data.ToArray());
            value.UnCompressMessageLength = (uint)value.UnCompressMessage.Length;
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0901 value, IJT808Config config)
        {
            var data = config.Compress.Compress(value.UnCompressMessage);
            writer.WriteUInt32((uint)data.Length);
            writer.WriteArray(data);
        }
    }
}
