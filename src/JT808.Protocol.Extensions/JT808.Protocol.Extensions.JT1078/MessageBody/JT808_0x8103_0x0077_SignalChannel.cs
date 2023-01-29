using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 
    /// </summary>
    public class JT808_0x8103_0x0077_SignalChannel: JT808MessagePackFormatter<JT808_0x8103_0x0077_SignalChannel>, IJT808Analyze
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
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
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0077_SignalChannel value = new JT808_0x8103_0x0077_SignalChannel();
            value.LogicChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.LogicChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.LogicChannelNo));
            value.RTS_EncodeMode = reader.ReadByte();
            writer.WriteString($"[{value.RTS_EncodeMode.ReadNumber()}]实时流编码模式", RTS_EncodeModeDisplay(value.RTS_EncodeMode));
            value.RTS_Resolution = reader.ReadByte();
            writer.WriteString($"[{value.RTS_Resolution.ReadNumber()}]实时流分辨率", RTS_ResolutionDisplay(value.RTS_Resolution));
            value.RTS_KF_Interval = reader.ReadUInt16();
            writer.WriteNumber($"[{value.RTS_KF_Interval.ReadNumber()}]实时流关键帧间隔(帧)", value.RTS_KF_Interval);
            value.RTS_Target_FPS = reader.ReadByte();
            writer.WriteNumber($"[{value.RTS_Target_FPS.ReadNumber()}]实时流目标帧率(帧/s)", value.RTS_Target_FPS);
            value.RTS_Target_CodeRate = reader.ReadUInt32();
            writer.WriteNumber($"[{value.RTS_Target_CodeRate.ReadNumber()}]实时流目标码率(kbps)", value.RTS_Target_CodeRate);
            value.StreamStore_EncodeMode = reader.ReadByte();
            writer.WriteString($"[{value.StreamStore_EncodeMode.ReadNumber()}]存储流编码模式", StreamStore_EncodeModeDisplay(value.StreamStore_EncodeMode));
            value.StreamStore_Resolution = reader.ReadByte();
            writer.WriteString($"[{value.StreamStore_Resolution.ReadNumber()}]存储流分辨率", StreamStore_ResolutionDisplay(value.StreamStore_Resolution));
            value.StreamStore_KF_Interval = reader.ReadUInt16();
            writer.WriteNumber($"[{value.StreamStore_KF_Interval.ReadNumber()}]存储流关键帧间隔(帧)", value.StreamStore_KF_Interval);
            value.StreamStore_Target_FPS = reader.ReadByte();
            writer.WriteNumber($"[{value.StreamStore_Target_FPS.ReadNumber()}]存储流目标帧率(帧/s)", value.StreamStore_Target_FPS);
            value.StreamStore_Target_CodeRate = reader.ReadUInt32();
            writer.WriteNumber($"[{value.StreamStore_Target_CodeRate.ReadNumber()}]存储流目标码率(kbps)", value.StreamStore_Target_CodeRate);
            value.OSD = reader.ReadUInt16();
            writer.WriteString($"[{value.OSD.ReadNumber()}]OSD字幕叠加设置", OBDDisplay(value.OSD));
            string LogicalChannelNoDisplay(byte LogicalChannelNo)
            {
                switch (LogicalChannelNo)
                {
                    case 1:
                        return "驾驶员";
                    case 2:
                        return "车辆正前方";
                    case 3:
                        return "车前门";
                    case 4:
                        return "车厢前部";
                    case 5:
                        return "车厢后部";
                    case 7:
                        return "行李舱";
                    case 8:
                        return "车辆左侧";
                    case 9:
                        return "车辆右侧";
                    case 10:
                        return "车辆正后方";
                    case 11:
                        return "车厢中部";
                    case 12:
                        return "车中门";
                    case 13:
                        return "驾驶席车门";
                    case 33:
                        return "驾驶员";
                    case 36:
                        return "车厢前部";
                    case 37:
                        return "车厢后部";
                    default:
                        return "预留";
                }
            }
            string RTS_EncodeModeDisplay(byte RTS_EncodeMode)
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
            string RTS_ResolutionDisplay(byte RTS_Resolution)
            {
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
            string StreamStore_EncodeModeDisplay(byte StreamStore_EncodeMode)
            {
                {
                    switch (StreamStore_EncodeMode)
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
            string StreamStore_ResolutionDisplay(byte StreamStore_Resolution)
            {
                switch (StreamStore_Resolution)
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
            string OBDDisplay(ushort OBD)
            {
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
        public override JT808_0x8103_0x0077_SignalChannel Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0077_SignalChannel jT808_0X8103_0X0077_SignalChannel = new JT808_0x8103_0x0077_SignalChannel();
            jT808_0X8103_0X0077_SignalChannel.LogicChannelNo = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.RTS_EncodeMode = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.RTS_Resolution = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.RTS_KF_Interval = reader.ReadUInt16();
            jT808_0X8103_0X0077_SignalChannel.RTS_Target_FPS = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.RTS_Target_CodeRate = reader.ReadUInt32();
            jT808_0X8103_0X0077_SignalChannel.StreamStore_EncodeMode = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.StreamStore_Resolution = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.StreamStore_KF_Interval = reader.ReadUInt16();
            jT808_0X8103_0X0077_SignalChannel.StreamStore_Target_FPS = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.StreamStore_Target_CodeRate = reader.ReadUInt32();
            jT808_0X8103_0X0077_SignalChannel.OSD = reader.ReadUInt16();
            return jT808_0X8103_0X0077_SignalChannel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0077_SignalChannel value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
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
        }
    }
}
