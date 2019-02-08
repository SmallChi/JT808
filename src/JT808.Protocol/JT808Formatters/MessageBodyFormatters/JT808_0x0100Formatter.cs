using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0100Formatter : IJT808Formatter<JT808_0x0100>
    {
        public JT808_0x0100 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0100 jT808_0X0100 = new JT808_0x0100
            {
                AreaID = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                CityOrCountyId = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                MakerId = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 5),
                TerminalModel = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 20),
                TerminalId = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 7),
                PlateColor = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                PlateNo = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset)
            };
            readSize = offset;
            return jT808_0X0100;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0100 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.AreaID);
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.CityOrCountyId);
            offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.MakerId.PadRight(5, '0'));
            offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.TerminalModel.PadRight(20, '0'));
            offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.TerminalId.PadRight(7, '0'));
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.PlateColor);
            offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.PlateNo);
            return offset;
        }
    }
}
