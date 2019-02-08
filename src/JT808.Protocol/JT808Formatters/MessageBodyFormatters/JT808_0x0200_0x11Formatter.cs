using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x11Formatter : IJT808Formatter<JT808_0x0200_0x11>
    {
        public JT808_0x0200_0x11 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0200_0x11 jT808LocationAttachImpl0x11 = new JT808_0x0200_0x11
            {
                AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                JT808PositionType = (JT808PositionType)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            if (jT808LocationAttachImpl0x11.JT808PositionType != JT808PositionType.无特定位置)
            {
                jT808LocationAttachImpl0x11.AreaId = JT808BinaryExtensions.ReadInt32Little(bytes, ref offset);
            }
            readSize = offset;
            return jT808LocationAttachImpl0x11;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0200_0x11 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.JT808PositionType);
            if (value.JT808PositionType != JT808PositionType.无特定位置)
            {
                offset += JT808BinaryExtensions.WriteInt32Little(bytes, offset, value.AreaId);
            }
            return offset;
        }
    }
}
