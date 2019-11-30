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
    /// 设置多边形区域
    /// 0x8604
    /// </summary>
    public class JT808_0x8604 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8604>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8604;
        /// <summary>
        /// 区域 ID
        /// </summary>
        public uint AreaId { get; set; }
        /// <summary>
        /// 区域属性
        /// <see cref="JT808.Protocol.Enums.JT808SettingProperty"/>
        /// </summary>
        public ushort AreaProperty { get; set; }
        /// <summary>
        /// 起始时间
        /// YY-MM-DD-hh-mm-ss，若区域属性 0 位为 0 则没有该字段
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// YY-MM-DD-hh-mm-ss，若区域属性 0 位为 0 则没有该字段
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 最高速度
        /// Km/h，若区域属性 1 位为 0 则没有该字段
        /// </summary>
        public ushort? HighestSpeed { get; set; }
        /// <summary>
        /// 超速持续时间 
        /// 单位为秒（s）（类似表述，同前修改），若区域属性 1 位为 0 则没有该字段
        /// </summary>
        public byte? OverspeedDuration { get; set; }
        /// <summary>
        /// 区域总顶点数
        /// </summary>
        public ushort PeakCount { get; set; }
        /// <summary>
        /// 顶点项
        /// </summary>
        public List<JT808PeakProperty> PeakItems { get; set; }
        /// <summary>
        /// 夜间最高速度
        /// 2019版本
        /// </summary>
        public ushort NightMaximumSpeed { get; set; }
        /// <summary>
        /// 名称长度
        /// 2019版本
        /// </summary>
        public ushort NameLength { get; set; }
        /// <summary>
        /// 区域名称
        /// 2019版本
        /// </summary>
        public string AreaName { get; set; }

        public JT808_0x8604 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8604 jT808_0X8604 = new JT808_0x8604();
            jT808_0X8604.AreaId = reader.ReadUInt32();
            jT808_0X8604.AreaProperty = reader.ReadUInt16();
            ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(jT808_0X8604.AreaProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                jT808_0X8604.StartTime = reader.ReadDateTime6();
                jT808_0X8604.EndTime = reader.ReadDateTime6();
            }
            bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
            if (!bit1Flag)
            {
                jT808_0X8604.HighestSpeed = reader.ReadUInt16();
                jT808_0X8604.OverspeedDuration = reader.ReadByte();
            }
            jT808_0X8604.PeakCount = reader.ReadUInt16();
            jT808_0X8604.PeakItems = new List<JT808PeakProperty>();
            for (var i = 0; i < jT808_0X8604.PeakCount; i++)
            {
                var item = new JT808PeakProperty();
                item.Lat = reader.ReadUInt32();
                item.Lng = reader.ReadUInt32();
                jT808_0X8604.PeakItems.Add(item);
            }
            if (reader.Version == JT808Version.JTT2019)
            {
                if (!bit1Flag) 
                {
                    jT808_0X8604.NightMaximumSpeed = reader.ReadUInt16();
                }
                jT808_0X8604.NameLength = reader.ReadUInt16();
                jT808_0X8604.AreaName = reader.ReadString(jT808_0X8604.NameLength);
            }
            return jT808_0X8604;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8604 value, IJT808Config config)
        {
            writer.WriteUInt32(value.AreaId);
            writer.WriteUInt16(value.AreaProperty);
            ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(value.AreaProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                if (value.StartTime.HasValue)
                {
                    writer.WriteDateTime6(value.StartTime.Value);
                }
                if (value.EndTime.HasValue)
                {
                    writer.WriteDateTime6(value.EndTime.Value);
                }
            }
            bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
            if (!bit1Flag)
            {
                if (value.HighestSpeed.HasValue)
                {
                    writer.WriteUInt16(value.HighestSpeed.Value);
                }
                if (value.OverspeedDuration.HasValue)
                {
                    writer.WriteByte(value.OverspeedDuration.Value);
                }
            }
            if (value.PeakItems != null && value.PeakItems.Count > 0)
            {
                writer.WriteUInt16((ushort)value.PeakItems.Count);
                foreach (var item in value.PeakItems)
                {
                    writer.WriteUInt32(item.Lat);
                    writer.WriteUInt32(item.Lng);
                }
            }
            if (writer.Version == JT808Version.JTT2019)
            {
                if (!bit1Flag)
                {
                    writer.WriteUInt16(value.NightMaximumSpeed);
                }
                writer.Skip(2, out int AreaNameLengthPosition);
                writer.WriteString(value.AreaName);
                writer.WriteUInt16Return((ushort)(writer.GetCurrentPosition() - AreaNameLengthPosition - 2), AreaNameLengthPosition);
            }
        }
    }
}
