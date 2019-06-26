using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8100_Formatter : IJT808MessagePackFormatter<JT808_0x8100>
    {
        public JT808_0x8100 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8100 jT808_0X8100 = new JT808_0x8100();
            jT808_0X8100.MsgNum = reader.ReadUInt16();
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
            writer.WriteUInt16(value.MsgNum);
            writer.WriteByte((byte)value.JT808TerminalRegisterResult);
            // 只有在成功后才有该字段
            if (value.JT808TerminalRegisterResult == JT808TerminalRegisterResult.成功)
            {
                writer.WriteString(value.Code);
            }
        }
    }
}
