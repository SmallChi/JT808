using JT808.Protocol.MessageBody;
using JT808.Protocol.Extensions;
using System;
using System.Buffers;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0100Formatter : IJT808Formatter<JT808_0x0100>
    {
        public JT808_0x0100 Deserialize(ReadOnlySpan<byte> bytes,  out int readSize)
        {
            int offset = 0;
            JT808_0x0100 jT808_0X0100 = new JT808_0x0100();
            jT808_0X0100.AreaID = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            jT808_0X0100.CityOrCountyId = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            jT808_0X0100.MakerId = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset,5);
            jT808_0X0100.TerminalModel = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 20);
            jT808_0X0100.TerminalId = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 7);
            jT808_0X0100.PlateColor = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0100.PlateNo = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X0100;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x0100 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.AreaID);
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.CityOrCountyId);
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.MakerId.PadRight(5, '0'));
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.TerminalModel.PadRight(20,'0'));
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.TerminalId.PadRight(7, '0'));
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.PlateColor);
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.PlateNo);
            return offset;
        }
    }
}
