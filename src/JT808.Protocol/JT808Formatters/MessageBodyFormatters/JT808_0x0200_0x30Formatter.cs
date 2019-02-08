using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x30Formatter : IJT808Formatter<JT808_0x0200_0x30>
    {
        public JT808_0x0200_0x30 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0200_0x30 jT808LocationAttachImpl0x30 = new JT808_0x0200_0x30
            {
                AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                WiFiSignalStrength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            readSize = offset;
            return jT808LocationAttachImpl0x30;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0200_0x30 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.WiFiSignalStrength);
            return offset;
        }
    }
}
