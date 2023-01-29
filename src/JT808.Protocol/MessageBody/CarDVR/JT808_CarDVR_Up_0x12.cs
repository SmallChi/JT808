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
    /// 采集指定的驾驶人身份记录
    /// 返回：符合条件的驾驶人登录退出记录
    /// </summary>
    public class JT808_CarDVR_Up_0x12 : JT808MessagePackFormatter<JT808_CarDVR_Up_0x12>, JT808CarDVRUpBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x12
        /// </summary>
        public byte CommandId => JT808CarDVRCommandID.collect_drivers_identification_records.ToByteValue();
        /// <summary>
        /// 请求发送指定的时间范围内 N 个单位数据块的数据（N≥1）
        /// </summary>
        public List<JT808_CarDVR_Up_0x12_DriveLogin> JT808_CarDVR_Up_0x12_DriveLogins { get; set; }
        /// <summary>
        /// 符合条件的驾驶人登录退出记录
        /// </summary>
        public string Description => "符合条件的驾驶人登录退出记录";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            writer.WriteStartArray("请求发送指定的时间范围内 N 个单位数据块的数据");
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 25;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x12_DriveLogin jT808_CarDVR_Up_0x12_DriveLogin = new JT808_CarDVR_Up_0x12_DriveLogin();
                writer.WriteStartObject();
                writer.WriteStartObject($"指定的结束时间之前最近的第 {i+1}条驾驶人登录退出记录");
                var hex = reader.ReadVirtualArray(6);
                jT808_CarDVR_Up_0x12_DriveLogin.LoginTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{hex.ToArray().ToHexString()}]登录/登出发生时间", jT808_CarDVR_Up_0x12_DriveLogin.LoginTime);
                hex = reader.ReadVirtualArray(18);
                jT808_CarDVR_Up_0x12_DriveLogin.DriverLicenseNo = reader.ReadASCII(18);
                writer.WriteString($"[{hex.ToArray().ToHexString()}]机动车驾驶证号码", jT808_CarDVR_Up_0x12_DriveLogin.DriverLicenseNo);
                jT808_CarDVR_Up_0x12_DriveLogin.LoginType = reader.ReadByte();
                writer.WriteString($"[{ jT808_CarDVR_Up_0x12_DriveLogin.LoginType.ReadNumber()}]登录/登出事件", LoginTypeDisplay(jT808_CarDVR_Up_0x12_DriveLogin.LoginType));
                writer.WriteEndObject();
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            static string LoginTypeDisplay(byte loginType){
                if (loginType == 1)
                {
                    return "登录";
                }
                else if(loginType == 2)
                {
                    return "退出";
                }
                else {
                    return "保留";
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x12 value, IJT808Config config)
        {
            foreach (var driveLogin in value.JT808_CarDVR_Up_0x12_DriveLogins)
            {
                writer.WriteDateTime_yyMMddHHmmss(driveLogin.LoginTime);
                var currentPosition = writer.GetCurrentPosition();
                writer.WriteASCII(driveLogin.DriverLicenseNo);
                writer.Skip(18 - (writer.GetCurrentPosition() - currentPosition), out var _);
                writer.WriteByte(driveLogin.LoginType);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Up_0x12 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x12 value = new JT808_CarDVR_Up_0x12();
            value.JT808_CarDVR_Up_0x12_DriveLogins = new List<JT808_CarDVR_Up_0x12_DriveLogin>();
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 25;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x12_DriveLogin jT808_CarDVR_Up_0x12_DriveLogin = new JT808_CarDVR_Up_0x12_DriveLogin();
                jT808_CarDVR_Up_0x12_DriveLogin.LoginTime = reader.ReadDateTime_yyMMddHHmmss();
                jT808_CarDVR_Up_0x12_DriveLogin.DriverLicenseNo = reader.ReadASCII(18);
                jT808_CarDVR_Up_0x12_DriveLogin.LoginType = reader.ReadByte();
                value.JT808_CarDVR_Up_0x12_DriveLogins.Add(jT808_CarDVR_Up_0x12_DriveLogin);
            }
            return value;
        }
    }
    /// <summary>
    /// 单位驾驶人身份记录数据块格式
    /// </summary>
    public class JT808_CarDVR_Up_0x12_DriveLogin
    {
        /// <summary>
        /// 登入登出时间发生时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 机动车驾驶证号码 18位
        /// </summary>
        public string DriverLicenseNo { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public byte LoginType { get; set; }
    }
}
