using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8003Formatter : IJT808Formatter<JT808_0x8003>
    {
        public JT808_0x8003 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8003 jT808_0X8003 = new JT808_0x8003();
            jT808_0X8003.OriginalMsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X8003.AgainPackageCount = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8003.AgainPackageData = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, jT808_0X8003.AgainPackageCount*2);
            readSize = offset;
            return jT808_0X8003;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8003 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.OriginalMsgNum);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.AgainPackageCount);
            offset += JT808BinaryExtensions.WriteBytesLittle(memoryOwner, offset, value.AgainPackageData);
            return offset;
        }
    }
}
