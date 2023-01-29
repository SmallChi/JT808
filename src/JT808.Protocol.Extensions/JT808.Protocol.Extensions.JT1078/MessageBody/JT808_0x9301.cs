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
    /// 云台旋转
    /// </summary>
    public class JT808_0x9301 : JT808MessagePackFormatter<JT808_0x9301>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 云台旋转
        /// </summary>
        public string Description => "云台旋转";
        /// <summary>
        /// 0x9301
        /// </summary>
        public ushort MsgId => 0x9301;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelNo { get; set; }
        /// <summary>
        /// 方向
        /// 0：停止
        /// 1：上
        /// 2：下
        /// 3：左
        /// 4：右
        /// </summary>
        public byte Direction { get; set; }
        /// <summary>
        /// 速度
        /// 0-255
        /// </summary>
        public byte Speed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9301 value = new JT808_0x9301();
            value.ChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.ChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.ChannelNo));
            value.Direction = reader.ReadByte();
            writer.WriteString($"[{value.Direction.ReadNumber()}]方向", DirectionDisplay(value.Direction));
            value.Speed = reader.ReadByte();
            writer.WriteNumber($"[{value.Speed.ReadNumber()}]速度", value.Speed);
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
            static string DirectionDisplay(byte Direction) {
                return Direction switch
                {
                    0 => "停止",
                    1 => "上",
                    2 => "下",
                    3 => "左",
                    4 => "右",
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
        public override JT808_0x9301 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            var jT808_0x9301 = new JT808_0x9301();
            jT808_0x9301.ChannelNo = reader.ReadByte();
            jT808_0x9301.Direction = reader.ReadByte();
            jT808_0x9301.Speed = reader.ReadByte();
            return jT808_0x9301;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9301 value, IJT808Config config)
        {
            writer.WriteByte(value.ChannelNo);
            writer.WriteByte(value.Direction);
            writer.WriteByte(value.Speed);
        }
    }
}
