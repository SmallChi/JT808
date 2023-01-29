using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody.CarDVR
{
    /// <summary>
    /// 采集指定的位置信息记录
    /// 返回：符合条件的位置信息记录
    /// 指定的时间范围内无数据记录，则本数据块数据为空
    /// </summary>
    public class JT808_CarDVR_Up_0x09 : JT808MessagePackFormatter<JT808_CarDVR_Up_0x09>, JT808CarDVRUpBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x09
        /// </summary>
        public byte CommandId => JT808CarDVRCommandID.collect_specified_location_information.ToByteValue();
        /// <summary>
        /// 请求发送指定的时间范围内 N 个单位数据块的数据（N≥1）
        /// </summary>
        public List<JT808_CarDVR_Up_0x09_PositionPerHour> JT808_CarDVR_Up_0x09_PositionPerHours { get; set; }
        /// <summary>
        /// 符合条件的位置信息记录
        /// </summary>
        public string Description => "符合条件的位置信息记录";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            writer.WriteStartArray("请求发送指定的时间范围内 N 个单位数据块的数据");
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 666;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x09_PositionPerHour jT808_CarDVR_Up_0x09_PositionPerHour = new JT808_CarDVR_Up_0x09_PositionPerHour();
                writer.WriteStartObject();
                writer.WriteStartObject($"指定的结束时间之前最近的第{i+1}小时的位置信息记录");
                var hex = reader.ReadVirtualArray(6);
                jT808_CarDVR_Up_0x09_PositionPerHour.StartTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{hex.ToArray().ToHexString()}]开始时间", jT808_CarDVR_Up_0x09_PositionPerHour.StartTime);
                for (int j = 0; j < 60; j++)//60钟
                {
                    JT808_CarDVR_Up_0x09_PositionPerMinute jT808_CarDVR_Up_0X09_PositionPerMinute = new JT808_CarDVR_Up_0x09_PositionPerMinute();
                    writer.WriteStartObject($"开始时间之后第{j+1}分钟的平均速度和位置信息");
                    jT808_CarDVR_Up_0X09_PositionPerMinute.GpsLng = reader.ReadInt32();
                    writer.WriteNumber($"[{jT808_CarDVR_Up_0X09_PositionPerMinute.GpsLng.ReadNumber()}]经度", jT808_CarDVR_Up_0X09_PositionPerMinute.GpsLng);
                    jT808_CarDVR_Up_0X09_PositionPerMinute.GpsLat = reader.ReadInt32();
                    writer.WriteNumber($"[{jT808_CarDVR_Up_0X09_PositionPerMinute.GpsLat.ReadNumber()}]纬度", jT808_CarDVR_Up_0X09_PositionPerMinute.GpsLat);
                    jT808_CarDVR_Up_0X09_PositionPerMinute.Height = reader.ReadInt16();
                    writer.WriteNumber($"[{jT808_CarDVR_Up_0X09_PositionPerMinute.Height.ReadNumber()}]高度", jT808_CarDVR_Up_0X09_PositionPerMinute.Height);
                    jT808_CarDVR_Up_0X09_PositionPerMinute.AvgSpeedAfterStartTime = reader.ReadByte();
                    writer.WriteNumber($"[{jT808_CarDVR_Up_0X09_PositionPerMinute.AvgSpeedAfterStartTime.ReadNumber()}]平均速度", jT808_CarDVR_Up_0X09_PositionPerMinute.AvgSpeedAfterStartTime);
                    writer.WriteEndObject();
                }
                writer.WriteEndObject();
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x09 value, IJT808Config config)
        {
            foreach (var positionPerHour in value.JT808_CarDVR_Up_0x09_PositionPerHours)
            {
                writer.WriteDateTime_yyMMddHHmmss(positionPerHour.StartTime);
                for (int i = 0; i < 60; i++)
                {
                    if (i < positionPerHour.JT808_CarDVR_Up_0x09_PositionPerMinutes.Count)
                    {
                        writer.WriteInt32(positionPerHour.JT808_CarDVR_Up_0x09_PositionPerMinutes[i].GpsLng);
                        writer.WriteInt32(positionPerHour.JT808_CarDVR_Up_0x09_PositionPerMinutes[i].GpsLat);
                        writer.WriteInt16(positionPerHour.JT808_CarDVR_Up_0x09_PositionPerMinutes[i].Height);
                        writer.WriteByte(positionPerHour.JT808_CarDVR_Up_0x09_PositionPerMinutes[i].AvgSpeedAfterStartTime);
                    }
                    else {
                        writer.WriteUInt32(0xFFFFFFFF);
                        writer.WriteUInt32(0xFFFFFFFF);
                        writer.WriteUInt16(0xFFFF);
                        writer.WriteByte(0xFF);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Up_0x09 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x09 value = new JT808_CarDVR_Up_0x09();
            value.JT808_CarDVR_Up_0x09_PositionPerHours = new List<JT808_CarDVR_Up_0x09_PositionPerHour>();
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 666;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x09_PositionPerHour jT808_CarDVR_Up_0x09_PositionPerHour = new JT808_CarDVR_Up_0x09_PositionPerHour()
                {
                    StartTime = reader.ReadDateTime_yyMMddHHmmss(),
                    JT808_CarDVR_Up_0x09_PositionPerMinutes = new List<JT808_CarDVR_Up_0x09_PositionPerMinute>()
                };
                for (int j = 0; j < 60; j++)//60钟
                {
                    jT808_CarDVR_Up_0x09_PositionPerHour.JT808_CarDVR_Up_0x09_PositionPerMinutes.Add(new JT808_CarDVR_Up_0x09_PositionPerMinute
                    {
                        GpsLng = reader.ReadInt32(),
                        GpsLat = reader.ReadInt32(),
                        Height = reader.ReadInt16(),
                        AvgSpeedAfterStartTime = reader.ReadByte()
                    });
                }
                value.JT808_CarDVR_Up_0x09_PositionPerHours.Add(jT808_CarDVR_Up_0x09_PositionPerHour);
            }
            return value;
        }
    }
    /// <summary>
    /// 指定的结束时间之前最近的每 小时的位置信息记录
    /// 1.本数据块总长度为 666 个字节，不足部分以 FFH补齐；
    /// 2.如单位分钟内无数据记录，则本数据块无效，数据长度为0，数据为空
    /// </summary>
    public class JT808_CarDVR_Up_0x09_PositionPerHour
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 60s钟，每秒的信息
        /// </summary>
        public List<JT808_CarDVR_Up_0x09_PositionPerMinute> JT808_CarDVR_Up_0x09_PositionPerMinutes { get; set; }

    }
    /// <summary>
    /// 开始时间之后每分钟的平均速度和位置信息
    /// </summary>
    public class JT808_CarDVR_Up_0x09_PositionPerMinute
    {
        /// <summary>
        /// 经度
        /// </summary>
        public int GpsLng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public int GpsLat { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public short Height { get; set; }
        /// <summary>
        /// 开始时间之后每分钟的平均速度
        /// </summary>
        public byte AvgSpeedAfterStartTime { get; set; }
    }
}
