using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// JT808分析器
    /// </summary>
    public interface IJT808Analyze
    {
        /// <summary>
        /// 分析器
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config);
    }
}
