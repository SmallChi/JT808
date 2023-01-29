using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 视频信号遮挡报警状态
    /// 0x0200_0x16
    /// </summary>
    public class JT808_0x0200_0x16 : JT808MessagePackFormatter<JT808_0x0200_0x16>, JT808_0x0200_CustomBodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = 0x16;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 视频信号遮挡报警状态
        /// </summary>
        public uint VideoSignalOcclusionAlarmStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x16 value = new JT808_0x0200_0x16();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.VideoSignalOcclusionAlarmStatus = reader.ReadUInt32();
            writer.WriteNumber($"[{value.VideoSignalOcclusionAlarmStatus.ReadNumber()}]视频信号遮挡报警状态", value.VideoSignalOcclusionAlarmStatus);
            var videoSignalOcclusionAlarmStatusSpan = Convert.ToString(value.VideoSignalOcclusionAlarmStatus, 2).PadLeft(32, '0').AsSpan();
            writer.WriteStartArray("视频信号遮挡报警状态集合");
            int index = 0;
            foreach (var item in videoSignalOcclusionAlarmStatusSpan)
            {
                if (item == '1')
                {
                    writer.WriteStringValue($"{index}通道视频信号遮挡");
                }
                else
                {
                    writer.WriteStringValue($"{index}通道视频正常");
                }
                index++;
            }
            writer.WriteEndArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x16 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x16 value = new JT808_0x0200_0x16();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.VideoSignalOcclusionAlarmStatus = reader.ReadUInt32();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x16 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.VideoSignalOcclusionAlarmStatus);
        }
    }
}
