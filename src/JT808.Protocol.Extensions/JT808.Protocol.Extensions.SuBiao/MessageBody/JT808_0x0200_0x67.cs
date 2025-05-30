﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using JT808.Protocol.Extensions.SuBiao.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions.SuBiao.MessageBody
{
    /// <summary>
    /// 盲区监测系统报警信息
    /// </summary>
    public class JT808_0x0200_0x67 : JT808MessagePackFormatter<JT808_0x0200_0x67>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 盲区监测系统报警信息Id
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_SuBiao_Constants.JT808_0X0200_0x67;
        /// <summary>
        /// 盲区监测系统报警信息长度
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// 报警ID
        /// </summary>
        public uint AlarmId { get; set; }
        /// <summary>
        /// 标志状态
        /// </summary>
        public byte FlagState { get; set; }
        /// <summary>
        /// 报警/事件类型
        /// </summary>
        public byte AlarmOrEventType { get; set; }
        /// <summary>
        /// 车速
        /// </summary>
        public byte Speed { get; set; }
        /// <summary>
        /// 高程
        /// </summary>
        public ushort Altitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public int Latitude { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public int Longitude { get; set; }
        /// <summary>
        /// 日期时间
        /// YYMMDDhhmmss
        /// BCD[6]
        /// </summary>
        public DateTime AlarmTime { get; set; }
        /// <summary>
        /// 车辆状态
        /// </summary>
        public ushort VehicleState { get; set; }
        /// <summary>
        /// 报警标识号
        /// </summary>
        public AlarmIdentificationProperty AlarmIdentification { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x67 value = new JT808_0x0200_0x67();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.AlarmId = reader.ReadUInt32();
            writer.WriteNumber($"[{value.AlarmId.ReadNumber()}]报警ID", value.AlarmId);
            value.FlagState = reader.ReadByte();
            string flagStateString = "未知";
            switch (value.FlagState)
            {
                case 0:
                    flagStateString = "不可用";
                    break;
                case 1:
                    flagStateString = "开始标志";
                    break;
                case 2:
                    flagStateString = "结束标志";
                    break;
            }
            writer.WriteNumber($"[{value.FlagState.ReadNumber()}]标志状态-{flagStateString}", value.FlagState);
            value.AlarmOrEventType = reader.ReadByte();
            string alarmOrEventTypeString = "";
            switch (value.AlarmOrEventType)
            {
                case 0x01:
                    alarmOrEventTypeString = "前向碰撞报警";
                    break;
                case 0x02:
                    alarmOrEventTypeString = "车道偏离报警";
                    break;
                case 0x03:
                    alarmOrEventTypeString = "车距过近报警";
                    break;
                case 0x04:
                    alarmOrEventTypeString = "行人碰撞报警";
                    break;
                case 0x05:
                    alarmOrEventTypeString = "频繁变道报警";
                    break;
                case 0x06:
                    alarmOrEventTypeString = "道路标识超限报警";
                    break;
                case 0x07:
                    alarmOrEventTypeString = "障碍物报警";
                    break;
                case 0x08:
                case 0x09:
                case 0x0A:
                case 0x0B:
                case 0x0C:
                case 0x0D:
                case 0x0E:
                case 0x0F:
                    alarmOrEventTypeString = "用户自定义";
                    break;
                case 0x10:
                    alarmOrEventTypeString = "道路标志识别事件";
                    break;
                case 0x11:
                    alarmOrEventTypeString = "主动抓拍事件";
                    break;
                case 0x12:
                case 0x13:
                case 0x14:
                case 0x15:
                case 0x16:
                case 0x17:
                case 0x18:
                case 0x19:
                case 0x1A:
                case 0x1B:
                case 0x1C:
                case 0x1D:
                case 0x1E:
                case 0x1F:
                    alarmOrEventTypeString = "用户自定义";
                    break;
            }
            writer.WriteNumber($"[{value.AlarmOrEventType.ReadNumber()}]报警_事件类型-{alarmOrEventTypeString}", value.AlarmOrEventType);
            value.Speed = reader.ReadByte();
            writer.WriteNumber($"[{value.Speed.ReadNumber()}]车速", value.Speed);
            value.Altitude = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Altitude.ReadNumber()}]高程", value.Altitude);
            value.Latitude = (int)reader.ReadUInt32();
            writer.WriteNumber($"[{value.Latitude.ReadNumber()}]纬度", value.Latitude);
            value.Longitude = (int)reader.ReadUInt32();
            writer.WriteNumber($"[{value.Longitude.ReadNumber()}]经度", value.Longitude);
            value.AlarmTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.AlarmTime.ToString("yyMMddHHmmss")}]日期时间", value.AlarmTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.VehicleState = reader.ReadUInt16();
            writer.WriteNumber($"[{value.VehicleState.ReadNumber()}]车辆状态", value.VehicleState);
            var vehicleStateBits = Convert.ToString(value.VehicleState, 2).PadLeft(16, '0');
            writer.WriteStartObject($"车辆状态对象[{vehicleStateBits}]");
            writer.WriteString($"[{vehicleStateBits[15]}]Bit0ACC状态", vehicleStateBits[15] == '0' ? "关闭" : "打开");
            writer.WriteString($"[{vehicleStateBits[14]}]Bit1左转向状态", vehicleStateBits[14] == '0' ? "关闭" : "打开");
            writer.WriteString($"[{vehicleStateBits[13]}]Bit2右转向状态", vehicleStateBits[13] == '0' ? "关闭" : "打开");
            writer.WriteString($"[{vehicleStateBits[12]}]Bit3雨刮器状态", vehicleStateBits[12] == '0' ? "关闭" : "打开");
            writer.WriteString($"[{vehicleStateBits[11]}]Bit4制动状态", vehicleStateBits[11] == '0' ? "未制动" : "制动");
            writer.WriteString($"[{vehicleStateBits[10]}]Bit5插卡状态", vehicleStateBits[10] == '0' ? "未插卡" : "已插卡");
            writer.WriteString($"[{vehicleStateBits[9]}]Bit6自定义", vehicleStateBits[9].ToString());
            writer.WriteString($"[{vehicleStateBits[8]}]Bit7自定义", vehicleStateBits[8].ToString());
            writer.WriteString($"[{vehicleStateBits[7]}]Bit8自定义", vehicleStateBits[7].ToString());
            writer.WriteString($"[{vehicleStateBits[6]}]Bit9自定义", vehicleStateBits[6].ToString());
            writer.WriteString($"[{vehicleStateBits[5]}]Bit10定位状态", vehicleStateBits[5] == '0' ? "未定位" : "已定位");
            writer.WriteString($"[{vehicleStateBits[4]}]Bit11自定义", vehicleStateBits[4].ToString());
            writer.WriteString($"[{vehicleStateBits[3]}]Bit12自定义", vehicleStateBits[3].ToString());
            writer.WriteString($"[{vehicleStateBits[2]}]Bit13自定义", vehicleStateBits[2].ToString());
            writer.WriteString($"[{vehicleStateBits[1]}]Bit14自定义", vehicleStateBits[1].ToString());
            writer.WriteString($"[{vehicleStateBits[0]}]Bit15自定义", vehicleStateBits[0].ToString());
            writer.WriteEndObject();
            value.AlarmIdentification = new AlarmIdentificationProperty();
            string terminalIDHex = reader.ReadVirtualArray(7).ToArray().ToHexString();
            value.AlarmIdentification.TerminalID = reader.ReadString(7);
            value.AlarmIdentification.Time = reader.ReadDateTime_yyMMddHHmmss();
            value.AlarmIdentification.SN = reader.ReadByte();
            value.AlarmIdentification.AttachCount = reader.ReadByte();
            value.AlarmIdentification.Retain = reader.ReadByte();
            writer.WriteString($"[{terminalIDHex}]报警标识号_终端ID", value.AlarmIdentification.TerminalID);
            writer.WriteString($"[{value.AlarmIdentification.Time.ToString("yyMMddHHmmss")}]报警标识号_日期时间", value.AlarmIdentification.Time.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteNumber($"[{value.AlarmIdentification.SN.ReadNumber()}]报警标识号_序号", value.AlarmIdentification.SN);
            writer.WriteNumber($"[{value.AlarmIdentification.AttachCount.ReadNumber()}]报警标识号_附件数量", value.AlarmIdentification.AttachCount);
            writer.WriteNumber($"[{value.AlarmIdentification.Retain.ReadNumber()}]报警标识号_预留", value.AlarmIdentification.Retain);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x67 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x67 value = new JT808_0x0200_0x67();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.AlarmId = reader.ReadUInt32();
            value.FlagState = reader.ReadByte();
            value.AlarmOrEventType = reader.ReadByte();
            value.Speed = reader.ReadByte();
            value.Altitude = reader.ReadUInt16();
            value.Latitude = (int)reader.ReadUInt32();
            value.Longitude = (int)reader.ReadUInt32();
            value.AlarmTime = reader.ReadDateTime_yyMMddHHmmss();
            value.VehicleState = reader.ReadUInt16();
            value.AlarmIdentification = new AlarmIdentificationProperty
            {
                TerminalID = reader.ReadString(7),
                Time = reader.ReadDateTime_yyMMddHHmmss(),
                SN = reader.ReadByte(),
                AttachCount = reader.ReadByte(),
                Retain = reader.ReadByte()
            };
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x67 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.Skip(1, out int AttachInfoLengthPosition);
            writer.WriteUInt32(value.AlarmId);
            writer.WriteByte(value.FlagState);
            writer.WriteByte(value.AlarmOrEventType);
            writer.WriteByte(value.Speed);
            writer.WriteUInt16(value.Altitude);
            writer.WriteUInt32((uint)value.Latitude);
            writer.WriteUInt32((uint)value.Longitude);
            writer.WriteDateTime_yyMMddHHmmss(value.AlarmTime);
            writer.WriteUInt16(value.VehicleState);
            if (value.AlarmIdentification == null)
            {
                throw new NullReferenceException($"{nameof(AlarmIdentificationProperty)}不为空");
            }
            writer.WriteString(value.AlarmIdentification.TerminalID);
            writer.WriteDateTime_yyMMddHHmmss(value.AlarmIdentification.Time);
            writer.WriteByte(value.AlarmIdentification.SN);
            writer.WriteByte(value.AlarmIdentification.AttachCount);
            writer.WriteByte(value.AlarmIdentification.Retain);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - AttachInfoLengthPosition - 1), AttachInfoLengthPosition);
        }
    }
}
