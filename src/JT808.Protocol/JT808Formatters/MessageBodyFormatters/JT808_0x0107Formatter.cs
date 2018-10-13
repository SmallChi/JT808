using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0107Formatter : IJT808Formatter<JT808_0x0107>
    {
        public JT808_0x0107 Deserialize(ReadOnlySpan<byte> bytes,out int readSize)
        {
            int offset = 0;
            JT808_0x0107 jT808_0X0107 = new JT808_0x0107();
            jT808_0X0107.TerminalType = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X0107.MakerId = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 5);
            jT808_0X0107.TerminalModel = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 20);
            jT808_0X0107.TerminalId = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 7);
            jT808_0X0107.Terminal_SIM_ICCID = JT808BinaryExtensions.ReadBCD(bytes, ref offset, 5).ToString();
            jT808_0X0107.Terminal_Hardware_Version_Length= JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0107.Terminal_Hardware_Version_Num = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0X0107.Terminal_Hardware_Version_Length);
            jT808_0X0107.Terminal_Firmware_Version_Length = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0107.Terminal_Firmware_Version_Num = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0X0107.Terminal_Firmware_Version_Length);
            jT808_0X0107.GNSSModule = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0107.CommunicationModule = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X0107;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x0107 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.TerminalType);
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.MakerId.PadRight(5, '0'));
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.TerminalModel.PadRight(20, '0'));
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.TerminalId.PadRight(7, '0'));
            offset += JT808BinaryExtensions.WriteBCDLittle(memoryOwner, offset, value.Terminal_SIM_ICCID,5,10);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset,(byte)value.Terminal_Hardware_Version_Num.Length);
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.Terminal_Hardware_Version_Num);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, (byte)value.Terminal_Firmware_Version_Num.Length);
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.Terminal_Firmware_Version_Num);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.GNSSModule);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.CommunicationModule);
            return offset;
        }
    }
}
