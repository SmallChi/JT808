using JT808.Protocol.MessageBody.JT808LocationAttach;
using JT808.Protocol.Extensions;
using System;
using System.Buffers;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x04Formatter : IJT808Formatter<JT808LocationAttachImpl0x04>
    {
        public JT808LocationAttachImpl0x04 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808LocationAttachImpl0x04 jT808LocationAttachImpl0x04 = new JT808LocationAttachImpl0x04();
            jT808LocationAttachImpl0x04.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x04.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x04.EventId = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            readSize = offset;
            return jT808LocationAttachImpl0x04;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808LocationAttachImpl0x04 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.EventId);
            return offset;
        }
    }
}
