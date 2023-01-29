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
    /// 音视频实时传输控制(LiveControl直播控制)
    /// </summary>
    public class JT808_0x9102 : JT808MessagePackFormatter<JT808_0x9102>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 音视频实时传输控制
        /// </summary>
        public string Description => "音视频实时传输控制";
        /// <summary>
        /// 0x9102
        /// </summary>
        public ushort MsgId => 0x9102;

        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelNo { get; set; }
        /// <summary>
        /// 控制指令
        /// 平台可以通过该指令对设备的实时音视频进行控制：
        /// 0:关闭音视频传输指令
        /// 1:切换码流（增加暂停和继续）
        /// 2:暂停该通道所有流的发送
        /// 3:恢复暂停前流的发送,与暂停前的流类型一致
        /// 4:关闭双向对讲
        /// </summary>
        public byte ControlCmd { get; set; }
        /// <summary>
        /// 关闭音视频类型
        /// 0:关闭该通道有关的音视频数据
        /// 1:只关闭该通道有关的音频，保留该通道有关的视频
        /// 2:只关闭该通道有关的视频，保留该通道有关的音频
        /// </summary>
        public byte CloseAVData { get; set; }
        /// <summary>
        /// 切换码流类型
        /// 将之前申请的码流切换为新申请的码流，音频与切换前保持一致。
        /// 新申请的码流为：
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
            JT808_0x9102 value = new JT808_0x9102();
            value.ChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.ChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.ChannelNo));
            value.ControlCmd = reader.ReadByte();
            writer.WriteString($"[{value.ControlCmd.ReadNumber()}]控制指令", ControlCmdDisplay(value.ControlCmd));
            value.CloseAVData = reader.ReadByte();
            writer.WriteString($"[{value.CloseAVData.ReadNumber()}]关闭音视频类型", CloseAVDataDisplay(value.CloseAVData));
            value.StreamType = reader.ReadByte();
            writer.WriteString($"[{value.StreamType.ReadNumber()}]切换码流类型", value.StreamType == 0 ? "主码流" : "子码流");

            string CloseAVDataDisplay(byte CloseAVData)
            {
                return CloseAVData switch
                {
                    0 => "关闭该通道有关的音视频数据",
                    1 => "只关闭该通道有关的音频，保留该通道有关的视频",
                    2 => "只关闭该通道有关的视频，保留该通道有关的音频",
                    _ => "未知",
                };
            }
            string ControlCmdDisplay(byte ControlCmd)
            {
                return ControlCmd switch
                {
                    0 => "关闭音视频传输指令",
                    1 => "切换码流（增加暂停和继续）",
                    2 => "暂停该通道所有流的发送",
                    3 => "恢复暂停前流的发送,与暂停前的流类型一致",
                    4 => "关闭双向对讲",
                    _ => "未知",
                };
            }
            string LogicalChannelNoDisplay(byte LogicalChannelNo)
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
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x9102 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            var jT808_0X9102 = new JT808_0x9102();
            jT808_0X9102.ChannelNo = reader.ReadByte();
            jT808_0X9102.ControlCmd = reader.ReadByte();
            jT808_0X9102.CloseAVData = reader.ReadByte();
            jT808_0X9102.StreamType = reader.ReadByte();
            return jT808_0X9102;
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9102 value, IJT808Config config)
        {
            writer.WriteByte(value.ChannelNo);
            writer.WriteByte(value.ControlCmd);
            writer.WriteByte(value.CloseAVData);
            writer.WriteByte(value.StreamType);
        }
    }
}
