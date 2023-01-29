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
    /// 采集指定的超时驾驶记录
    /// 返回：符合条件的超时驾驶记录
    /// </summary>
    public class JT808_CarDVR_Up_0x11 : JT808MessagePackFormatter<JT808_CarDVR_Up_0x11>, JT808CarDVRUpBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x11
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.collect_specified_driving_overtime_records.ToByteValue();
        /// <summary>
        /// 请求发送指定的时间范围内 N 个单位数据块的数据（N≥1）
        /// </summary>
        public List<JT808_CarDVR_Up_0x11_DriveOverTime> JT808_CarDVR_Up_0x11_DriveOverTimes{ get; set; }
        /// <summary>
        /// 符合条件的超时驾驶记录
        /// </summary>
        public string Description => "符合条件的超时驾驶记录";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            writer.WriteStartArray("请求发送指定的时间范围内 N 个单位数据块的数据");
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 50;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x11_DriveOverTime jT808_CarDVR_Up_0x11_DriveOverTime = new JT808_CarDVR_Up_0x11_DriveOverTime();
                writer.WriteStartObject();
                writer.WriteStartObject($"指定的结束时间前最近的第 {i+1}条超时驾驶记录");
                var hex = reader.ReadVirtualArray(18);
                jT808_CarDVR_Up_0x11_DriveOverTime.DriverLicenseNo = reader.ReadASCII(18);
                writer.WriteString($"[{hex.ToArray().ToHexString()}机动车驾驶证号码]", jT808_CarDVR_Up_0x11_DriveOverTime.DriverLicenseNo);
                hex = reader.ReadVirtualArray(6);
                jT808_CarDVR_Up_0x11_DriveOverTime.ContinueDrivingStartTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{hex.ToArray().ToHexString()}连续驾驶开始时间]", jT808_CarDVR_Up_0x11_DriveOverTime.ContinueDrivingStartTime);
                hex = reader.ReadVirtualArray(6);
                jT808_CarDVR_Up_0x11_DriveOverTime.ContinueDrivingEndTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{hex.ToArray().ToHexString()}连续驾驶结束时间]", jT808_CarDVR_Up_0x11_DriveOverTime.ContinueDrivingEndTime);
                writer.WriteStartObject("连续驾驶开始时间所在的最近一次有效位置信息");
                jT808_CarDVR_Up_0x11_DriveOverTime.GpsStartLng = reader.ReadInt32();
                writer.WriteNumber($"[{ jT808_CarDVR_Up_0x11_DriveOverTime.GpsStartLng.ReadNumber()}]经度", jT808_CarDVR_Up_0x11_DriveOverTime.GpsStartLng);
                jT808_CarDVR_Up_0x11_DriveOverTime.GpsStartLat = reader.ReadInt32();
                writer.WriteNumber($"[{ jT808_CarDVR_Up_0x11_DriveOverTime.GpsStartLat.ReadNumber()}纬度", jT808_CarDVR_Up_0x11_DriveOverTime.GpsStartLat);
                jT808_CarDVR_Up_0x11_DriveOverTime.StartHeight = reader.ReadInt16();
                writer.WriteNumber($"[{ jT808_CarDVR_Up_0x11_DriveOverTime.StartHeight.ReadNumber()}]高度", jT808_CarDVR_Up_0x11_DriveOverTime.StartHeight);
                writer.WriteEndObject();
                writer.WriteStartObject("连续驾驶结束时间所在的最近一次有效位置信息");
                jT808_CarDVR_Up_0x11_DriveOverTime.GpsEndLng = reader.ReadInt32();
                writer.WriteNumber($"[{ jT808_CarDVR_Up_0x11_DriveOverTime.GpsEndLng.ReadNumber()}]经度", jT808_CarDVR_Up_0x11_DriveOverTime.GpsEndLng);
                jT808_CarDVR_Up_0x11_DriveOverTime.GpsEndLat = reader.ReadInt32();
                writer.WriteNumber($"[{ jT808_CarDVR_Up_0x11_DriveOverTime.GpsEndLat.ReadNumber()}]纬度", jT808_CarDVR_Up_0x11_DriveOverTime.GpsEndLat);
                jT808_CarDVR_Up_0x11_DriveOverTime.EndHeight = reader.ReadInt16();
                writer.WriteNumber($"[{ jT808_CarDVR_Up_0x11_DriveOverTime.EndHeight.ReadNumber()}]高度", jT808_CarDVR_Up_0x11_DriveOverTime.EndHeight);
                writer.WriteEndObject();
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
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x11 value, IJT808Config config)
        {
            foreach (var driveOverTime in value.JT808_CarDVR_Up_0x11_DriveOverTimes)
            {
                var currentPosition = writer.GetCurrentPosition();
                writer.WriteASCII(driveOverTime.DriverLicenseNo);
                writer.Skip(18 - (writer.GetCurrentPosition() - currentPosition), out var _);
                writer.WriteDateTime_yyMMddHHmmss(driveOverTime.ContinueDrivingStartTime);
                writer.WriteDateTime_yyMMddHHmmss(driveOverTime.ContinueDrivingEndTime);
                writer.WriteInt32(driveOverTime.GpsStartLng);
                writer.WriteInt32(driveOverTime.GpsStartLat);
                writer.WriteInt16(driveOverTime.StartHeight);
                writer.WriteInt32(driveOverTime.GpsEndLng);
                writer.WriteInt32(driveOverTime.GpsEndLat);
                writer.WriteInt16(driveOverTime.EndHeight);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Up_0x11 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x11 value = new JT808_CarDVR_Up_0x11();
            value.JT808_CarDVR_Up_0x11_DriveOverTimes = new List<JT808_CarDVR_Up_0x11_DriveOverTime>();
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 50;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x11_DriveOverTime jT808_CarDVR_Up_0x11_DriveOverTime = new JT808_CarDVR_Up_0x11_DriveOverTime();
                jT808_CarDVR_Up_0x11_DriveOverTime.DriverLicenseNo = reader.ReadASCII(18);
                jT808_CarDVR_Up_0x11_DriveOverTime.ContinueDrivingStartTime = reader.ReadDateTime_yyMMddHHmmss();
                jT808_CarDVR_Up_0x11_DriveOverTime.ContinueDrivingEndTime = reader.ReadDateTime_yyMMddHHmmss();
                jT808_CarDVR_Up_0x11_DriveOverTime.GpsStartLng = reader.ReadInt32();
                jT808_CarDVR_Up_0x11_DriveOverTime.GpsStartLat = reader.ReadInt32();
                jT808_CarDVR_Up_0x11_DriveOverTime.StartHeight = reader.ReadInt16();
                jT808_CarDVR_Up_0x11_DriveOverTime.GpsEndLng = reader.ReadInt32();
                jT808_CarDVR_Up_0x11_DriveOverTime.GpsEndLat = reader.ReadInt32();
                jT808_CarDVR_Up_0x11_DriveOverTime.EndHeight = reader.ReadInt16();
                value.JT808_CarDVR_Up_0x11_DriveOverTimes.Add(jT808_CarDVR_Up_0x11_DriveOverTime);
            }
            return value;
        }
    }
    /// <summary>
    /// 单位超时驾驶记录数据块
    /// </summary>
    public class JT808_CarDVR_Up_0x11_DriveOverTime
    {
        /// <summary>
        /// 机动车驾驶证号码 18位
        /// </summary>
        public string DriverLicenseNo { get; set; }
        /// <summary>
        /// 连续驾驶开始时间
        /// </summary>
        public DateTime ContinueDrivingStartTime { get; set; }
        /// <summary>
        /// 连续驾驶结束时间
        /// </summary>
        public DateTime ContinueDrivingEndTime { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public int GpsStartLng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public int GpsStartLat { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public short StartHeight { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public int GpsEndLng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public int GpsEndLat { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public short EndHeight { get; set; }
    }
}
