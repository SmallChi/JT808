using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions
{
    /// <summary>
    /// JT808分析器扩展
    /// </summary>
    public static class JT808AnalyzeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public static void Analyze(this object instance, ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            if(instance is IJT808Analyze analyze)
            {
                analyze.Analyze(ref reader, writer, config);
            }
            else
            {
                throw new NotImplementedException($"Not Implemented {instance.GetType().FullName} {nameof(IJT808Analyze)}");
            }
        }
    }
}
