using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody.CarDVR
{
    /// <summary>
    /// 采集指定的参数修改记录
    /// 返回：符合条件的参数修改记录
    /// </summary>
    public class JT808_CarDVR_Down_0x14 : JT808CarDVRDownBodies
    {
        public override byte CommandId => JT808CarDVRCommandID.采集指定的参数修改记录.ToByteValue();

        public override string Description => "符合条件的参数修改记录";
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 最大单位数据块个数
        /// </summary>
        public ushort Count { get; set; }
        public override JT808CarDVRDownBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Down_0x14 value = new JT808_CarDVR_Down_0x14();
            value.StartTime = reader.ReadDateTime6();
            value.EndTime = reader.ReadDateTime6();
            value.Count = reader.ReadUInt16();
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRDownBodies jT808CarDVRDownBodies, IJT808Config config)
        {
            JT808_CarDVR_Down_0x14 value = jT808CarDVRDownBodies as JT808_CarDVR_Down_0x14;
            writer.WriteDateTime6(value.StartTime);
            writer.WriteDateTime6(value.EndTime);
            writer.WriteUInt16(value.Count);
        }
    }
}
