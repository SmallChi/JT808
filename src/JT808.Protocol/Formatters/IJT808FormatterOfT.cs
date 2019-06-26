using System;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters
{
    [Obsolete("使用IJT808MessagePackFormatter")]
    public interface IJT808Formatter<T>: IJT808Formatter
    {
        T Deserialize(ReadOnlySpan<byte> bytes, out int readSize, IJT808Config config);

        int Serialize(ref byte[] bytes, int offset, T value, IJT808Config config);
    }

    public interface IJT808MessagePackFormatter<T> : IJT808Formatter
    {
        void Serialize(ref JT808MessagePackWriter writer, T value, IJT808Config config);
        T Deserialize(ref JT808MessagePackReader reader, IJT808Config config);
    }

    public interface IJT808Formatter
    {

    }
}
