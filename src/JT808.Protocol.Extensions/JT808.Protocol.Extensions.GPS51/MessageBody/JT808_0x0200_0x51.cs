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
    /// 温度
    /// </summary>
    public class JT808_0x0200_0x51 : JT808MessagePackFormatter<JT808_0x0200_0x51>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 温度,Id
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_GPS51_Constants.JT808_0x0200_0x51;
        /// <summary>
        /// 温度信息附加长度
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// 温度数据
        /// </summary>
        public List<short> Temperatures { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x51 value = new JT808_0x0200_0x51();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.Temperatures = new List<short>();
            for (ushort i = 0; i < value.AttachInfoLength/2; i++) 
            {
                value.Temperatures.Add(reader.ReadInt16());
            }
            writer.WriteString($"[温度值：{string.Join("", value.Temperatures.Select(m=>m.ReadNumber()))}]",string.Join("，", value.Temperatures));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x51 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x51 value = new JT808_0x0200_0x51();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Temperatures = new List<short>();
            for (int i = 0; i < value.AttachInfoLength / 2; i++) {
                value.Temperatures.Add(reader.ReadInt16());
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x51 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte((byte)(value.Temperatures.Count*2));
            foreach (var item in value.Temperatures)
            {
                writer.WriteInt16(item);
            }
        }
    }
}
