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
    /// 平台下发远程录像回放请求(vod点播请求)
    /// </summary>
    public class JT808_0x9201 : JT808MessagePackFormatter<JT808_0x9201>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 平台下发远程录像回放请求
        /// </summary>
        public string Description => "平台下发远程录像回放请求";
        /// <summary>
        /// 0x9201
        /// </summary>
        public ushort MsgId => 0x9201;
        /// <summary>
        /// 服务器IP地址长度
        /// </summary>
        public byte ServerIpLength { get; set; }
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string ServerIp { get; set; }
        /// <summary>
        /// 视频服务器TCP端口号，不使用TCP协议传输时保持默认值0即可（TCP和UDP二选一，当TCP和UDP均非默认值时一般以TCP为准）
        /// </summary>
        public ushort TcpPort { get; set; }
        /// <summary>
        /// 视频服务器UDP端口号，不使用UDP协议传输时保持默认值0即可（TCP和UDP二选一，当TCP和UDP均非默认值时一般以TCP为准）
        /// </summary>
        public ushort UdpPort { get; set; }
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelNo { get; set; }
        /// <summary>
        /// 音视频类型(媒体类型)
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
        /// 如果此通道只传输音频，置为0
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
        /// 回放方式
        /// 0：正常
        /// 1：快进
        /// 2：关键帧快退回放
        /// 3：关键帧播放
        /// 4：单帧上传
        /// </summary>
        public byte PlaybackWay { get; set; }
        /// <summary>
        /// 快进或快退倍数，当<see cref="PlaybackWay"/>为1和2时，此字段有效，否则置0
        /// 0：无效
        /// 1：1倍
        /// 2：2倍
        /// 3：4倍
        /// 4：8倍
        /// 5：16倍
        /// </summary>
        public byte PlaySpeed { get; set; }
        /// <summary>
        /// 开始时间，当<see cref="PlaybackWay"/>为4时，该字段表示单帧上传时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间，当<see cref="PlaybackWay"/>为4时，该字段无效
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 格式分析
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            var value = new JT808_0x9201();
            value.ServerIpLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ServerIpLength.ReadNumber()}]服务器IP地址长度", value.ServerIpLength);
            string ipHex = reader.ReadVirtualArray(value.ServerIpLength).ToArray().ToHexString();
            value.ServerIp = reader.ReadString(value.ServerIpLength);
            writer.WriteString($"[{ipHex}]服务器IP地址", value.ServerIp);
            value.TcpPort = reader.ReadUInt16();
            writer.WriteNumber($"[{value.TcpPort.ReadNumber()}]服务器视频通道监听端口号(TCP)", value.TcpPort);
            value.UdpPort = reader.ReadUInt16();
            writer.WriteNumber($"[{value.UdpPort.ReadNumber()}]服务器视频通道监听端口号（UDP）", value.UdpPort);
            value.ChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.ChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.ChannelNo));
            value.MediaType = reader.ReadByte();
            writer.WriteString($"[{value.MediaType.ReadNumber()}]音视频类型", AVItemTypeDisplay(value.MediaType));
            value.StreamType = reader.ReadByte();
            writer.WriteString($"[{value.StreamType.ReadNumber()}]码流类型", StreamTypeDisplay(value.StreamType));
            value.MemoryType = reader.ReadByte();
            writer.WriteString($"[{value.MemoryType.ReadNumber()}]存储器类型", MemTypeDisplay(value.MemoryType));
            value.PlaybackWay = reader.ReadByte();
            writer.WriteString($"[{value.PlaybackWay.ReadNumber()}]回访方式", PlayBackWayDisplay(value.PlaybackWay));
            value.PlaySpeed = reader.ReadByte();
            writer.WriteString($"[{value.PlaySpeed.ReadNumber()}]快进或快退倍数", FastForwardOrFastRewindMultiplesDisplay(value.PlaySpeed));
            value.BeginTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.BeginTime:yyMMddHHmmss}]起始时间", value.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.EndTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.EndTime:yyMMddHHmmss}]结束时间", value.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            static string AVItemTypeDisplay(byte AVItemType)
            {
                return AVItemType switch
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
                    0 => "主码流或子码流",
                    1 => "主码流",
                    2 => "子码流",
                    _ => "未知",
                };
            }
            static string MemTypeDisplay(byte MemType)
            {
                return MemType switch
                {
                    0 => "主存储器或灾备服务器",
                    1 => "主存储器",
                    2 => "灾备服务器",
                    _ => "未知",
                };
            }
            static string PlayBackWayDisplay(byte PlayBackWay)
            {
                return PlayBackWay switch
                {
                    0 => "正常回放",
                    1 => "快进回放",
                    2 => "关键帧快退回访",
                    3 => "关键帧播放",
                    4 => "单帧上传",
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
        ///  反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x9201 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9201 jT808_0x9201 = new JT808_0x9201();
            jT808_0x9201.ServerIpLength = reader.ReadByte();
            jT808_0x9201.ServerIp = reader.ReadString(jT808_0x9201.ServerIpLength);
            jT808_0x9201.TcpPort = reader.ReadUInt16();
            jT808_0x9201.UdpPort = reader.ReadUInt16();
            jT808_0x9201.ChannelNo = reader.ReadByte();
            jT808_0x9201.MediaType = reader.ReadByte();
            jT808_0x9201.StreamType = reader.ReadByte();
            jT808_0x9201.MemoryType = reader.ReadByte();
            jT808_0x9201.PlaybackWay = reader.ReadByte();
            jT808_0x9201.PlaySpeed = reader.ReadByte();
            jT808_0x9201.BeginTime = reader.ReadDateTime_yyMMddHHmmss();
            jT808_0x9201.EndTime = reader.ReadDateTime_yyMMddHHmmss();
            return jT808_0x9201;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9201 value, IJT808Config config)
        {
            writer.Skip(1, out int position);
            writer.WriteString(value.ServerIp);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position - 1), position);//计算完字符串后，回写字符串长度
            writer.WriteUInt16(value.TcpPort);
            writer.WriteUInt16(value.UdpPort);
            writer.WriteByte(value.ChannelNo);
            writer.WriteByte(value.MediaType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemoryType);
            writer.WriteByte(value.PlaybackWay);
            writer.WriteByte(value.PlaySpeed);
            writer.WriteDateTime_yyMMddHHmmss(value.BeginTime);
            writer.WriteDateTime_yyMMddHHmmss(value.EndTime);
        }
    }
}
