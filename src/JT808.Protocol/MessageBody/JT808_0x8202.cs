using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 临时位置跟踪控制
    /// </summary>
    public class JT808_0x8202 : JT808MessagePackFormatter<JT808_0x8202>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x8202
        /// </summary>
        public ushort MsgId => 0x8202;
        /// <summary>
        /// 临时位置跟踪控制
        /// </summary>
        public string Description => "临时位置跟踪控制";
        /// <summary>
        /// 时间间隔
        /// 单位为秒（s），0 则停止跟踪。停止跟踪无需带后继字段
        /// </summary>
        public ushort Interval { get; set; }
        /// <summary>
        /// 位置跟踪有效期
        /// 单位为秒（s），终端在接收到位置跟踪控制消息后，在有效期截止时间之前，依据消息中的时间间隔发送位置汇报
        /// </summary>
        public int LocationTrackingValidity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8202 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8202 jT808_0X8202 = new JT808_0x8202();
            jT808_0X8202.Interval = reader.ReadUInt16();
            jT808_0X8202.LocationTrackingValidity = reader.ReadInt32();
            return jT808_0X8202;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8202 value, IJT808Config config)
        {
            writer.WriteUInt16(value.Interval);
            writer.WriteInt32(value.LocationTrackingValidity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8202 value = new JT808_0x8202();
            value.Interval = reader.ReadUInt16();
            writer.WriteNumber($"[{ value.Interval.ReadNumber()}]时间间隔", value.Interval);
            value.LocationTrackingValidity = reader.ReadInt32();
            writer.WriteNumber($"[{ value.LocationTrackingValidity.ReadNumber()}]位置跟踪有效期", value.LocationTrackingValidity);
        }
    }
}
