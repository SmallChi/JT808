using JT808.Protocol.Enums;
using JT808.Protocol.MessageBody.JT808LocationAttach;
using JT808.Protocol.Extensions;
using System;
using System.Buffers;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x12Formatter : IJT808Formatter<JT808LocationAttachImpl0x12>
    {
        public JT808LocationAttachImpl0x12 Deserialize(ReadOnlySpan<byte> bytes,out int readSize)
        {
            int offset = 0;
            JT808LocationAttachImpl0x12 jT808LocationAttachImpl0x12 = new JT808LocationAttachImpl0x12();
            jT808LocationAttachImpl0x12.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x12.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x12.JT808PositionType =(JT808PositionType)JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x12.AreaId = JT808BinaryExtensions.ReadInt32Little(bytes,ref offset);
            jT808LocationAttachImpl0x12.Direction =(JT808DirectionType)JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            readSize = offset;
            return jT808LocationAttachImpl0x12;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808LocationAttachImpl0x12 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, (byte)value.JT808PositionType);
            offset += JT808BinaryExtensions.WriteInt32Little(memoryOwner, offset, value.AreaId);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, (byte)value.Direction);
            return offset;
        }
    }
}
