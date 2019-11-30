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
    /// 设置圆形区域
    /// 0x8600
    /// 注：本条消息协议支持周期时间范围，如要限制每天的8:30-18:00，起始/结束时间设为：00-00-00-08-30-00/00-00-00-18-00-00，其他以此类推
    /// </summary>
    public class JT808_0x8600 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8600>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8600;
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
        public List<JT808CircleAreaProperty> AreaItems { get; set; }

        public JT808_0x8600 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8600 jT808_0X8600 = new JT808_0x8600();
            jT808_0X8600.SettingAreaProperty = reader.ReadByte();
            jT808_0X8600.AreaCount = reader.ReadByte();
            jT808_0X8600.AreaItems = new List<JT808CircleAreaProperty>();
            for (var i = 0; i < jT808_0X8600.AreaCount; i++)
            {
                JT808CircleAreaProperty jT808CircleAreaProperty = new JT808CircleAreaProperty();
                jT808CircleAreaProperty.AreaId = reader.ReadUInt32();
                jT808CircleAreaProperty.AreaProperty = reader.ReadUInt16();
                jT808CircleAreaProperty.CenterPointLat = reader.ReadUInt32();
                jT808CircleAreaProperty.CenterPointLng = reader.ReadUInt32();
                jT808CircleAreaProperty.Radius = reader.ReadUInt32();
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
                    if (reader.Version == JT808Version.JTT2019)
                    {
                        jT808CircleAreaProperty.NightMaximumSpeed = reader.ReadUInt16();
                    }
                }
                if(reader.Version== JT808Version.JTT2019)
                {
                    jT808CircleAreaProperty.NameLength = reader.ReadUInt16();
                    jT808CircleAreaProperty.AreaName = reader.ReadString(jT808CircleAreaProperty.NameLength);
                }
                jT808_0X8600.AreaItems.Add(jT808CircleAreaProperty);
            }
            return jT808_0X8600;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8600 value, IJT808Config config)
        {
            writer.WriteByte(value.SettingAreaProperty);
            if (value.AreaItems != null)
            {
                writer.WriteByte((byte)value.AreaItems.Count);
                foreach (var item in value.AreaItems)
                {
                    writer.WriteUInt32(item.AreaId);
                    writer.WriteUInt16(item.AreaProperty);
                    writer.WriteUInt32(item.CenterPointLat);
                    writer.WriteUInt32(item.CenterPointLng);
                    writer.WriteUInt32(item.Radius);
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
                        writer.WriteUInt16Return((ushort)(writer.GetCurrentPosition()- AreaNameLengthPosition-2), AreaNameLengthPosition);
                    }
                }
            }
        }
    }
}
