using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x01Formatter : IJT808Formatter<JT808_0x0200_0x01>
    {
        public JT808_0x0200_0x01 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0200_0x01 jT808LocationAttachImpl0X01 = new JT808_0x0200_0x01() { };
            jT808LocationAttachImpl0X01.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808LocationAttachImpl0X01.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808LocationAttachImpl0X01.Mileage = JT808BinaryExtensions.ReadInt32Little(bytes, ref offset);
            readSize = offset;
            return jT808LocationAttachImpl0X01;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0200_0x01 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteInt32Little(bytes, offset, value.Mileage);
            return offset;
        }
    }
}
