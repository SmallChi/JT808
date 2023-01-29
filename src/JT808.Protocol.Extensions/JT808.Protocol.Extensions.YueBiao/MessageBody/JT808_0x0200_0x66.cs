using JT808.Protocol.Extensions.YueBiao.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.YueBiao.MessageBody
{
    /// <summary>
    /// 胎压监测系统报警信息
    /// </summary>
    public class JT808_0x0200_0x66 : JT808MessagePackFormatter<JT808_0x0200_0x66>, JT808_0x0200_CustomBodyBase, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 胎压监测系统报警信息Id
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_YueBiao_Constants.JT808_0X0200_0x66;
        /// <summary>
        /// 胎压监测系统报警信息长度
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
        /// 报警/事件列表总数
        /// </summary>
        public byte AlarmOrEventCount { get; set; }
        /// <summary>
        /// 报警/事件信息列表
        /// </summary>
        public List<AlarmOrEventProperty> AlarmOrEvents { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x66 value = new JT808_0x0200_0x66();
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
            string terminalIDHex = reader.ReadVirtualArray(30).ToArray().ToHexString();
            value.AlarmIdentification.TerminalId = reader.ReadString(30);
            value.AlarmIdentification.Time = reader.ReadDateTime_yyMMddHHmmss();
            value.AlarmIdentification.SN = reader.ReadByte();
            value.AlarmIdentification.AttachCount = reader.ReadByte();
            value.AlarmIdentification.Retain1 = reader.ReadByte();
            value.AlarmIdentification.Retain2 = reader.ReadByte();
            value.AlarmOrEventCount = reader.ReadByte();
            writer.WriteNumber($"[{value.AlarmOrEventCount.ReadNumber()}]报警_事件列表总数", value.AlarmOrEventCount);
            if (value.AlarmOrEventCount > 0)
            {
                writer.WriteStartArray("报警_事件列表");
                for (int i = 0; i < value.AlarmOrEventCount; i++)
                {
                    writer.WriteStartObject();
                    AlarmOrEventProperty item = new AlarmOrEventProperty();
                    item.TirePressureAlarmPosition = reader.ReadUInt16();
                    writer.WriteNumber($"[{item.TirePressureAlarmPosition.ReadNumber()}]胎压报警位置", item.TirePressureAlarmPosition);
                    item.AlarmOrEventType = reader.ReadUInt16();
                    writer.WriteNumber($"[{item.AlarmOrEventType.ReadNumber()}]事件/报警类型", item.AlarmOrEventType);
                    var AlarmOrEventTypeBits = Convert.ToString(item.AlarmOrEventType, 2).PadLeft(16, '0');
                    writer.WriteStartObject($"事件/报警类型[{AlarmOrEventTypeBits}]");
                    writer.WriteString($"[{AlarmOrEventTypeBits[15]}]Bit0胎压(定时上报)", AlarmOrEventTypeBits[15] == '0' ? "无报警" : "有报警");
                    writer.WriteString($"[{AlarmOrEventTypeBits[14]}]Bit1胎压过高报警", AlarmOrEventTypeBits[14] == '0' ? "无报警" : "有报警");
                    writer.WriteString($"[{AlarmOrEventTypeBits[13]}]Bit2胎压过低报警", AlarmOrEventTypeBits[13] == '0' ? "无报警" : "有报警");
                    writer.WriteString($"[{AlarmOrEventTypeBits[12]}]Bit3胎温过高报警", AlarmOrEventTypeBits[12] == '0' ? "无报警" : "有报警");
                    writer.WriteString($"[{AlarmOrEventTypeBits[11]}]Bit4传感器异常报警", AlarmOrEventTypeBits[11] == '0' ? "无报警" : "有报警");
                    writer.WriteString($"[{AlarmOrEventTypeBits[10]}]Bit5胎压不平衡报警", AlarmOrEventTypeBits[10] == '0' ? "无报警" : "有报警");
                    writer.WriteString($"[{AlarmOrEventTypeBits[9]}]Bit6慢漏气报警", AlarmOrEventTypeBits[9] == '0' ? "无报警" : "有报警");
                    writer.WriteString($"[{AlarmOrEventTypeBits[8]}]Bit7电池电量低报警", AlarmOrEventTypeBits[8] == '0' ? "无报警" : "有报警");
                    writer.WriteString($"[{AlarmOrEventTypeBits[7]}]Bit8自定义", AlarmOrEventTypeBits[7].ToString());
                    writer.WriteString($"[{AlarmOrEventTypeBits[6]}]Bit9自定义", AlarmOrEventTypeBits[6].ToString());
                    writer.WriteString($"[{AlarmOrEventTypeBits[5]}]Bit10自定义", AlarmOrEventTypeBits[5].ToString());
                    writer.WriteString($"[{AlarmOrEventTypeBits[4]}]Bit11自定义", AlarmOrEventTypeBits[4].ToString());
                    writer.WriteString($"[{AlarmOrEventTypeBits[3]}]Bit12自定义", AlarmOrEventTypeBits[3].ToString());
                    writer.WriteString($"[{AlarmOrEventTypeBits[2]}]Bit13自定义", AlarmOrEventTypeBits[2].ToString());
                    writer.WriteString($"[{AlarmOrEventTypeBits[1]}]Bit14自定义", AlarmOrEventTypeBits[1].ToString());
                    writer.WriteString($"[{AlarmOrEventTypeBits[0]}]Bit15自定义", AlarmOrEventTypeBits[0].ToString());
                    writer.WriteEndObject();
                    item.TirePressure = reader.ReadUInt16();
                    writer.WriteNumber($"[{item.TirePressure.ReadNumber()}]胎压Kpa", item.TirePressure);
                    item.TireTemperature = reader.ReadUInt16();
                    writer.WriteNumber($"[{item.TireTemperature.ReadNumber()}]胎温℃", item.TireTemperature);
                    item.BatteryLevel = reader.ReadUInt16();
                    writer.WriteNumber($"[{item.BatteryLevel.ReadNumber()}]电池电量%", item.BatteryLevel);
                    writer.WriteEndObject();
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
        public override JT808_0x0200_0x66 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x66 value = new JT808_0x0200_0x66();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.AlarmId = reader.ReadUInt32();
            value.FlagState = reader.ReadByte();
            value.Speed = reader.ReadByte();
            value.Altitude = reader.ReadUInt16();
            value.Latitude = (int)reader.ReadUInt32();
            value.Longitude = (int)reader.ReadUInt32();
            value.AlarmTime = reader.ReadDateTime_yyMMddHHmmss();
            value.VehicleState = reader.ReadUInt16();
            value.AlarmIdentification = new AlarmIdentificationProperty
            {
                TerminalId = reader.ReadString(30),
                Time = reader.ReadDateTime_yyMMddHHmmss(),
                SN = reader.ReadByte(),
                AttachCount = reader.ReadByte(),
                Retain1 = reader.ReadByte(),
                Retain2 = reader.ReadByte()
            };
            value.AlarmOrEventCount = reader.ReadByte();
            if (value.AlarmOrEventCount > 0)
            {
                value.AlarmOrEvents = new List<AlarmOrEventProperty>();
                for (int i = 0; i < value.AlarmOrEventCount; i++)
                {
                    AlarmOrEventProperty alarmOrEventProperty = new AlarmOrEventProperty();
                    alarmOrEventProperty.TirePressureAlarmPosition = reader.ReadUInt16();
                    alarmOrEventProperty.AlarmOrEventType = reader.ReadUInt16();
                    alarmOrEventProperty.TirePressure = reader.ReadUInt16();
                    alarmOrEventProperty.TireTemperature = reader.ReadUInt16();
                    alarmOrEventProperty.BatteryLevel = reader.ReadUInt16();
                    value.AlarmOrEvents.Add(alarmOrEventProperty);
                }
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x66 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.Skip(1, out int AttachInfoLengthPosition);
            writer.WriteUInt32(value.AlarmId);
            writer.WriteByte(value.FlagState);
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
            writer.WriteString(value.AlarmIdentification.TerminalId.PadRight(30, '\0'));
            writer.WriteDateTime_yyMMddHHmmss(value.AlarmIdentification.Time);
            writer.WriteByte(value.AlarmIdentification.SN);
            writer.WriteByte(value.AlarmIdentification.AttachCount);
            writer.WriteByte(value.AlarmIdentification.Retain1);
            writer.WriteByte(value.AlarmIdentification.Retain2);
            if (value.AlarmOrEvents.Count > 0)
            {
                writer.WriteByte((byte)value.AlarmOrEvents.Count);
                foreach (var item in value.AlarmOrEvents)
                {
                    writer.WriteUInt16(item.TirePressureAlarmPosition);
                    writer.WriteUInt16(item.AlarmOrEventType);
                    writer.WriteUInt16(item.TirePressure);
                    writer.WriteUInt16(item.TireTemperature);
                    writer.WriteUInt16(item.BatteryLevel);
                }
            }
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - AttachInfoLengthPosition - 1), AttachInfoLengthPosition);
        }
    }
}
