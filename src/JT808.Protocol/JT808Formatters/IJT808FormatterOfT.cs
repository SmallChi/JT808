using System;
using System.Buffers;

namespace JT808.Protocol.JT808Formatters
{
    public interface IJT808Formatter<T> 
    {
        T Deserialize(ReadOnlySpan<byte> bytes,  out int readSize);

        int Serialize(IMemoryOwner<byte> memoryOwner, int offset, T value);
    }
}
