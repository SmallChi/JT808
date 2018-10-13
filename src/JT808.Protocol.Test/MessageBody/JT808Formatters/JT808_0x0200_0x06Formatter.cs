using JT808.Protocol.MessageBody.JT808LocationAttach;
using JT808.Protocol.Test.JT808LocationAttach;
using JT808.Protocol.Extensions;
using System;
using JT808.Protocol.JT808Formatters;
using System.Buffers;

namespace JT808.Protocol.Test.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x06Formatter : IJT808Formatter<JT808LocationAttachImpl0x06>
    {
        public JT808LocationAttachImpl0x06 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808LocationAttachImpl0x06 jT808LocationAttachImpl0x06 = new JT808LocationAttachImpl0x06() { };
            jT808LocationAttachImpl0x06.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x06.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x06.Age= JT808BinaryExtensions.ReadInt32Little(bytes,ref offset);
            jT808LocationAttachImpl0x06.Gender = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x06.UserName = JT808BinaryExtensions.ReadStringLittle(bytes,ref offset);
            readSize = offset;
            return jT808LocationAttachImpl0x06;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808LocationAttachImpl0x06 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteInt32Little(memoryOwner, offset, value.Age);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.Gender);
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.UserName);
            return offset;
        }
    }
}
