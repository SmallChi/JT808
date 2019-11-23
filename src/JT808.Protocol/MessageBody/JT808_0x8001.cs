using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 平台通用应答
    /// </summary>
    public class JT808_0x8001 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8001>
    {
        public override ushort MsgId { get; } = 0x8001;
        public ushort MsgNum { get; set; }
        /// <summary>
        /// <see cref="JT808.Protocol.Enums.JT808MsgId"/>
        /// </summary>
        public ushort AckMsgId { get; set; }
        public JT808PlatformResult JT808PlatformResult { get; set; }
        public JT808_0x8001 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8001 jT808_0X8001 = new JT808_0x8001();
            jT808_0X8001.MsgNum = reader.ReadUInt16();
            jT808_0X8001.AckMsgId = reader.ReadUInt16();
            jT808_0X8001.JT808PlatformResult = (JT808PlatformResult)reader.ReadByte();
            return jT808_0X8001;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8001 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteUInt16(value.AckMsgId);
            writer.WriteByte((byte)value.JT808PlatformResult);
        }
    }
}
