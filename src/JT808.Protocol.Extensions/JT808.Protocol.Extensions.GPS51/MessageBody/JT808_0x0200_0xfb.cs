using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using JT808.Protocol.Extensions.GPS51.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions.GPS51.MessageBody
{
    /// <summary>
    /// 4
    /// 电量百分比和外部电压,电压精度0.01V,充电状态0未充电 1充电中,没有的数据传00 
    /// 例子:fb045F050701 解析结果:电量百分比5F=95 电压:0507=1287 最终显示为12.87V 01:充电中
    /// </summary>
    public class JT808_0x0200_0xfb : JT808MessagePackFormatter<JT808_0x0200_0xfb>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_GPS51_Constants.JT808_0x0200_0xfb;
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// 电量百分比
        /// </summary>
        public byte PowerPercent{ get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        public ushort Power { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0xfb value = new JT808_0x0200_0xfb();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.PowerPercent = reader.ReadByte();
            writer.WriteNumber($"[{value.PowerPercent.ReadNumber()}]电量百分比", value.PowerPercent);
            value.Power = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Power.ReadNumber()}]电压", value.Power);
            value.Status = reader.ReadByte();
            writer.WriteString($"[{value.Status.ReadNumber()}]状态", value.Status==0?"未充电":"充电中");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0xfb Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0xfb value = new JT808_0x0200_0xfb();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.PowerPercent = reader.ReadByte();
            value.Power = reader.ReadUInt16();
            value.Status = reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0xfb value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(4);
            writer.WriteByte(value.PowerPercent);
            writer.WriteUInt16(value.Power);
            writer.WriteByte(value.Status);
        }
    }
}
