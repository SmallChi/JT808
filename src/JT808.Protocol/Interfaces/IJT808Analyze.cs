using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.Interfaces
{
    public interface IJT808Analyze
    {
        void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config);
    }
}
