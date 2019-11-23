using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端注册应答
    /// </summary>
    public class JT808_0x8100 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8100>
    {
        public override ushort MsgId { get; } = 0x8100;
        /// <summary>
        /// 应答流水号
        /// 对应的终端注册消息的流水号
        /// </summary>
        public ushort AckMsgNum { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public JT808TerminalRegisterResult JT808TerminalRegisterResult { get; set; }

        /// <summary>
        /// 鉴权码
        /// 只有在成功后才有该字段
        /// </summary>
        public string Code { get; set; }

        public JT808_0x8100 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8100 jT808_0X8100 = new JT808_0x8100();
            jT808_0X8100.AckMsgNum = reader.ReadUInt16();
            jT808_0X8100.JT808TerminalRegisterResult = (JT808TerminalRegisterResult)reader.ReadByte();
            // 只有在成功后才有该字段
            if (jT808_0X8100.JT808TerminalRegisterResult == JT808TerminalRegisterResult.成功)
            {
                jT808_0X8100.Code = reader.ReadRemainStringContent();
            }
            return jT808_0X8100;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8100 value, IJT808Config config)
        {
            writer.WriteUInt16(value.AckMsgNum);
            writer.WriteByte((byte)value.JT808TerminalRegisterResult);
            // 只有在成功后才有该字段
            if (value.JT808TerminalRegisterResult == JT808TerminalRegisterResult.成功)
            {
                writer.WriteString(value.Code);
            }
        }
    }
}
