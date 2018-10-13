using JT808.Protocol.MessageBody.JT808LocationAttach;
using JT808.Protocol.Extensions;
using System;
using System.Buffers;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x2BFormatter : IJT808Formatter<JT808LocationAttachImpl0x2B>
    {
        public JT808LocationAttachImpl0x2B Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808LocationAttachImpl0x2B jT808LocationAttachImpl0x2B = new JT808LocationAttachImpl0x2B();
            jT808LocationAttachImpl0x2B.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x2B.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x2B.Analog = JT808BinaryExtensions.ReadInt32Little(bytes,ref offset);
            readSize = offset;
            return jT808LocationAttachImpl0x2B;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808LocationAttachImpl0x2B value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteInt32Little(memoryOwner, offset, value.Analog);
            return offset;
        }
    }
}
