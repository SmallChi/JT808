using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 电话回拨
    /// </summary>
    public class JT808_0x8400 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8400>
    {
        public override ushort MsgId { get; } = 0x8400;
        /// <summary>
        /// 0:普通通话；1:监听
        /// </summary>
        public JT808CallBackType CallBack { get; set; }
        /// <summary>
        /// 电话号码 
        /// 最长为 20 字节
        /// </summary>
        public string PhoneNumber { get; set; }

        public JT808_0x8400 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8400 jT808_0X8400 = new JT808_0x8400();
            jT808_0X8400.CallBack = (JT808CallBackType)reader.ReadByte();
            // 最长为 20 字节
            jT808_0X8400.PhoneNumber = reader.ReadRemainStringContent();
            return jT808_0X8400;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8400 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.CallBack);
            writer.WriteString(value.PhoneNumber);
        }
    }
}
