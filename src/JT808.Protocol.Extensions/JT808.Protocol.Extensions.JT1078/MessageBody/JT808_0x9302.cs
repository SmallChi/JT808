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
    /// 云台调整焦距控制
    /// </summary>
    public class JT808_0x9302 : JT808MessagePackFormatter<JT808_0x9302>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 云台调整焦距控制
        /// </summary>
        public string Description => "云台调整焦距控制";
        /// <summary>
        /// 0x9302
        /// </summary>
        public ushort MsgId => 0x9302;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 焦距调整方向
        /// </summary>
        public byte FocusAdjustmentDirection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            var value = new JT808_0x9302();
 
            value.LogicChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.LogicChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.LogicChannelNo));
            value.FocusAdjustmentDirection = reader.ReadByte();
            writer.WriteString($"[{value.FocusAdjustmentDirection.ReadNumber()}]焦距调整方向", value.FocusAdjustmentDirection==0?"焦距调大":"焦距调小");

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
        public override JT808_0x9302 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            var jT808_0x9302 = new JT808_0x9302();
            jT808_0x9302.LogicChannelNo = reader.ReadByte();
            jT808_0x9302.FocusAdjustmentDirection = reader.ReadByte();
            return jT808_0x9302;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9302 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.FocusAdjustmentDirection);
        }
    }
}
