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
    /// 实时音视频传输请求（live、talk、listen、fanout、passThrough直播、对讲、监听、广播、透传请求）
    /// </summary>
    public class JT808_0x9101: JT808MessagePackFormatter<JT808_0x9101>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 实时音视频传输请求
        /// </summary>
        public string Description => "实时音视频传输请求";
        /// <summary>
        /// 0x9101
        /// </summary>
        public ushort MsgId => 0x9101;

        /// <summary>
        /// 视频服务器IP地址长度
        /// </summary>
        public byte ServerIpLength { get;internal set; }
        /// <summary>
        /// 视频服务器IP地址
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
        /// 数据类型
        /// 0:音视频
        /// 1:视频
        /// 2:双向对讲
        /// 3:监听
        /// 4:中心广播
        /// 5:透传
        /// </summary>
        public byte DataType { get; set; }
        /// <summary>
        /// 码流类型
        /// 0:主码流
        /// 1:子码流
        /// </summary>
        public byte StreamType { get; set; }

        /// <summary>
        /// 格式分析
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            var value = new JT808_0x9101();
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
            value.DataType = reader.ReadByte();
            writer.WriteString($"[{value.DataType.ReadNumber()}]数据类型", DataTypeDisplay(value.DataType));
            value.StreamType = reader.ReadByte();
            writer.WriteString($"[{value.StreamType.ReadNumber()}]码流类型", value.StreamType==0?"主码流":"子码流");
            string DataTypeDisplay(byte DataType) {
                return DataType switch
                {
                    0 => "音视频",
                    1 => "视频",
                    2 => "双向对讲",
                    3 => "监听",
                    4 => "中心广播",
                    5 => "透传",
                    _ => "未知",
                };
            }
            string LogicalChannelNoDisplay(byte LogicalChannelNo) {
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
        public override JT808_0x9101 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            var jT808_0x9101 = new JT808_0x9101();
            jT808_0x9101.ServerIpLength = reader.ReadByte();
            jT808_0x9101.ServerIp = reader.ReadString(jT808_0x9101.ServerIpLength);
            jT808_0x9101.TcpPort = reader.ReadUInt16();
            jT808_0x9101.UdpPort = reader.ReadUInt16();
            jT808_0x9101.ChannelNo = reader.ReadByte();
            jT808_0x9101.DataType = reader.ReadByte();
            jT808_0x9101.StreamType = reader.ReadByte();
            return jT808_0x9101;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9101 value, IJT808Config config)
        {
            writer.Skip(1, out int position);
            writer.WriteString(value.ServerIp);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position - 1), position);
            writer.WriteUInt16(value.TcpPort);
            writer.WriteUInt16(value.UdpPort);
            writer.WriteByte(value.ChannelNo);
            writer.WriteByte(value.DataType);
            writer.WriteByte(value.StreamType);
        }
    }
}
