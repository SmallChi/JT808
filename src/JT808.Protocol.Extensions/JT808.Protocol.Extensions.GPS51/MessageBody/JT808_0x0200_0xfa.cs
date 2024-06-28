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
    /// 	4  
    /// 	第0位:震动报警 
    /// 	第1位:拆除报警 
    /// 	例子:第0位:震动报警 fa0400000001 第1位:拆除报警 fa0400000002 第2位:进入深度休眠 fa0400000004 第3位:急加速 fa0400000008 
    /// 	第4位:急减速 fa0400000010 第5位:急转弯 fa0400000020 第6位:acc开报警 fa0400000040 第7位:acc关报警 fa0400000080 
    /// 	第8位:内部电池电量低fa0400000100 第9位:人为关机 fa0400000200 第10位:低电关机 fa0400000400 第11位:设防(状态) fa0400000800
    /// </summary>
    public class JT808_0x0200_0xfa : JT808MessagePackFormatter<JT808_0x0200_0xfa>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_GPS51_Constants.JT808_0x0200_0xfa;
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoLength { get; set; }

        public uint Alarm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0xfa value = new JT808_0x0200_0xfa();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.Alarm = reader.ReadUInt32();
            writer.WriteStartObject("报警信息");
            var alarm = Convert.ToString(value.Alarm, 2).PadLeft(32, '0').AsSpan();
            writer.WriteString("值", Convert.ToString(value.Alarm, 2).PadLeft(32, '0'));
            writer.WriteString("bit0-震动报警", (value.Alarm & 2^0) == 1 ? "有" : "无");
            writer.WriteString("bit1-拆除报警", (value.Alarm & 2 ^ 1) == 1 ? "有" : "无");
            writer.WriteString("bit2-进入深度休眠", (value.Alarm & 2 ^ 2) == 1 ? "有" : "无");
            writer.WriteString("bit3-急加速", (value.Alarm & 2 ^ 3) == 1 ? "有" : "无");
            writer.WriteString("bit4-急减速", (value.Alarm & 2 ^ 4) == 1 ? "有" : "无");
            writer.WriteString("bit5-急转弯", (value.Alarm & 2 ^ 5) == 1 ? "有" : "无");
            writer.WriteString("bit6-acc开报警", (value.Alarm & 2 ^ 6) == 1 ? "有" : "无");
            writer.WriteString("bit7-acc关报警", (value.Alarm & 2 ^ 7) == 1 ? "有" : "无");
            writer.WriteString("bit8-内部电池电量低", (value.Alarm & 2 ^ 8) == 1 ? "有" : "无");
            writer.WriteString("bit9-人为关机", (value.Alarm & 2 ^ 8) == 1 ? "有" : "无");
            writer.WriteString("bit10-低电关机", (value.Alarm & 2 ^ 10) == 1 ? "有" : "无");
            writer.WriteString("bit11-设防(状态)", (value.Alarm & 2 ^ 11) == 1 ? "有" : "无");
            writer.WriteString("bit12~31", "保留");
            writer.WriteEndObject();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0xfa Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0xfa value = new JT808_0x0200_0xfa();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Alarm= reader.ReadUInt32();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0xfa value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(4);
            writer.WriteUInt32(value.Alarm);
        }
    }
}
