using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据上行透传
    /// </summary>
    public class JT808_0x0900 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0900>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x0900;
        /// <summary>
        /// 透传消息类型
        /// </summary>
        public byte PassthroughType { get; set; }

        /// <summary>
        /// 透传数据
        /// </summary>
        public byte[] PassthroughData { get; set; }

        /// <summary>
        /// 透传消息内容
        /// </summary>
        public JT808_0x0900_BodyBase JT808_0x0900_BodyBase { get; set; }

        public JT808_0x0900 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0900 jT808_0X0900 = new JT808_0x0900();
            jT808_0X0900.PassthroughType = reader.ReadByte();
            jT808_0X0900.PassthroughData = reader.ReadContent().ToArray(); ;
            return jT808_0X0900;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0900 value, IJT808Config config)
        {
            writer.WriteByte(value.PassthroughType);
            object obj = config.GetMessagePackFormatterByType(value.JT808_0x0900_BodyBase.GetType());
            JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(obj, ref writer, value.JT808_0x0900_BodyBase, config);
        }
    }
}
