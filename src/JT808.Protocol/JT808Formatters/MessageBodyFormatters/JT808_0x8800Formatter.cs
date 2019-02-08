using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8800Formatter : IJT808Formatter<JT808_0x8800>
    {
        public JT808_0x8800 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8800 jT808_0X8800 = new JT808_0x8800
            {
                MultimediaId = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset),
                RetransmitPackageCount = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            jT808_0X8800.RetransmitPackageIds = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, jT808_0X8800.RetransmitPackageCount * 2);
            readSize = offset;
            return jT808_0X8800;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8800 value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.MultimediaId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)(value.RetransmitPackageIds.Length / 2));
            offset += JT808BinaryExtensions.WriteBytesLittle(bytes, offset, value.RetransmitPackageIds);
            return offset;
        }
    }
}
