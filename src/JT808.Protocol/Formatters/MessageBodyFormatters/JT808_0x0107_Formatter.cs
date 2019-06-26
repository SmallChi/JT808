using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0107_Formatter : IJT808MessagePackFormatter<JT808_0x0107>
    {
        public JT808_0x0107 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0107 jT808_0X0107 = new JT808_0x0107();
            jT808_0X0107.TerminalType = reader.ReadUInt16();
            jT808_0X0107.MakerId = reader.ReadString(5);
            jT808_0X0107.TerminalModel = reader.ReadString(20);
            jT808_0X0107.TerminalId = reader.ReadString(7);
            jT808_0X0107.Terminal_SIM_ICCID = reader.ReadBCD(10);
            jT808_0X0107.Terminal_Hardware_Version_Length = reader.ReadByte();
            jT808_0X0107.Terminal_Hardware_Version_Num = reader.ReadString(jT808_0X0107.Terminal_Hardware_Version_Length);
            jT808_0X0107.Terminal_Firmware_Version_Length = reader.ReadByte();
            jT808_0X0107.Terminal_Firmware_Version_Num = reader.ReadString(jT808_0X0107.Terminal_Firmware_Version_Length);
            jT808_0X0107.GNSSModule = reader.ReadByte();
            jT808_0X0107.CommunicationModule = reader.ReadByte();
            return jT808_0X0107;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0107 value, IJT808Config config)
        {
            writer.WriteUInt16(value.TerminalType);
            writer.WriteString(value.MakerId.PadRight(5, '0'));
            writer.WriteString(value.TerminalModel.PadRight(20, '0'));
            writer.WriteString(value.TerminalId.PadRight(7, '0'));
            writer.WriteBCD(value.Terminal_SIM_ICCID, 10);
            writer.WriteByte((byte)value.Terminal_Hardware_Version_Num.Length);
            writer.WriteString(value.Terminal_Hardware_Version_Num);
            writer.WriteByte((byte)value.Terminal_Firmware_Version_Num.Length);
            writer.WriteString(value.Terminal_Firmware_Version_Num);
            writer.WriteByte(value.GNSSModule);
            writer.WriteByte(value.CommunicationModule);
        }
    }
}
