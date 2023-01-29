using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Interfaces;
using System.Text.Json;
using JT808.Protocol.Extensions.JT1078.Enums;
using System.Linq;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 视频相关报警
    /// 0x0200_0x14
    /// </summary>
    public class JT808_0x0200_0x14 : JT808MessagePackFormatter<JT808_0x0200_0x14>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = 0x14;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 视频相关报警
        /// <see cref="JT808.Protocol.Extensions.JT1078.Enums.VideoRelateAlarmType"/>
        /// </summary>
        public uint VideoRelateAlarm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x14 value = new JT808_0x0200_0x14();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.VideoRelateAlarm = reader.ReadUInt32();
            writer.WriteNumber($"[{value.VideoRelateAlarm.ReadNumber()}]视频相关报警", value.VideoRelateAlarm);
            var videoRelateAlarmFlags = JT808EnumExtensions.GetEnumTypes<VideoRelateAlarmType>(value.VideoRelateAlarm, 32);
            if (videoRelateAlarmFlags.Any())
            {
                writer.WriteStartArray("视频报警集合");
                foreach (var item in videoRelateAlarmFlags)
                {
                    writer.WriteStringValue(item.ToString());
                }
                writer.WriteEndArray();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x14 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x14 value = new JT808_0x0200_0x14();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.VideoRelateAlarm = reader.ReadUInt32();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x14 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.VideoRelateAlarm);
        }
    }
}
