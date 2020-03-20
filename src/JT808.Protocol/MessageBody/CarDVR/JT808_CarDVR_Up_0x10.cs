
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
    public class JT808_CarDVR_Up_0x10 : JT808CarDVRUpBodies, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.采集指定的事故疑点记录.ToByteValue();
        /// <summary>
        /// 请求发送指定的时间范围内 N 个单位数据块的数据（N≥1）
        /// </summary>
        public List<JT808_CarDVR_Up_0x10_AccidentSuspectin> JT808_CarDVR_Up_0x10_AccidentSuspectins { get; set; }
        public override string Description => "符合条件的事故疑点记录";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808CarDVRUpBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x10 value = new JT808_CarDVR_Up_0x10();
            var count = (reader.ReadCurrentRemainContentLength() - 1 - 1) / 234;//记录块个数, -1 去掉808校验位，-1去掉808尾部标志
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x10_AccidentSuspectin jT808_CarDVR_Up_0x10_AccidentSuspectin = new JT808_CarDVR_Up_0x10_AccidentSuspectin();
                jT808_CarDVR_Up_0x10_AccidentSuspectin.EndTime = reader.ReadDateTime6();
                jT808_CarDVR_Up_0x10_AccidentSuspectin.DriverLicenseNo = reader.ReadASCII(18);
                jT808_CarDVR_Up_0x10_AccidentSuspectin.JT808_CarDVR_Up_0x09_DrivingStatuss = new List<JT808_CarDVR_Up_0x10_DrivingStatus>();
                for (int j = 0; j < 100; j++)//100组
                {
                    jT808_CarDVR_Up_0x10_AccidentSuspectin.JT808_CarDVR_Up_0x09_DrivingStatuss.Add(new JT808_CarDVR_Up_0x10_DrivingStatus
                    {
                        Speed = reader.ReadByte(),
                        StatusSignal = reader.ReadByte()
                    });
                }
                jT808_CarDVR_Up_0x10_AccidentSuspectin.GpsLng = reader.ReadInt32();
                jT808_CarDVR_Up_0x10_AccidentSuspectin.GpsLat = reader.ReadInt32();
                jT808_CarDVR_Up_0x10_AccidentSuspectin.Height = reader.ReadInt16();
            }
            return value;
        }
         
        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpBodies jT808CarDVRUpBodies, IJT808Config config)
        {
            JT808_CarDVR_Up_0x10 value = jT808CarDVRUpBodies as JT808_CarDVR_Up_0x10;
            foreach (var accidentSuspectin in value.JT808_CarDVR_Up_0x10_AccidentSuspectins)
            {
                writer.WriteDateTime6(accidentSuspectin.EndTime);
                writer.WriteASCII(accidentSuspectin.DriverLicenseNo.PadRight(18, '0'));
                foreach (var drivingStatus in accidentSuspectin.JT808_CarDVR_Up_0x09_DrivingStatuss)
                {
                    writer.WriteByte(drivingStatus.Speed);
                    writer.WriteByte(drivingStatus.StatusSignal);
                }
                writer.WriteInt32(accidentSuspectin.GpsLng);
                writer.WriteInt32(accidentSuspectin.GpsLat);
                writer.WriteInt16(accidentSuspectin.Height);
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
            public List<JT808_CarDVR_Up_0x10_DrivingStatus> JT808_CarDVR_Up_0x09_DrivingStatuss { get; set; }
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
}
