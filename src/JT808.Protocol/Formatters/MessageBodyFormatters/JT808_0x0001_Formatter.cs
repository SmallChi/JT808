using JT808.Protocol.Enums;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0001_Formatter :IJT808MessagePackFormatter<JT808_0x0001>
    {
        public JT808_0x0001 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0001 jT808_0X0001 = new JT808_0x0001();
            jT808_0X0001.MsgNum = reader.ReadUInt16();
            jT808_0X0001.MsgId = reader.ReadUInt16();
            jT808_0X0001.JT808TerminalResult = (JT808TerminalResult)reader.ReadByte();
            return jT808_0X0001;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0001 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteUInt16(value.MsgId);
            writer.WriteByte((byte)value.JT808TerminalResult);
        }
    }
}
