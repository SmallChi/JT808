using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询服务器时间应答
    /// 2019版本
    /// </summary>
    public class JT808_0x8004 : JT808MessagePackFormatter<JT808_0x8004>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8004
        /// </summary>
        public ushort MsgId  => 0x8004;
        /// <summary>
        /// 查询服务器时间应答
        /// </summary>
        public string Description => "查询服务器时间应答";
        /// <summary>
        /// 服务器时间
        /// </summary>
        public DateTime Time { get; set; } = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            var datetime= reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{ datetime.ToString("yyMMddHHmmss")}]查询服务器时间应答", datetime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8004 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8004 value = new JT808_0x8004();
            value.Time = reader.ReadDateTime_yyMMddHHmmss();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8004 value, IJT808Config config)
        {
            writer.WriteDateTime_yyMMddHHmmss(value.Time);
        }
    }
}
