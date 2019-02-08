using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8003Formatter : IJT808Formatter<JT808_0x8003>
    {
        public JT808_0x8003 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8003 jT808_0X8003 = new JT808_0x8003
            {
                OriginalMsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                AgainPackageCount = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            jT808_0X8003.AgainPackageData = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, jT808_0X8003.AgainPackageCount * 2);
            readSize = offset;
            return jT808_0X8003;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8003 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.OriginalMsgNum);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)(value.AgainPackageData.Length / 2));
            offset += JT808BinaryExtensions.WriteBytesLittle(bytes, offset, value.AgainPackageData);
            return offset;
        }
    }
}
