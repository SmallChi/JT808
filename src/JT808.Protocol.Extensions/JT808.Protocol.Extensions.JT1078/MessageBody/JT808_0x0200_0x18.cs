using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 异常驾驶行为报警详细描述
    /// 0x0200_0x18
    /// </summary>
    public class JT808_0x0200_0x18 : JT808MessagePackFormatter<JT808_0x0200_0x18>, JT808_0x0200_CustomBodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = 0x18;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte AttachInfoLength { get; set; } = 3;
        /// <summary>
        /// 异常驾驶行为报警类型
        /// </summary>
        public ushort AbnormalDrivingBehaviorAlarmType{ get; set; }
        /// <summary>
        /// 疲劳程度
        /// </summary>
        public byte FatigueLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x18 value = new JT808_0x0200_0x18();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.AbnormalDrivingBehaviorAlarmType = reader.ReadUInt16();
            writer.WriteNumber($"[{value.AbnormalDrivingBehaviorAlarmType.ReadNumber()}]异常驾驶行为报警类型", value.AbnormalDrivingBehaviorAlarmType);
            writer.WriteStartArray("视频信号遮挡报警状态集合");
            var abnormalDrivingBehaviorAlarmTypeSpan = Convert.ToString(value.AbnormalDrivingBehaviorAlarmType, 2).PadLeft(16, '0').AsSpan();
            int index = 0;
            foreach (var item in abnormalDrivingBehaviorAlarmTypeSpan)
            {
                string tmpResult = item == '1' ? "有" : "无";
                if (index == 0)
                {
                    writer.WriteStringValue($"[bit{index}疲劳]_{tmpResult}");
                }
                else if (index == 1)
                {
                    writer.WriteStringValue($"[bit{index}打电话]_{tmpResult}");
                }
                else if (index == 2)
                {
                    writer.WriteStringValue($"[bit{index}抽烟]_{tmpResult}");
                }
                else if (index>=3 && index<=10)
                {
                    writer.WriteStringValue($"[bit{index}保留]_{tmpResult}");
                }
                else
                {
                    writer.WriteStringValue($"[bit{index}自定义]_{tmpResult}");
                }
                index++;
            }
            writer.WriteEndArray();
            value.FatigueLevel = reader.ReadByte();
            writer.WriteNumber($"[{value.FatigueLevel.ReadNumber()}]疲劳程度", value.FatigueLevel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x18 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x18 value = new JT808_0x0200_0x18();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.AbnormalDrivingBehaviorAlarmType = reader.ReadUInt16();
            value.FatigueLevel = reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x18 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.AbnormalDrivingBehaviorAlarmType);
            writer.WriteByte(value.FatigueLevel);
        }
    }
}
