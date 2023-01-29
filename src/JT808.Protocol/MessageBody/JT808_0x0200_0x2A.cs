using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// IO状态位
    /// </summary>
    public class JT808_0x0200_0x2A : JT808MessagePackFormatter<JT808_0x0200_0x2A>, JT808_0x0200_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// IO状态位
        /// Flags
        /// <see cref="JT808.Protocol.Enums.JT808IOStatus"/>
        /// </summary>
        public ushort IOStatus { get; set; }
        /// <summary>
        /// JT808_0x0200_0x2A
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x2A;
        /// <summary>
        /// AttachInfoLength
        /// </summary>
        public byte AttachInfoLength { get; set; } = 2;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x2A value = new JT808_0x0200_0x2A();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.IOStatus = reader.ReadUInt16();
            writer.WriteNumber($"[{value.IOStatus.ReadNumber()}]IO状态位", value.IOStatus);
            writer.WriteStartObject("IO状态位对象信息");
            var carSignalStatus = Convert.ToString(value.IOStatus, 2).PadLeft(16, '0').AsSpan();
            writer.WriteString("值", Convert.ToString(value.IOStatus, 2).PadLeft(16, '0'));
            writer.WriteString("bit2~15", "保留");
            writer.WriteString("bit1", (value.IOStatus & 2) == 2 ? "休眠状态" : "无");
            writer.WriteString("bit0", (value.IOStatus & 1) == 1 ? "深度休眠状态" : "无");
            writer.WriteEndObject();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x2A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x2A value = new JT808_0x0200_0x2A();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.IOStatus = reader.ReadUInt16();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x2A value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.IOStatus);
        }
    }
}
