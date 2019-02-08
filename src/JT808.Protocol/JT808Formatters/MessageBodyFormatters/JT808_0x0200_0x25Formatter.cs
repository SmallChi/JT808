using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x25Formatter : IJT808Formatter<JT808_0x0200_0x25>
    {
        public JT808_0x0200_0x25 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0200_0x25 jT808LocationAttachImpl0x13 = new JT808_0x0200_0x25
            {
                AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                CarSignalStatus = JT808BinaryExtensions.ReadInt32Little(bytes, ref offset)
            };
            readSize = offset;
            return jT808LocationAttachImpl0x13;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0200_0x25 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteInt32Little(bytes, offset, value.CarSignalStatus);
            return offset;
        }
    }
}
