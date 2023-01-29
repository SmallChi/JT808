using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 终端上传乘客流量
    /// </summary>
    public class JT808_0x1005 : JT808MessagePackFormatter<JT808_0x1005>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public string Description => "终端上传乘客流量";
        /// <summary>
        /// 
        /// </summary>
        public ushort MsgId => 0x1005;
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 上车人数
        /// </summary>
        public ushort GettingOnNumber { get; set; }
        /// <summary>
        /// 下车人数
        /// </summary>
        public ushort GettingOffNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x1005 value = new JT808_0x1005();
            value.BeginTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.BeginTime.ToString("yyMMddHHmmss")}]开始时间", value.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.EndTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.EndTime.ToString("yyMMddHHmmss")}]结束时间", value.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.GettingOnNumber = reader.ReadUInt16();
            writer.WriteNumber($"[{value.GettingOnNumber.ReadNumber()}]从开始时间到结束时间的上车人数",value.GettingOnNumber);
            value.GettingOffNumber = reader.ReadUInt16();
            writer.WriteNumber($"[{value.GettingOffNumber.ReadNumber()}]从开始时间到结束时间的下车人数", value.GettingOffNumber);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x1005 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x1005 jT808_0x1005 = new JT808_0x1005();
            jT808_0x1005.BeginTime = reader.ReadDateTime_yyMMddHHmmss();
            jT808_0x1005.EndTime = reader.ReadDateTime_yyMMddHHmmss();
            jT808_0x1005.GettingOnNumber = reader.ReadUInt16();
            jT808_0x1005.GettingOffNumber = reader.ReadUInt16();
            return jT808_0x1005;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x1005 value, IJT808Config config)
        {
            writer.WriteDateTime_yyMMddHHmmss(value.BeginTime);
            writer.WriteDateTime_yyMMddHHmmss(value.EndTime);
            writer.WriteUInt16(value.GettingOnNumber);
            writer.WriteUInt16(value.GettingOffNumber);
        }
    }
}
