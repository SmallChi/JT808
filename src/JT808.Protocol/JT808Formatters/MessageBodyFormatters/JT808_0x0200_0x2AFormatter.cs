using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x2AFormatter : IJT808Formatter<JT808_0x0200_0x2A>
    {
        public JT808_0x0200_0x2A Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0200_0x2A jT808LocationAttachImpl0X2A = new JT808_0x0200_0x2A
            {
                AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                IOStatus = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset)
            };
            readSize = offset;
            return jT808LocationAttachImpl0X2A;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0200_0x2A value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.IOStatus);
            return offset;
        }
    }
}
