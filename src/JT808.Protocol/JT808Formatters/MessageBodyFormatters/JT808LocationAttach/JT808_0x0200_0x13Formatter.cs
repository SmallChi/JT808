using JT808.Protocol.Enums;
using JT808.Protocol.MessageBody.JT808LocationAttach;
using JT808.Protocol.Extensions;
using System;
using System.Buffers;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x13Formatter : IJT808Formatter<JT808LocationAttachImpl0x13>
    {
        public JT808LocationAttachImpl0x13 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808LocationAttachImpl0x13 jT808LocationAttachImpl0x13 = new JT808LocationAttachImpl0x13();
            jT808LocationAttachImpl0x13.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x13.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x13.DrivenRouteId =JT808BinaryExtensions.ReadInt32Little(bytes,ref offset);
            jT808LocationAttachImpl0x13.Time = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            jT808LocationAttachImpl0x13.DrivenRoute =(JT808DrivenRouteType)JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            readSize = offset;
            return jT808LocationAttachImpl0x13;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808LocationAttachImpl0x13 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteInt32Little(memoryOwner, offset, value.DrivenRouteId);
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.Time);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, (byte)value.DrivenRoute);
            return offset;
        }
    }
}
