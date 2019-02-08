using JT808.Protocol.Extensions;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.Test.MessageBody.JT808_0x0701BodiesImpl;
using System;

namespace JT808.Protocol.Test.MessageBody.JT808Formatters
{
    public class JT808_0x0701TestBodiesImplFormatter : IJT808Formatter<JT808_0x0701TestBodiesImpl>
    {
        public JT808_0x0701TestBodiesImpl Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0701TestBodiesImpl jT808_0X0701TestBodiesImpl = new JT808_0x0701TestBodiesImpl
            {
                Id = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset),
                UserNameLength = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset)
            };
            jT808_0X0701TestBodiesImpl.UserName = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0X0701TestBodiesImpl.UserNameLength);
            readSize = offset;
            return jT808_0X0701TestBodiesImpl;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0701TestBodiesImpl value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.Id);
            // 先计算内容长度（汉字为两个字节）
            offset += 2;
            int byteLength = JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.UserName);
            JT808BinaryExtensions.WriteUInt16Little(bytes, offset - 2, (ushort)byteLength);
            offset += byteLength;
            return offset;
        }
    }
}
