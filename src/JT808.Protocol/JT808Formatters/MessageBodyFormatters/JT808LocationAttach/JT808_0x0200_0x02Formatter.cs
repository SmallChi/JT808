using JT808.Protocol.MessageBody.JT808LocationAttach;
using JT808.Protocol.Extensions;
using System;
using System.Buffers;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x02Formatter : IJT808Formatter<JT808LocationAttachImpl0x02>
    {
        public JT808LocationAttachImpl0x02 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808LocationAttachImpl0x02 jT808LocationAttachImpl0X02 = new JT808LocationAttachImpl0x02();
            jT808LocationAttachImpl0X02.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0X02.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0X02.Oil = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            readSize = offset;
            return jT808LocationAttachImpl0X02;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x02 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.Oil);
            return offset;
        }
    }
}
