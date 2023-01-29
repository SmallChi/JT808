using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Metadata;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 信息点播菜单设置
    /// 0x8303
    /// </summary>
    public class JT808_0x8303 : JT808MessagePackFormatter<JT808_0x8303>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8303
        /// </summary>
        public ushort MsgId =>0x8303;
        /// <summary>
        /// 信息点播菜单设置
        /// </summary>
        public string Description => "信息点播菜单设置";
        /// <summary>
        /// 设置类型
        /// <see cref="JT808.Protocol.Enums.JT808InformationSettingType"/>
        /// </summary>
        public byte SettingType { get; set; }
        /// <summary>
        /// 信息项总数
        /// </summary>
        public byte InformationItemCount { get; set; }
        /// <summary>
        /// 信息点播信息项组成数据
        /// 信息项列表
        /// </summary>
        public List<JT808InformationItemProperty> InformationItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8303 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8303 jT808_0X8303 = new JT808_0x8303();
            jT808_0X8303.SettingType = reader.ReadByte();
            jT808_0X8303.InformationItemCount = reader.ReadByte();
            jT808_0X8303.InformationItems = new List<JT808InformationItemProperty>();
            for (var i = 0; i < jT808_0X8303.InformationItemCount; i++)
            {
                JT808InformationItemProperty jT808InformationItemProperty = new JT808InformationItemProperty();
                jT808InformationItemProperty.InformationType = reader.ReadByte();
                jT808InformationItemProperty.InformationLength = reader.ReadUInt16();
                jT808InformationItemProperty.InformationName = reader.ReadString(jT808InformationItemProperty.InformationLength);
                jT808_0X8303.InformationItems.Add(jT808InformationItemProperty);
            }
            return jT808_0X8303;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8303 value, IJT808Config config)
        {
            writer.WriteByte(value.SettingType);
            writer.WriteByte((byte)value.InformationItems.Count);
            foreach (var item in value.InformationItems)
            {
                writer.WriteByte(item.InformationType);
                // 先计算内容长度（汉字为两个字节）
                writer.Skip(2, out int position);
                writer.WriteString(item.InformationName);
                ushort length = (ushort)(writer.GetCurrentPosition() - position - 2);
                writer.WriteUInt16Return(length, position);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8303 value = new JT808_0x8303();
            value.SettingType = reader.ReadByte();
            writer.WriteNumber($"[{value.SettingType.ReadNumber()}]设置类型", value.SettingType);
            value.InformationItemCount = reader.ReadByte();
            writer.WriteNumber($"[{value.InformationItemCount.ReadNumber()}]信息项总数", value.InformationItemCount);
            writer.WriteStartArray("信息项列表");
            for (var i = 0; i < value.InformationItemCount; i++)
            {
                writer.WriteStartObject();
                JT808InformationItemProperty jT808InformationItemProperty = new JT808InformationItemProperty();
                jT808InformationItemProperty.InformationType = reader.ReadByte();
                writer.WriteNumber($"[{jT808InformationItemProperty.InformationType.ReadNumber()}]信息类型", jT808InformationItemProperty.InformationType);
                jT808InformationItemProperty.InformationLength = reader.ReadUInt16();
                writer.WriteNumber($"[{jT808InformationItemProperty.InformationLength.ReadNumber()}]信息名称长度", jT808InformationItemProperty.InformationLength);
                var infoBuffer = reader.ReadVirtualArray(jT808InformationItemProperty.InformationLength).ToArray();
                jT808InformationItemProperty.InformationName = reader.ReadString(jT808InformationItemProperty.InformationLength);
                writer.WriteString($"[{infoBuffer.ToHexString()}]信息名称", jT808InformationItemProperty.InformationName);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
