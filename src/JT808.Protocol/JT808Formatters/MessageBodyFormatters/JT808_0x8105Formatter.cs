using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8105Formatter : IJT808Formatter<JT808_0x8105>
    {
        public JT808_0x8105 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8105 jT808_0x8105 = new JT808_0x8105
            {
                CommandWord = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            if (jT808_0x8105.CommandWord == 1 || jT808_0x8105.CommandWord == 2)
            {
                jT808_0x8105.CommandValue = new CommandParams();
                jT808_0x8105.CommandValue.SetCommandParams(JT808BinaryExtensions.ReadStringLittle(bytes, ref offset));
            }
            readSize = offset;
            return jT808_0x8105;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8105 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.CommandWord);
            if (value.CommandWord == 1 || value.CommandWord == 2)
            {
                offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.CommandValue.ToString());
            }
            return offset;
        }
    }
}
