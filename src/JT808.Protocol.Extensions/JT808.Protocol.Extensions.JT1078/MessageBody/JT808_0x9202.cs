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
    /// 平台下发远程录像回放控制(VodControl点播控制)
    /// </summary>
    public class JT808_0x9202 : JT808MessagePackFormatter<JT808_0x9202>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 平台下发远程录像回放控制
        /// </summary>
        public string Description => "平台下发远程录像回放控制";
        /// <summary>
        /// 0x9202
        /// </summary>
        public ushort MsgId => 0x9202;
        /// <summary>
        /// 音视频通道号
        /// </summary>
        public byte ChannelNo { get; set; }
        /// <summary>
        /// 回放控制
        /// 0：开始
        /// 1：暂停
        /// 2：结束
        /// 3：快进
        /// 4：关键帧快退播放
        /// 5：拖动(到指定位置)
        /// 6：关键帧播放
        /// </summary>
        public byte PlayControl { get; set; }
        /// <summary>
        /// 快进或快退倍数，当<see cref="PlayControl"/>为3和4时，此字段有效，否则置0
        /// 0：无效
        /// 1：1倍
        /// 2：2倍
        /// 3：4倍
        /// 4：8倍
        /// 5：16倍
        /// </summary>
        public byte PlaySpeed { get; set; }
        /// <summary>
        /// 拖动回放位置，当<see cref="PlayControl"/>为5时有效（必须）
        /// </summary>
        public DateTime DragPlayPosition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9202 value = new JT808_0x9202();
            value.ChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.ChannelNo.ReadNumber()}]音视频通道号", AVChannelNoDisplay(value.ChannelNo));
            value.PlayControl = reader.ReadByte();
            writer.WriteString($"[{value.PlayControl.ReadNumber()}]回放控制", PlayBackControlDisplay(value.PlayControl));
            value.PlaySpeed = reader.ReadByte();
            writer.WriteString($"[{value.PlaySpeed.ReadNumber()}]快进或快退倍数", FastForwardOrFastRewindMultiplesDisplay(value.PlaySpeed));
            value.DragPlayPosition = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.DragPlayPosition.ToString("yyMMddHHmmss")}]拖动回放位置", value.DragPlayPosition.ToString("yyyy-MM-dd HH:mm:ss"));
            static string AVChannelNoDisplay(byte LogicalChannelNo)
            {
                return LogicalChannelNo switch
                {
                    1 => "驾驶员",
                    2 => "车辆正前方",
                    3 => "车前门",
                    4 => "车厢前部",
                    5 => "车厢后部",
                    7 => "行李舱",
                    8 => "车辆左侧",
                    9 => "车辆右侧",
                    10 => "车辆正后方",
                    11 => "车厢中部",
                    12 => "车中门",
                    13 => "驾驶席车门",
                    33 => "驾驶员",
                    36 => "车厢前部",
                    37 => "车厢后部",
                    _ => "预留",
                };
            }
            static string PlayBackControlDisplay(byte PlayBackControl) {
                return PlayBackControl switch
                {
                    0 => "开始回放",
                    1 => "暂停回放",
                    2 => "结束回放",
                    3 => "快进回放",
                    4 => "关键帧快退回放",
                    5 => "拖动回放",
                    6 => "关键帧播放",
                    _ => "未知",
                };
            }
            static string FastForwardOrFastRewindMultiplesDisplay(byte FastForwardOrFastRewindMultiples)
            {
                return FastForwardOrFastRewindMultiples switch
                {
                    0 => "无效",
                    1 => "1倍",
                    2 => "2倍",
                    3 => "4倍",
                    4 => "8倍",
                    5 => "16倍",
                    _ => "未知",
                };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x9202 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            var jT808_0x9202 = new JT808_0x9202();
            jT808_0x9202.ChannelNo = reader.ReadByte();
            jT808_0x9202.PlayControl = reader.ReadByte();
            jT808_0x9202.PlaySpeed = reader.ReadByte();
            jT808_0x9202.DragPlayPosition = reader.ReadDateTime_yyMMddHHmmss();
            return jT808_0x9202;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9202 value, IJT808Config config)
        {
            writer.WriteByte(value.ChannelNo);
            writer.WriteByte(value.PlayControl);
            writer.WriteByte(value.PlaySpeed);
            writer.WriteDateTime_yyMMddHHmmss(value.DragPlayPosition);
        }
    }
}
