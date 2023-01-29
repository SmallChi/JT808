
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
    /// 采集指定的事故疑点记录
    /// 返回：符合条件的事故疑点记录
    /// 指定的时间范围内无数据记录，则本数据块数据为空
    /// </summary>
    public class JT808_CarDVR_Up_0x10 : JT808MessagePackFormatter<JT808_CarDVR_Up_0x10>, JT808CarDVRUpBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x10
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.collect_specified_incident_suspect_records.ToByteValue();
        /// <summary>
        /// 请求发送指定的时间范围内 N 个单位数据块的数据（N≥1）
        /// </summary>
        public List<JT808_CarDVR_Up_0x10_AccidentSuspectin> JT808_CarDVR_Up_0x10_AccidentSuspectins { get; set; }
        /// <summary>
        /// 符合条件的事故疑点记录
        /// </summary>
        public string Description => "符合条件的事故疑点记录";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            writer.WriteStartArray("请求发送指定的时间范围内 N 个单位数据块的数据");
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 234;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x10_AccidentSuspectin jT808_CarDVR_Up_0x10_AccidentSuspectin = new JT808_CarDVR_Up_0x10_AccidentSuspectin();
                writer.WriteStartObject();
                writer.WriteStartObject($"指定的结束时间之前最近的第{i+1}条事故疑点记录");
                var hex = reader.ReadVirtualArray(6);
                jT808_CarDVR_Up_0x10_AccidentSuspectin.EndTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{hex.ToArray().ToHexString()}]行驶结束时间", jT808_CarDVR_Up_0x10_AccidentSuspectin.EndTime);
                hex = reader.ReadVirtualArray(18);
                jT808_CarDVR_Up_0x10_AccidentSuspectin.DriverLicenseNo = reader.ReadASCII(18);
                writer.WriteString($"[{hex.ToArray().ToHexString()}]机动车驾驶证号码", jT808_CarDVR_Up_0x10_AccidentSuspectin.DriverLicenseNo);
                for (int j = 0; j < 100; j++)//100组
                {
                    JT808_CarDVR_Up_0x10_DrivingStatus jT808_CarDVR_Up_0X10_DrivingStatus = new JT808_CarDVR_Up_0x10_DrivingStatus();
                    if (j == 0)
                    {
                        writer.WriteStartObject("行驶结束时的速度");
                    }
                    else {
                        writer.WriteStartObject($"行驶结束时间前 { j * 0.2:F1} 秒时的速度");
                    }
                    jT808_CarDVR_Up_0X10_DrivingStatus.Speed = reader.ReadByte();
                    writer.WriteNumber($"[{ jT808_CarDVR_Up_0X10_DrivingStatus.Speed.ReadNumber()}]速度", jT808_CarDVR_Up_0X10_DrivingStatus.Speed);
                    jT808_CarDVR_Up_0X10_DrivingStatus.StatusSignal = reader.ReadByte();
                    writer.WriteNumber($"[{ jT808_CarDVR_Up_0X10_DrivingStatus.StatusSignal.ReadNumber()}]状态信号", jT808_CarDVR_Up_0X10_DrivingStatus.StatusSignal);
                    writer.WriteEndObject();
                }
                jT808_CarDVR_Up_0x10_AccidentSuspectin.GpsLng = reader.ReadInt32();
                writer.WriteNumber($"[{ jT808_CarDVR_Up_0x10_AccidentSuspectin.GpsLng.ReadNumber()}]经度", jT808_CarDVR_Up_0x10_AccidentSuspectin.GpsLng);
                jT808_CarDVR_Up_0x10_AccidentSuspectin.GpsLat = reader.ReadInt32();
                writer.WriteNumber($"[{   jT808_CarDVR_Up_0x10_AccidentSuspectin.GpsLat.ReadNumber()}]纬度", jT808_CarDVR_Up_0x10_AccidentSuspectin.GpsLat);
                jT808_CarDVR_Up_0x10_AccidentSuspectin.Height = reader.ReadInt16();
                writer.WriteNumber($"[{  jT808_CarDVR_Up_0x10_AccidentSuspectin.Height.ReadNumber()}]高度", jT808_CarDVR_Up_0x10_AccidentSuspectin.Height);
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
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x10 value, IJT808Config config)
        {
            foreach (var accidentSuspectin in value.JT808_CarDVR_Up_0x10_AccidentSuspectins)
            {
                writer.WriteDateTime_yyMMddHHmmss(accidentSuspectin.EndTime);
                var currentPosition = writer.GetCurrentPosition();
                writer.WriteASCII(accidentSuspectin.DriverLicenseNo);
                writer.Skip(18 - (writer.GetCurrentPosition() - currentPosition), out var _);
                for (int i = 0; i < 100; i++)
                {
                    if (i < accidentSuspectin.JT808_CarDVR_Up_0x10_DrivingStatuss.Count)
                    {
                        writer.WriteByte(accidentSuspectin.JT808_CarDVR_Up_0x10_DrivingStatuss[i].Speed);
                        writer.WriteByte(accidentSuspectin.JT808_CarDVR_Up_0x10_DrivingStatuss[i].StatusSignal);
                    }
                    else
                    {
                        writer.WriteByte(0xFF);
                        writer.WriteByte(0xFF);
                    }
                }
                writer.WriteInt32(accidentSuspectin.GpsLng);
                writer.WriteInt32(accidentSuspectin.GpsLat);
                writer.WriteInt16(accidentSuspectin.Height);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Up_0x10 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x10 value = new JT808_CarDVR_Up_0x10();
            value.JT808_CarDVR_Up_0x10_AccidentSuspectins = new List<JT808_CarDVR_Up_0x10_AccidentSuspectin>();
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 234;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x10_AccidentSuspectin jT808_CarDVR_Up_0x10_AccidentSuspectin = new JT808_CarDVR_Up_0x10_AccidentSuspectin();
                jT808_CarDVR_Up_0x10_AccidentSuspectin.EndTime = reader.ReadDateTime_yyMMddHHmmss();
                jT808_CarDVR_Up_0x10_AccidentSuspectin.DriverLicenseNo = reader.ReadASCII(18);
                jT808_CarDVR_Up_0x10_AccidentSuspectin.JT808_CarDVR_Up_0x10_DrivingStatuss = new List<JT808_CarDVR_Up_0x10_DrivingStatus>();
                for (int j = 0; j < 100; j++)//100组
                {
                    jT808_CarDVR_Up_0x10_AccidentSuspectin.JT808_CarDVR_Up_0x10_DrivingStatuss.Add(new JT808_CarDVR_Up_0x10_DrivingStatus
                    {
                        Speed = reader.ReadByte(),
                        StatusSignal = reader.ReadByte()
                    });
                }
                jT808_CarDVR_Up_0x10_AccidentSuspectin.GpsLng = reader.ReadInt32();
                jT808_CarDVR_Up_0x10_AccidentSuspectin.GpsLat = reader.ReadInt32();
                jT808_CarDVR_Up_0x10_AccidentSuspectin.Height = reader.ReadInt16();
                value.JT808_CarDVR_Up_0x10_AccidentSuspectins.Add(jT808_CarDVR_Up_0x10_AccidentSuspectin);
            }
            return value;
        }
    }
    /// <summary>
    /// 指定的结束时间之前最近的每条事故疑点记录
    /// 1.本数据块总长度为 234 个字节;
    /// 2.如单位分钟内无数据记录，则本数据块无效，数据长度为0，数据为空
    /// </summary>
    public class JT808_CarDVR_Up_0x10_AccidentSuspectin
    {
        /// <summary>
        /// 行驶结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 机动车驾驶证号码
        /// </summary>
        public string DriverLicenseNo { get; set; }
        /// <summary>
        /// 每 0.2s 间隔采集 1 次，共 100组 20s 的事故疑点记录，按时间倒序排列
        /// </summary>
        public List<JT808_CarDVR_Up_0x10_DrivingStatus> JT808_CarDVR_Up_0x10_DrivingStatuss { get; set; }
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

    }
    /// <summary>
    /// 行驶结束时间前的状态
    /// </summary>
    public class JT808_CarDVR_Up_0x10_DrivingStatus
    {
        /// <summary>
        /// 速度
        /// </summary>
        public byte Speed { get; set; }
        /// <summary>
        /// 状态信号
        /// </summary>
        public byte StatusSignal { get; set; }
    }
}
