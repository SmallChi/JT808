using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Metadata;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 设置矩形区域
    /// 0x8602
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8602_Formatter))]
    public class JT808_0x8602 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8602>
    {
        /// <summary>
        /// 设置属性
        /// <see cref="JT808.Protocol.Enums.JT808SettingProperty"/>
        /// </summary>
        public byte SettingAreaProperty { get; set; }
        /// <summary>
        /// 区域总数
        /// </summary>
        public byte AreaCount { get; set; }
        /// <summary>
        /// 区域项
        /// </summary>
        public List<JT808RectangleAreaProperty> AreaItems { get; set; }

        public JT808_0x8602 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8602 jT808_0X8602 = new JT808_0x8602();
            jT808_0X8602.SettingAreaProperty = reader.ReadByte();
            jT808_0X8602.AreaCount = reader.ReadByte();
            jT808_0X8602.AreaItems = new List<JT808RectangleAreaProperty>();
            for (var i = 0; i < jT808_0X8602.AreaCount; i++)
            {
                JT808RectangleAreaProperty jT808CircleAreaProperty = new JT808RectangleAreaProperty();
                jT808CircleAreaProperty.AreaId = reader.ReadUInt32();
                jT808CircleAreaProperty.AreaProperty = reader.ReadUInt16();
                jT808CircleAreaProperty.UpLeftPointLat = reader.ReadUInt32();
                jT808CircleAreaProperty.UpLeftPointLng = reader.ReadUInt32();
                jT808CircleAreaProperty.LowRightPointLat = reader.ReadUInt32();
                jT808CircleAreaProperty.LowRightPointLng = reader.ReadUInt32();
                ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(jT808CircleAreaProperty.AreaProperty, 2).PadLeft(16, '0').AsSpan();
                bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
                if (!bit0Flag)
                {
                    jT808CircleAreaProperty.StartTime = reader.ReadDateTime6();
                    jT808CircleAreaProperty.EndTime = reader.ReadDateTime6();
                }
                bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
                if (!bit1Flag)
                {
                    jT808CircleAreaProperty.HighestSpeed = reader.ReadUInt16();
                    jT808CircleAreaProperty.OverspeedDuration = reader.ReadByte();
                }
                jT808_0X8602.AreaItems.Add(jT808CircleAreaProperty);
            }
            return jT808_0X8602;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8602 value, IJT808Config config)
        {
            writer.WriteByte(value.SettingAreaProperty);
            if (value.AreaItems != null)
            {
                writer.WriteByte((byte)value.AreaItems.Count);
                foreach (var item in value.AreaItems)
                {
                    writer.WriteUInt32(item.AreaId);
                    writer.WriteUInt16(item.AreaProperty);
                    writer.WriteUInt32(item.UpLeftPointLat);
                    writer.WriteUInt32(item.UpLeftPointLng);
                    writer.WriteUInt32(item.LowRightPointLat);
                    writer.WriteUInt32(item.LowRightPointLng);
                    ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(item.AreaProperty, 2).PadLeft(16, '0').AsSpan();
                    bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
                    if (!bit0Flag)
                    {
                        if (item.StartTime.HasValue)
                        {
                            writer.WriteDateTime6(item.StartTime.Value);
                        }
                        if (item.EndTime.HasValue)
                        {
                            writer.WriteDateTime6(item.EndTime.Value);
                        }
                    }
                    bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
                    if (!bit1Flag)
                    {
                        if (item.HighestSpeed.HasValue)
                        {
                            writer.WriteUInt16(item.HighestSpeed.Value);
                        }
                        if (item.OverspeedDuration.HasValue)
                        {
                            writer.WriteByte(item.OverspeedDuration.Value);
                        }
                    }
                }
            }
        }
    }
}
