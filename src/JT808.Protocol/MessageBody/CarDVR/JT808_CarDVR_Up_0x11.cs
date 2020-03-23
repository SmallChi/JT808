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
    public class JT808_CarDVR_Up_0x11 : JT808CarDVRUpBodies, IJT808MessagePackFormatter<JT808_CarDVR_Up_0x11>, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.采集指定的超时驾驶记录.ToByteValue();
        /// <summary>
        /// 请求发送指定的时间范围内 N 个单位数据块的数据（N≥1）
        /// </summary>
        public List<JT808_CarDVR_Up_0x11_DriveOverTime> JT808_CarDVR_Up_0x11_DriveOverTimes{ get; set; }
        public override string Description => "符合条件的超时驾驶记录";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x11 value, IJT808Config config)
        {
            foreach (var driveOverTime in value.JT808_CarDVR_Up_0x11_DriveOverTimes)
            {
                writer.WriteASCII(driveOverTime.DriverLicenseNo.PadRight(18, '0'));
                writer.WriteDateTime6(driveOverTime.ContinueDrivingStartTime);
                writer.WriteDateTime6(driveOverTime.ContinueDrivingEndTime);
                writer.WriteInt32(driveOverTime.GpsStartLng);
                writer.WriteInt32(driveOverTime.GpsStartLat);
                writer.WriteInt16(driveOverTime.StartHeight);
                writer.WriteInt32(driveOverTime.GpsEndLng);
                writer.WriteInt32(driveOverTime.GpsStartLat);
                writer.WriteInt16(driveOverTime.EndHeight);
            }
        }

        public JT808_CarDVR_Up_0x11 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x11 value = new JT808_CarDVR_Up_0x11();
            value.JT808_CarDVR_Up_0x11_DriveOverTimes = new List<JT808_CarDVR_Up_0x11_DriveOverTime>();
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 50;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x11_DriveOverTime jT808_CarDVR_Up_0x11_DriveOverTime = new JT808_CarDVR_Up_0x11_DriveOverTime();
                jT808_CarDVR_Up_0x11_DriveOverTime.DriverLicenseNo = reader.ReadASCII(18);
                jT808_CarDVR_Up_0x11_DriveOverTime.ContinueDrivingStartTime = reader.ReadDateTime6();
                jT808_CarDVR_Up_0x11_DriveOverTime.ContinueDrivingEndTime = reader.ReadDateTime6();
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
