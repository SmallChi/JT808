using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0100_Formatter : IJT808MessagePackFormatter<JT808_0x0100>
    {
        public JT808_0x0100 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0100 jT808_0X0100 = new JT808_0x0100();
            jT808_0X0100.AreaID = reader.ReadUInt16();
            jT808_0X0100.CityOrCountyId = reader.ReadUInt16();
            jT808_0X0100.MakerId = reader.ReadString(5);
            jT808_0X0100.TerminalModel = reader.ReadString(20);
            jT808_0X0100.TerminalId = reader.ReadString(7);
            jT808_0X0100.PlateColor = reader.ReadByte();
            jT808_0X0100.PlateNo = reader.ReadRemainStringContent();
            return jT808_0X0100;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0100 value, IJT808Config config)
        {
            writer.WriteUInt16(value.AreaID);
            writer.WriteUInt16(value.CityOrCountyId);
            writer.WriteString(value.MakerId.PadRight(5, '0'));
            writer.WriteString(value.TerminalModel.PadRight(20, '0'));
            writer.WriteString(value.TerminalId.PadRight(7, '0'));
            writer.WriteByte(value.PlateColor);
            writer.WriteString(value.PlateNo);
        }
    }
}
