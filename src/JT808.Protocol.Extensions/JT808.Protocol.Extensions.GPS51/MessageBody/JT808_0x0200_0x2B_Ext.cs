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
    /// 多路油耗模拟量
    /// </summary>
    public class JT808_0x0200_0x2B_Ext : JT808MessagePackFormatter<JT808_0x0200_0x2B_Ext>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 多路油耗模拟量,Id
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_GPS51_Constants.JT808_0x0200_0x2B;
        /// <summary>
        /// 多路油耗模拟量信息附加长度
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// 油量数据
        /// </summary>
        public List<ushort> Oils { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x2B_Ext value = new JT808_0x0200_0x2B_Ext();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.Oils = new List<ushort>();
            for (ushort i = 0; i < value.AttachInfoLength/2; i++) {
                value.Oils.Add(reader.ReadUInt16());
            }
            writer.WriteString($"[油量值：{string.Join("", value.Oils.Select(m=>m.ReadNumber()))}]",string.Join("，", value.Oils));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x2B_Ext Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x2B_Ext value = new JT808_0x0200_0x2B_Ext();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Oils = new List<ushort>();
            for (int i = 0; i < value.AttachInfoLength/2; i++) {
                value.Oils.Add(reader.ReadUInt16());
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x2B_Ext value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte((byte)(value.Oils.Count*2));
            foreach (var item in value.Oils)
            {
                writer.WriteUInt16(item);
            }
        }
    }
}
