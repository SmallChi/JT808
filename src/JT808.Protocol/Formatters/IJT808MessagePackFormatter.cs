using System;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters
{
    public interface IJT808MessagePackFormatter<T> : IJT808Formatter
    {
        void Serialize(ref JT808MessagePackWriter writer, T value, IJT808Config config);
        T Deserialize(ref JT808MessagePackReader reader, IJT808Config config);
    }

    public interface IJT808Formatter
    {

    }
}
