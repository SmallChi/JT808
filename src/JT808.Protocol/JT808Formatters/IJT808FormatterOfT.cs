using System;

namespace JT808.Protocol.JT808Formatters
{
    public interface IJT808Formatter<T>
    {
        T Deserialize(ReadOnlySpan<byte> bytes, out int readSize);

        int Serialize(ref byte[] bytes, int offset, T value);
    }
}
