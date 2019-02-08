using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x13Formatter : IJT808Formatter<JT808_0x0200_0x13>
    {
        public JT808_0x0200_0x13 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0200_0x13 jT808LocationAttachImpl0x13 = new JT808_0x0200_0x13
            {
                AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                DrivenRouteId = JT808BinaryExtensions.ReadInt32Little(bytes, ref offset),
                Time = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                DrivenRoute = (JT808DrivenRouteType)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            readSize = offset;
            return jT808LocationAttachImpl0x13;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0200_0x13 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteInt32Little(bytes, offset, value.DrivenRouteId);
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.Time);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.DrivenRoute);
            return offset;
        }
    }
}
