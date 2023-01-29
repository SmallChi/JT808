using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 扩展车辆信号状态位
    /// </summary>
    public class JT808_0x0200_0x25 : JT808MessagePackFormatter<JT808_0x0200_0x25>, JT808_0x0200_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 扩展车辆信号状态位
        /// <seealso cref="JT808.Protocol.Enums.JT808CarSignalStatus"/>
        /// </summary>
        public uint CarSignalStatus { get; set; }
        /// <summary>
        /// JT808_0x0200_0x25
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x25;
        /// <summary>
        /// 4 byte
        /// </summary>
        public byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x25 value = new JT808_0x0200_0x25();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.CarSignalStatus = reader.ReadUInt32();
            writer.WriteNumber($"[{value.CarSignalStatus.ReadNumber()}]扩展车辆信号状态位", value.CarSignalStatus);
            writer.WriteStartObject("扩展车辆信号状态位对象信息");
            var carSignalStatus = Convert.ToString(value.CarSignalStatus, 2).PadLeft(32, '0').AsSpan();
            writer.WriteString("值", Convert.ToString(value.CarSignalStatus, 2).PadLeft(32, '0'));
            writer.WriteString("bit15~31", "保留");
            writer.WriteString("bit14-离合器状态", (value.CarSignalStatus & 16384) == 16384 ? "离合器状态" : "无");
            writer.WriteString("bit13-加热器工作", (value.CarSignalStatus & 8192) == 8192 ? "加热器工作" : "无");
            writer.WriteString("bit12-ABS工作", (value.CarSignalStatus & 4096) == 4096 ? "ABS工作" : "无");
            writer.WriteString("bit11-缓速器工作", (value.CarSignalStatus & 2048) == 2048 ? "缓速器工作" : "无");
            writer.WriteString("bit10-空挡信号", (value.CarSignalStatus & 1024) == 1024 ? "空挡信号" : "无");
            writer.WriteString("bit9-空调状态", (value.CarSignalStatus & 512) == 512 ? "空调状态" : "无");
            writer.WriteString("bit8-喇叭信号", (value.CarSignalStatus & 256) == 256 ? "喇叭信号" : "无");
            writer.WriteString("bit7-示廓灯", (value.CarSignalStatus & 128) == 128 ? "示廓灯" : "无");
            writer.WriteString("bit6-雾灯信号", (value.CarSignalStatus & 64) == 64 ? "雾灯信号" : "无");
            writer.WriteString("bit5-倒档信号", (value.CarSignalStatus & 32) == 32 ? "倒档信号" : "无");
            writer.WriteString("bit4-制动信号", (value.CarSignalStatus & 16) == 16 ? "制动信号" : "无");
            writer.WriteString("bit3-左转向灯信号", (value.CarSignalStatus & 8) == 8 ? "左转向灯信号" : "无");
            writer.WriteString("bit2-右转向灯信号", (value.CarSignalStatus & 4) == 4 ? "右转向灯信号" : "无");
            writer.WriteString("bit1-远光灯信号", (value.CarSignalStatus & 2) == 2 ? "远光灯信号" : "无");
            writer.WriteString("bit0-近光灯信号", (value.CarSignalStatus & 1) ==1?"近光灯信号":"无");
            writer.WriteEndObject();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x25 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x25 value = new JT808_0x0200_0x25();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.CarSignalStatus = reader.ReadUInt32();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x25 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.CarSignalStatus);
        }
    }
}
