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
    /// 查询资源列表
    /// </summary>
    public class JT808_0x9205 : JT808MessagePackFormatter<JT808_0x9205>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 查询资源列表
        /// </summary>
        public string Description => "查询资源列表";
        /// <summary>
        /// 0x9205
        /// </summary>
        public ushort MsgId => 0x9205;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelNo { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime  { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 报警标志
        /// </summary>
        public ulong AlarmFlag { get; set; }
        /// <summary>
        /// 音视频资源类型
        /// 0：audio and video
        /// 1：audio
        /// 2：video
        /// 3：audio or video
        /// </summary>
        public byte MediaType { get; set; }
        /// <summary>
        /// 码流类型
        /// 0：主或子码流
        /// 1：主
        /// 2：子
        /// </summary>
        public byte StreamType { get; set; }
        /// <summary>
        /// 存储器类型
        /// 0：主或灾备存储器
        /// 1：主存储器
        /// 2：灾备存储器
        /// </summary>
        public byte MemoryType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9205 value = new JT808_0x9205();
            value.ChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.ChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.ChannelNo));
            value.BeginTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.BeginTime.ToString("yyMMddHHmmss")}]起始时间", value.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.EndTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.EndTime.ToString("yyMMddHHmmss")}]起始时间", value.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.AlarmFlag = reader.ReadUInt64();
            writer.WriteNumber($"[{value.AlarmFlag.ReadNumber()}]报警标志", value.AlarmFlag);
            value.MediaType = reader.ReadByte();
            writer.WriteString($"[{value.MediaType.ReadNumber()}]音视频类型", AVResourceTypeDisplay(value.MediaType));
            value.StreamType = reader.ReadByte();
            writer.WriteString($"[{value.StreamType.ReadNumber()}]码流类型", StreamTypeDisplay(value.StreamType));
            value.MemoryType = reader.ReadByte();
            writer.WriteString($"[{value.MemoryType.ReadNumber()}]存储器类型", MemoryTypeDisplay(value.MemoryType));

            static string AVResourceTypeDisplay(byte AVResourceType)
            {
                return AVResourceType switch
                {
                    0 => "音视频",
                    1 => "音频",
                    2 => "视频",
                    3 => "音频或视频",
                    _ => "未知",
                };
            }
            static string StreamTypeDisplay(byte StreamType)
            {
                return StreamType switch
                {
                    0 => "所有码流",
                    1 => "主码流",
                    2 => "子码流",
                    _ => "未知",
                };
            }
            static string MemoryTypeDisplay(byte MemType)
            {
                return MemType switch
                {
                    0 => "所有存储器",
                    1 => "主存储器",
                    2 => "灾备服务器",
                    _ => "未知",
                };
            }
            static string LogicalChannelNoDisplay(byte LogicalChannelNo)
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
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x9205 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            var jT808_0x9205 = new JT808_0x9205();
            jT808_0x9205.ChannelNo = reader.ReadByte();
            jT808_0x9205.BeginTime = reader.ReadDateTime_yyMMddHHmmss();
            jT808_0x9205.EndTime = reader.ReadDateTime_yyMMddHHmmss();
            jT808_0x9205.AlarmFlag = reader.ReadUInt64();
            jT808_0x9205.MediaType = reader.ReadByte();
            jT808_0x9205.StreamType = reader.ReadByte();
            jT808_0x9205.MemoryType = reader.ReadByte();
            return jT808_0x9205;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9205 value, IJT808Config config)
        {
            writer.WriteByte(value.ChannelNo);
            writer.WriteDateTime_yyMMddHHmmss(value.BeginTime);
            writer.WriteDateTime_yyMMddHHmmss(value.EndTime);
            writer.WriteUInt64(value.AlarmFlag);
            writer.WriteByte(value.MediaType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemoryType);
        }
    }
}
