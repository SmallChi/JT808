using JT808.Protocol.Enums;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Formatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端通用应答
    /// </summary>
    public class JT808_0x0001 : JT808Bodies,IJT808MessagePackFormatter<JT808_0x0001>
    {
        public override ushort MsgId { get; } = 0x0001;
        /// <summary>
        /// 应答流水号
        /// 对应的平台消息的流水号
        /// </summary>
        public ushort ReplyMsgNum { get; set; }
        /// <summary>
        /// 应答 ID
        /// 对应的平台消息的 ID
        /// <see cref="JT808.Protocol.Enums.JT808MsgId"/>
        /// </summary>
        public ushort ReplyMsgId { get; set; }

        /// <summary>
        /// 结果
        /// 0：成功/确认；1：失败；2：消息有误；3：不支持
        /// </summary>
        public JT808TerminalResult JT808TerminalResult { get; set; }

        public JT808_0x0001 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0001 jT808_0X0001 = new JT808_0x0001();
            jT808_0X0001.ReplyMsgNum = reader.ReadUInt16();
            jT808_0X0001.ReplyMsgId = reader.ReadUInt16();
            jT808_0X0001.JT808TerminalResult = (JT808TerminalResult)reader.ReadByte();
            return jT808_0X0001;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0001 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ReplyMsgNum);
            writer.WriteUInt16(value.ReplyMsgId);
            writer.WriteByte((byte)value.JT808TerminalResult);
        }
    }
}
