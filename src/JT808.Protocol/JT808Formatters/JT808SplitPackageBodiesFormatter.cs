using JT808.Protocol.Extensions;
using System;

namespace JT808.Protocol.JT808Formatters
{
    public class JT808SplitPackageBodiesFormatter : IJT808Formatter<JT808SplitPackageBodies>
    {
        public JT808SplitPackageBodies Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            JT808SplitPackageBodies jT808SplitPackageBodies = new JT808SplitPackageBodies
            {
                Data = bytes.ToArray()
            };
            readSize = bytes.Length;
            return jT808SplitPackageBodies;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808SplitPackageBodies value)
        {
            offset += JT808BinaryExtensions.WriteBytesLittle(bytes, offset, value.Data);
            return offset;
        }
    }
}
