using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8106Formatter : IJT808Formatter<JT808_0x8106>
    {
        public JT808_0x8106 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8106 jT808_0X8106 = new JT808_0x8106
            {
                ParameterCount = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            jT808_0X8106.Parameters = new uint[jT808_0X8106.ParameterCount];
            for (int i = 0; i < jT808_0X8106.ParameterCount; i++)
            {
                jT808_0X8106.Parameters.SetValue(JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset), i);
            }
            readSize = offset;
            return jT808_0X8106;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8106 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.ParameterCount);
            for (int i = 0; i < value.ParameterCount; i++)
            {
                offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.Parameters[i]);
            }
            return offset;
        }
    }
}
