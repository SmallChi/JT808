using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据下行透传
    /// </summary>
    public class JT808_0x8900 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8900>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8900;
        /// <summary>
        /// 透传消息类型
        /// 透传消息类型定义见 表 93
        /// </summary>
        public byte PassthroughType { get; set; }

        /// <summary>
        /// 数据下行透传数据
        /// </summary>
        public byte[] PassthroughData { get; set; }

        /// <summary>
        /// 透传消息内容
        /// </summary>
        public JT808_0x8900_BodyBase JT808_0X8900_BodyBase { get; set; }

        public JT808_0x8900 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8900 jT808_0X8900 = new JT808_0x8900();
            jT808_0X8900.PassthroughType = reader.ReadByte();
            jT808_0X8900.PassthroughData = reader.ReadContent().ToArray();
            return jT808_0X8900;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8900 value, IJT808Config config)
        {
            writer.WriteByte(value.PassthroughType);
            JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(value.JT808_0X8900_BodyBase, ref writer, value.JT808_0X8900_BodyBase, config);
        }
    }
}
