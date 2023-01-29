using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 音视频参数设置
    /// 0x8103_0x0075
    /// </summary>
    public class JT808_0x8103_0x0075 : JT808MessagePackFormatter<JT808_0x8103_0x0075>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public uint ParamId { get; set; } = 0x0075;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte ParamLength { get; set; } = 21;
        /// <summary>
        /// 实时流编码模式
        /// </summary>
        public byte RTS_EncodeMode { get; set; }
        /// <summary>
        /// 实时流分辨率
        /// </summary>
        public byte RTS_Resolution { get; set; }
        /// <summary>
        /// 实时流关键帧间隔
        /// （范围1-1000）帧
        /// </summary>
        public ushort RTS_KF_Interval { get; set; }
        /// <summary>
        /// 实时流目标帧率
        /// </summary>
        public byte RTS_Target_FPS { get; set; }
        /// <summary>
        /// 实时流目标码率
        /// 单位未千位每秒（kbps）
        /// </summary>
        public uint RTS_Target_CodeRate { get; set; }
        /// <summary>
        /// 存储流编码模式
        /// </summary>
        public byte StreamStore_EncodeMode { get; set; }
        /// <summary>
        /// 存储流分辨率
        /// </summary>
        public byte StreamStore_Resolution { get; set; }
        /// <summary>
        /// 存储流关键帧间隔
        /// （范围1-1000）帧
        /// </summary>
        public ushort StreamStore_KF_Interval { get; set; }
        /// <summary>
        /// 存储流目标帧率
        /// </summary>
        public byte StreamStore_Target_FPS { get; set; }
        /// <summary>
        /// 存储流目标码率
        /// 单位未千位每秒（kbps）
        /// </summary>
        public uint StreamStore_Target_CodeRate { get; set; }
        /// <summary>
        ///OSD字幕叠加设置
        /// </summary>
        public ushort OSD { get; set; }
        /// <summary>
        ///是否启用音频输出
        ///0:不启用
        ///1：启用
        /// </summary>
        public byte AudioOutputEnabled { get; set; }
        /// <summary>
        /// 音视频参数设置
        /// </summary>
        public string Description => "音视频参数设置";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0075 value = new JT808_0x8103_0x0075();
            value.ParamId = reader.ReadUInt32();
            writer.WriteNumber($"[{value.ParamId.ReadNumber()}]参数 ID", value.ParamId);
            value.ParamLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]数据长度", value.ParamLength);
            value.RTS_EncodeMode = reader.ReadByte();
            writer.WriteString($"[{value.RTS_EncodeMode.ReadNumber()}]实时流编码模式", RTS_EncodeModeDisplay(value.RTS_EncodeMode));
            value.RTS_Resolution = reader.ReadByte();
            writer.WriteString($"[{value.RTS_Resolution.ReadNumber()}]实时流分辨率", RTS_ResolutionDisplay(value.RTS_Resolution));
            value.RTS_KF_Interval = reader.ReadUInt16();
            writer.WriteNumber($"[{value.RTS_KF_Interval.ReadNumber()}]实时流关键帧间隔(帧)", value.RTS_KF_Interval);
            value.RTS_Target_FPS = reader.ReadByte();
            writer.WriteNumber($"[{value.RTS_Target_FPS.ReadNumber()}]实时流目标帧率(帧/s)", value.RTS_Target_FPS);
            value.RTS_Target_CodeRate = reader.ReadUInt32();
            writer.WriteNumber($"[{value.RTS_Target_CodeRate.ReadNumber()}]实时流目标码流(kbps)", value.RTS_Target_CodeRate);
            value.StreamStore_EncodeMode = reader.ReadByte();
            writer.WriteString($"[{value.StreamStore_EncodeMode.ReadNumber()}]存储量编码模式", StreamStore_EncodeModeDisplay(value.StreamStore_EncodeMode));
            value.StreamStore_Resolution = reader.ReadByte();
            writer.WriteString($"[{value.StreamStore_Resolution.ReadNumber()}]存储流分辨率", StreamStore_ResolutionDisplay(value.StreamStore_Resolution));
            value.StreamStore_KF_Interval = reader.ReadUInt16();
            writer.WriteNumber($"[{value.StreamStore_KF_Interval.ReadNumber()}]存储流关键帧间隔(帧)", value.StreamStore_KF_Interval);
            value.StreamStore_Target_FPS = reader.ReadByte();
            writer.WriteNumber($"[{value.StreamStore_Target_FPS.ReadNumber()}]存储流目标帧率(帧/s)", value.StreamStore_Target_FPS);
            value.StreamStore_Target_CodeRate = reader.ReadUInt32();
            writer.WriteNumber($"[{value.StreamStore_Target_CodeRate.ReadNumber()}]存储流目标码流(kbps)", value.StreamStore_Target_CodeRate);
            value.OSD = reader.ReadUInt16();
            writer.WriteString($"[{value.OSD.ReadNumber()}]OBD字幕叠加设置", OBDDisplay(value.OSD));
            value.AudioOutputEnabled = reader.ReadByte();
            writer.WriteString($"[{value.AudioOutputEnabled.ReadNumber()}]是否启用音频输出", value.AudioOutputEnabled == 0 ? "不启用" : "启用");
           string RTS_EncodeModeDisplay(byte RTS_EncodeMode) {
                switch (RTS_EncodeMode)
                {
                    case 0:
                        return "CBR固定码流";
                    case 1:
                        return "VBR可变码流";
                    case 2:
                        return "ABR平均码流";
                    default:
                        break;
                }
                return "未知";
            }
            string RTS_ResolutionDisplay(byte RTS_Resolution) {
                switch (RTS_Resolution)
                {
                    case 0:
                        return "QCIF";
                    case 1:
                        return "CIF";
                    case 2:
                        return "WCIF";
                    case 3:
                        return "D1";
                    case 4:
                        return "WD1";
                    case 5:
                        return "720P";
                    case 6:
                        return "1080P";
                    default:
                        break;
                }
                return "未知";
            }
            string StreamStore_EncodeModeDisplay(byte StreamStore_EncodeMode) {
                {
                    switch (RTS_EncodeMode)
                    {
                        case 0:
                            return "CBR固定码流";
                        case 1:
                            return "VBR可变码流";
                        case 2:
                            return "ABR平均码流";
                        default:
                            break;
                    }
                    return "未知";
                }
            }
            string StreamStore_ResolutionDisplay(byte StreamStore_Resolution) {
                switch (RTS_Resolution)
                {
                    case 0:
                        return "QCIF";
                    case 1:
                        return "CIF";
                    case 2:
                        return "WCIF";
                    case 3:
                        return "D1";
                    case 4:
                        return "WD1";
                    case 5:
                        return "720P";
                    case 6:
                        return "1080P";
                    default:
                        break;
                }
                return "未知";
            }
            string OBDDisplay(ushort OBD) {
                string obdDisplay = string.Empty;
                obdDisplay += ((OBD & 0x0001) == 1) ? ",日期和时间" : "";
                obdDisplay += ((OBD >> 1 & 0x0001) == 1) ? ",车牌号码" : "";
                obdDisplay += ((OBD >> 2 & 0x0001) == 1) ? ",逻辑通道号" : "";
                obdDisplay += ((OBD >> 3 & 0x0001) == 1) ? ",经纬度" : "";
                obdDisplay += ((OBD >> 4 & 0x0001) == 1) ? ",行驶记录速度" : "";
                obdDisplay += ((OBD >> 5 & 0x0001) == 1) ? ",卫星定位速度" : "";
                obdDisplay += ((OBD >> 6 & 0x0001) == 1) ? ",连续驾驶时间" : "";
                return obdDisplay.Length > 0 ? obdDisplay.Substring(1) : "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0075 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0075 jT808_0X8103_0X0075 = new JT808_0x8103_0x0075();
            jT808_0X8103_0X0075.ParamId = reader.ReadUInt32();
            jT808_0X8103_0X0075.ParamLength = reader.ReadByte();
            jT808_0X8103_0X0075.RTS_EncodeMode = reader.ReadByte();
            jT808_0X8103_0X0075.RTS_Resolution = reader.ReadByte();
            jT808_0X8103_0X0075.RTS_KF_Interval = reader.ReadUInt16();
            jT808_0X8103_0X0075.RTS_Target_FPS = reader.ReadByte();
            jT808_0X8103_0X0075.RTS_Target_CodeRate = reader.ReadUInt32();
            jT808_0X8103_0X0075.StreamStore_EncodeMode = reader.ReadByte();
            jT808_0X8103_0X0075.StreamStore_Resolution = reader.ReadByte();
            jT808_0X8103_0X0075.StreamStore_KF_Interval = reader.ReadUInt16();
            jT808_0X8103_0X0075.StreamStore_Target_FPS = reader.ReadByte();
            jT808_0X8103_0X0075.StreamStore_Target_CodeRate = reader.ReadUInt32();
            jT808_0X8103_0X0075.OSD = reader.ReadUInt16();
            jT808_0X8103_0X0075.AudioOutputEnabled = reader.ReadByte();
            return jT808_0X8103_0X0075;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0075 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.RTS_EncodeMode);
            writer.WriteByte(value.RTS_Resolution);
            writer.WriteUInt16(value.RTS_KF_Interval);
            writer.WriteByte(value.RTS_Target_FPS);
            writer.WriteUInt32(value.RTS_Target_CodeRate);
            writer.WriteByte(value.StreamStore_EncodeMode);
            writer.WriteByte(value.StreamStore_Resolution);
            writer.WriteUInt16(value.StreamStore_KF_Interval);
            writer.WriteByte(value.StreamStore_Target_FPS);
            writer.WriteUInt32(value.StreamStore_Target_CodeRate);
            writer.WriteUInt16(value.OSD);
            writer.WriteByte(value.AudioOutputEnabled);
        }
    }
}
