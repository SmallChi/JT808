using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 事件报告
    /// 0x0301
    /// 2019版本已作删除
    /// </summary>
    public class JT808_0x0301 : JT808MessagePackFormatter<JT808_0x0301>, JT808Bodies, IJT808_2019_Version, IJT808Analyze
    {
        /// <summary>
        /// 0x0301
        /// </summary>
        public ushort MsgId  => 0x0301;
        /// <summary>
        /// 事件报告
        /// </summary>
        public string Description => "事件报告";
        /// <summary>
        /// 事件 ID 
        /// </summary>
        public byte EventId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            var eventId = reader.ReadByte();
            writer.WriteNumber($"[{eventId.ReadNumber()}]事件ID", eventId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0301 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0301 jT808_0X0301 = new JT808_0x0301();
            jT808_0X0301.EventId = reader.ReadByte();
            return jT808_0X0301;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0301 value, IJT808Config config)
        {
            writer.WriteByte(value.EventId);
        }
    }
}
