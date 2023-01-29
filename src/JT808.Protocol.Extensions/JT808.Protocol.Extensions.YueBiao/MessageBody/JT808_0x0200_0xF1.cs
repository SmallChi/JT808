using JT808.Protocol.Extensions.YueBiao.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.YueBiao.MessageBody
{
    /// <summary>
    /// 安装异常信息
    /// </summary>
    public class JT808_0x0200_0xF1 : JT808MessagePackFormatter<JT808_0x0200_0xF1>, JT808_0x0200_CustomBodyBase, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 安装异常信息附件ID
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_YueBiao_Constants.JT808_0X0200_0xF1;
        /// <summary>
        /// 算法异常信息长度
        /// </summary>
        public byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 厂家自定义
        /// </summary>
        public uint Retain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0xF1 value = new JT808_0x0200_0xF1();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.Retain = reader.ReadUInt32();
            writer.WriteNumber($"[{value.Retain.ReadNumber()}]厂家自定义", value.Retain);
         }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0xF1 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0xF1 value = new JT808_0x0200_0xF1();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Retain = reader.ReadUInt32();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0xF1 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.Retain);
        }
    }
}
