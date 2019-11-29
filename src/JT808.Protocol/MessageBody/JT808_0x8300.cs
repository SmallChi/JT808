using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 文本信息下发
    /// </summary>
    public class JT808_0x8300 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8300>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8300;
        /// <summary>
        /// 文本信息标志位含义见 表 38
        /// </summary>
        public byte TextFlag { get; set; }
        /// <summary>
        /// 文本类型
        /// 1=通知,2=服务
        /// 2019版本
        /// </summary>
        public byte TextType { get; set; }
        /// <summary>
        /// 文本信息
        /// 最长为 1024 字节，经GBK编码
        /// </summary>
        public string TextInfo { get; set; }

        public JT808_0x8300 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8300 jT808_0X8300 = new JT808_0x8300();
            jT808_0X8300.TextFlag = reader.ReadByte();
            if(reader.Version== JT808Version.JTT2019)
            {
                jT808_0X8300.TextType = reader.ReadByte();
            }
            jT808_0X8300.TextInfo = reader.ReadRemainStringContent();
            return jT808_0X8300;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8300 value, IJT808Config config)
        {
            writer.WriteByte(value.TextFlag);
            if (writer.Version == JT808Version.JTT2019)
            {
                writer.WriteByte(value.TextType);
            }
            writer.WriteString(value.TextInfo);
        }
    }
}
