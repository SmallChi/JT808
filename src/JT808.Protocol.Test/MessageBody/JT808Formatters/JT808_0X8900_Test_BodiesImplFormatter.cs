using JT808.Protocol.Extensions;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.Test.MessageBody.JT808_0X8900_BodiesImpl;
using System;

namespace JT808.Protocol.Test.MessageBody.JT808Formatters
{
    public class JT808_0X8900_Test_BodiesImplFormatter : IJT808Formatter<JT808_0X8900_Test_BodiesImpl>
    {
        public JT808_0X8900_Test_BodiesImpl Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0X8900_Test_BodiesImpl jT808_0X8900_Test_BodiesImpl = new JT808_0X8900_Test_BodiesImpl
            {
                Id = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset),
                Sex = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };

            readSize = offset;
            return jT808_0X8900_Test_BodiesImpl;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0X8900_Test_BodiesImpl value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.Id);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Sex);
            return offset;
        }
    }
}
