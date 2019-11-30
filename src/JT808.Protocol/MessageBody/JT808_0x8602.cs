using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
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
    public class JT808_0x8602 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8602>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8602;
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
                JT808RectangleAreaProperty areaProperty = new JT808RectangleAreaProperty();
                areaProperty.AreaId = reader.ReadUInt32();
                areaProperty.AreaProperty = reader.ReadUInt16();
                areaProperty.UpLeftPointLat = reader.ReadUInt32();
                areaProperty.UpLeftPointLng = reader.ReadUInt32();
                areaProperty.LowRightPointLat = reader.ReadUInt32();
                areaProperty.LowRightPointLng = reader.ReadUInt32();
                ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(areaProperty.AreaProperty, 2).PadLeft(16, '0').AsSpan();
                bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
                if (!bit0Flag)
                {
                    areaProperty.StartTime = reader.ReadDateTime6();
                    areaProperty.EndTime = reader.ReadDateTime6();
                }
                bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
                if (!bit1Flag)
                {
                    areaProperty.HighestSpeed = reader.ReadUInt16();
                    areaProperty.OverspeedDuration = reader.ReadByte();
                    if (reader.Version == JT808Version.JTT2019)
                    {
                        areaProperty.NightMaximumSpeed = reader.ReadUInt16();
                    }
                }
                if (reader.Version == JT808Version.JTT2019)
                {
                    areaProperty.NameLength = reader.ReadUInt16();
                    areaProperty.AreaName = reader.ReadString(areaProperty.NameLength);
                }
                jT808_0X8602.AreaItems.Add(areaProperty);
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
                        if (writer.Version == JT808Version.JTT2019)
                        {
                            writer.WriteUInt16(item.NightMaximumSpeed);
                        }
                    }
                    if (writer.Version == JT808Version.JTT2019)
                    {
                        writer.Skip(2, out int AreaNameLengthPosition);
                        writer.WriteString(item.AreaName);
                        writer.WriteUInt16Return((ushort)(writer.GetCurrentPosition() - AreaNameLengthPosition - 2), AreaNameLengthPosition);
                    }
                }
            }
        }
    }
}
