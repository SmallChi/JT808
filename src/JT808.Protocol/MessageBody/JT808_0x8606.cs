using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 设置路线
    /// 0x8606
    /// </summary>
    public class JT808_0x8606 : JT808MessagePackFormatter<JT808_0x8606>, JT808Bodies,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8606
        /// </summary>
        public ushort MsgId => 0x8606;
        /// <summary>
        /// 设置路线
        /// </summary>
        public string Description => "设置路线";
        /// <summary>
        /// 路线 ID
        /// </summary>
        public uint RouteId { get; set; }
        /// <summary>
        /// 路线属性
        /// 路线属性数据格式见 表 67
        /// </summary>
        public ushort RouteProperty { get; set; }
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
        /// 路线总拐点数
        /// </summary>
        public ushort InflectionPointCount { get; set; }
        /// <summary>
        /// 拐点项
        /// </summary>
        public List<JT808InflectionPointProperty> InflectionPointItems { get; set; }
        /// <summary>
        /// 名称长度
        /// </summary>
        public ushort RouteNameLength { get; set; }
        /// <summary>
        /// 路线名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8606 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8606 jT808_0X8606 = new JT808_0x8606();
            jT808_0X8606.RouteId = reader.ReadUInt32();
            jT808_0X8606.RouteProperty = reader.ReadUInt16();
            ReadOnlySpan<char> routeProperty16Bit = Convert.ToString(jT808_0X8606.RouteProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = routeProperty16Bit.Slice(routeProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                jT808_0X8606.StartTime = reader.ReadDateTime_yyMMddHHmmss();
                jT808_0X8606.EndTime = reader.ReadDateTime_yyMMddHHmmss();
            }
            jT808_0X8606.InflectionPointCount = reader.ReadUInt16();
            jT808_0X8606.InflectionPointItems = new List<JT808InflectionPointProperty>();
            for (var i = 0; i < jT808_0X8606.InflectionPointCount; i++)
            {
                JT808InflectionPointProperty jT808InflectionPointProperty = new JT808InflectionPointProperty();
                jT808InflectionPointProperty.InflectionPointId = reader.ReadUInt32();
                jT808InflectionPointProperty.SectionId = reader.ReadUInt32();
                jT808InflectionPointProperty.InflectionPointLat = reader.ReadUInt32();
                jT808InflectionPointProperty.InflectionPointLng = reader.ReadUInt32();
                jT808InflectionPointProperty.SectionWidth = reader.ReadByte();
                jT808InflectionPointProperty.SectionProperty = reader.ReadByte();
                ReadOnlySpan<char> sectionProperty16Bit = Convert.ToString(jT808InflectionPointProperty.SectionProperty, 2).PadLeft(16, '0').AsSpan();
                bool sectionBit0Flag = sectionProperty16Bit.Slice(sectionProperty16Bit.Length - 1).ToString().Equals("0");
                if (!sectionBit0Flag)
                {
                    jT808InflectionPointProperty.SectionLongDrivingThreshold = reader.ReadUInt16();
                    jT808InflectionPointProperty.SectionDrivingUnderThreshold = reader.ReadUInt16();
                }
                bool sectionBit1Flag = sectionProperty16Bit.Slice(sectionProperty16Bit.Length - 2, 1).ToString().Equals("0");
                if (!sectionBit1Flag)
                {
                    jT808InflectionPointProperty.SectionHighestSpeed = reader.ReadUInt16();
                    jT808InflectionPointProperty.SectionOverspeedDuration = reader.ReadByte();
                    if (reader.Version == JT808Version.JTT2019)
                        jT808InflectionPointProperty.NightMaximumSpeed = reader.ReadUInt16();
                }
                jT808_0X8606.InflectionPointItems.Add(jT808InflectionPointProperty);
            }
            if (reader.Version == JT808Version.JTT2019)
            {
                jT808_0X8606.RouteNameLength = reader.ReadUInt16();
                jT808_0X8606.RouteName = reader.ReadString(jT808_0X8606.RouteNameLength);
            }
            return jT808_0X8606;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8606 value, IJT808Config config)
        {
            writer.WriteUInt32(value.RouteId);
            writer.WriteUInt16(value.RouteProperty);
            ReadOnlySpan<char> routeProperty16Bit = Convert.ToString(value.RouteProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = routeProperty16Bit.Slice(routeProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                if (value.StartTime.HasValue)
                    writer.WriteDateTime_yyMMddHHmmss(value.StartTime.Value);

                if (value.EndTime.HasValue)
                    writer.WriteDateTime_yyMMddHHmmss(value.EndTime.Value);
            }
            //bool bit1Flag = routeProperty16Bit.Slice(routeProperty16Bit.Length - 2, 1).ToString().Equals("0");
            if (value.InflectionPointItems != null && value.InflectionPointItems.Count > 0)
            {
                writer.WriteUInt16((ushort)value.InflectionPointItems.Count);
                foreach (var item in value.InflectionPointItems)
                {
                    writer.WriteUInt32(item.InflectionPointId);
                    writer.WriteUInt32(item.SectionId);
                    writer.WriteUInt32(item.InflectionPointLat);
                    writer.WriteUInt32(item.InflectionPointLng);
                    writer.WriteByte(item.SectionWidth);
                    writer.WriteByte(item.SectionProperty);

                    ReadOnlySpan<char> sectionProperty16Bit = Convert.ToString(item.SectionProperty, 2).PadLeft(16, '0').AsSpan();
                    bool sectionBit0Flag = sectionProperty16Bit.Slice(sectionProperty16Bit.Length - 1).ToString().Equals("0");
                    if (!sectionBit0Flag)
                    {
                        if (item.SectionLongDrivingThreshold.HasValue)
                            writer.WriteUInt16(item.SectionLongDrivingThreshold.Value);
                        if (item.SectionDrivingUnderThreshold.HasValue)
                            writer.WriteUInt16(item.SectionDrivingUnderThreshold.Value);
                    }
                    bool sectionBit1Flag = sectionProperty16Bit.Slice(sectionProperty16Bit.Length - 2, 1).ToString().Equals("0");
                    if (!sectionBit1Flag)
                    {
                        if (item.SectionHighestSpeed.HasValue)
                            writer.WriteUInt16(item.SectionHighestSpeed.Value);
                        if (item.SectionOverspeedDuration.HasValue)
                            writer.WriteByte(item.SectionOverspeedDuration.Value);
                        if (writer.Version == JT808Version.JTT2019)
                        {
                            if (item.NightMaximumSpeed.HasValue)
                                writer.WriteUInt16(item.NightMaximumSpeed.Value);
                        }
                    }
                }
            }
            if (writer.Version == JT808Version.JTT2019)
            {
                writer.Skip(2, out int RouteNameLengthPosition);
                writer.WriteString(value.RouteName);
                writer.WriteUInt16Return((ushort)(writer.GetCurrentPosition() - RouteNameLengthPosition - 2), RouteNameLengthPosition);
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
            JT808_0x8606 value = new JT808_0x8606();
            value.RouteId = reader.ReadUInt32();
            writer.WriteNumber($"[{ value.RouteId.ReadNumber()}]路线ID", value.RouteId);
            value.RouteProperty = reader.ReadUInt16();
            writer.WriteNumber($"[{ value.RouteProperty.ReadNumber()}]路线属性", value.RouteProperty);
            ReadOnlySpan<char> routeProperty16Bit =string.Join("", Convert.ToString(value.RouteProperty, 2).PadLeft(16, '0').Reverse()).AsSpan();
            writer.WriteStartObject($"路线属性对象[{routeProperty16Bit.ToString()}]");
            if (reader.Version == JT808Version.JTT2019)
            {
                writer.WriteString($"[bit6~bit15]保留", routeProperty16Bit.Slice(6,10).ToString());
                writer.WriteString($"[bit5]出路线是否报警给平台-{routeProperty16Bit[5]}", routeProperty16Bit[5] == '0' ? "否" : "是");
                writer.WriteString($"[bit4]出路线是否报警给平驾驶员-{routeProperty16Bit[4]}", routeProperty16Bit[4] == '0' ? "否" : "是");
                writer.WriteString($"[bit3]进路线是否报警给平台-{routeProperty16Bit[3]}", routeProperty16Bit[3] == '0' ? "否" : "是");
                writer.WriteString($"[bit2]进路线是否报警给驾驶员-{routeProperty16Bit[2]}", routeProperty16Bit[2] == '0' ? "否" : "是");
                writer.WriteString($"[bit1]保留", routeProperty16Bit[1] == '0' ? "否" : "是");
                writer.WriteString($"[bit0]是否启用起始时间与结束时间的判断规则-{routeProperty16Bit[0]}", routeProperty16Bit[0] == '0' ? "否" : "是");
            }
            else
            {
                writer.WriteString($"[bit6~bit15]保留", routeProperty16Bit.Slice(6, 10).ToString());
                writer.WriteString($"[bit5]{routeProperty16Bit[5]}", routeProperty16Bit[5] == '1' ? "出路线报警给平台" : "无");
                writer.WriteString($"[bit4]{routeProperty16Bit[4]}", routeProperty16Bit[4] == '1' ? "出路线报警给驾驶员" : "无");
                writer.WriteString($"[bit3]{routeProperty16Bit[3]}", routeProperty16Bit[3] == '1' ? "进路线报警给平台" : "无");
                writer.WriteString($"[bit2]{routeProperty16Bit[2]}", routeProperty16Bit[2] == '1' ? "进路线报警给驾驶员" : "无");
                writer.WriteString($"[bit1]保留", $"{routeProperty16Bit[1]}");
                writer.WriteString($"[bit0]{routeProperty16Bit[0]}", routeProperty16Bit[0] == '1' ? "根据时间" : "无");
            }
            writer.WriteEndObject();
            bool bit0Flag = routeProperty16Bit.Slice(0,1).ToString().Equals("0");
            if (!bit0Flag)
            {
                value.StartTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{ value.StartTime.Value.ToString("yyMMddHHmmss")}]起始时间", value.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                value.EndTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{ value.EndTime.Value.ToString("yyMMddHHmmss")}]结束时间", value.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            value.InflectionPointCount = reader.ReadUInt16();
            writer.WriteNumber($"[{ value.InflectionPointCount.ReadNumber()}]路线总拐点数", value.InflectionPointCount);
            writer.WriteStartArray("拐点项");
            for (var i = 0; i < value.InflectionPointCount; i++)
            {
                writer.WriteStartObject();
                JT808InflectionPointProperty jT808InflectionPointProperty = new JT808InflectionPointProperty();
                jT808InflectionPointProperty.InflectionPointId = reader.ReadUInt32();
                writer.WriteNumber($"[{ jT808InflectionPointProperty.InflectionPointId.ReadNumber()}]拐点ID", jT808InflectionPointProperty.InflectionPointId);
                jT808InflectionPointProperty.SectionId = reader.ReadUInt32();
                writer.WriteNumber($"[{ jT808InflectionPointProperty.SectionId.ReadNumber()}]路段ID", jT808InflectionPointProperty.SectionId);
                jT808InflectionPointProperty.InflectionPointLat = reader.ReadUInt32();
                writer.WriteNumber($"[{ jT808InflectionPointProperty.InflectionPointLat.ReadNumber()}]拐点纬度", jT808InflectionPointProperty.InflectionPointLat);
                jT808InflectionPointProperty.InflectionPointLng = reader.ReadUInt32();
                writer.WriteNumber($"[{ jT808InflectionPointProperty.InflectionPointLng.ReadNumber()}]拐点经度", jT808InflectionPointProperty.InflectionPointLng);
                jT808InflectionPointProperty.SectionWidth = reader.ReadByte();
                writer.WriteNumber($"[{ jT808InflectionPointProperty.SectionWidth.ReadNumber()}]路段宽度", jT808InflectionPointProperty.SectionWidth);
                jT808InflectionPointProperty.SectionProperty = reader.ReadByte();
                writer.WriteNumber($"[{ jT808InflectionPointProperty.SectionProperty.ReadNumber()}]路段属性", jT808InflectionPointProperty.SectionProperty);
                ReadOnlySpan<char> sectionProperty8Bit =string.Join("", Convert.ToString(jT808InflectionPointProperty.SectionProperty, 2).PadLeft(8, '0').Reverse()).AsSpan();
                writer.WriteStartObject($"路段属性对象[{sectionProperty8Bit.ToString()}]");
                writer.WriteString($"[bit4~bit7]保留", sectionProperty8Bit.Slice(4, 4).ToString());
                writer.WriteString($"[bit3]进路线是否报警给平台-{sectionProperty8Bit[3]}", sectionProperty8Bit[3] == '0' ? "无" : "限速");
                writer.WriteString($"[bit2]进路线是否报警给驾驶员-{sectionProperty8Bit[2]}", sectionProperty8Bit[2] == '0' ? "北纬" : "南纬");
                writer.WriteString($"[bit1]{sectionProperty8Bit[1]}", sectionProperty8Bit[1] == '0' ? "东经" : "西经");
                writer.WriteString($"[bit0]{sectionProperty8Bit[0]}", sectionProperty8Bit[0] == '0' ? "无" : "行驶时间");
                writer.WriteEndObject();
                bool sectionBit0Flag = sectionProperty8Bit.Slice(0,1).ToString().Equals("0");
                if (!sectionBit0Flag)
                {
                    jT808InflectionPointProperty.SectionLongDrivingThreshold = reader.ReadUInt16();
                    writer.WriteNumber($"[{ jT808InflectionPointProperty.SectionLongDrivingThreshold.Value.ReadNumber()}]路段行驶过长阈值", jT808InflectionPointProperty.SectionLongDrivingThreshold.Value) ;
                    jT808InflectionPointProperty.SectionDrivingUnderThreshold = reader.ReadUInt16();
                    writer.WriteNumber($"[{ jT808InflectionPointProperty.SectionDrivingUnderThreshold.Value.ReadNumber()}]路段行驶不足阈值", jT808InflectionPointProperty.SectionDrivingUnderThreshold.Value);
                }
                bool sectionBit1Flag = sectionProperty8Bit.Slice(1, 1).ToString().Equals("0");
                if (!sectionBit1Flag)
                {
                    jT808InflectionPointProperty.SectionHighestSpeed = reader.ReadUInt16();
                    writer.WriteNumber($"[{ jT808InflectionPointProperty.SectionHighestSpeed.Value.ReadNumber()}]路段最高速度", jT808InflectionPointProperty.SectionHighestSpeed.Value);
                    jT808InflectionPointProperty.SectionOverspeedDuration = reader.ReadByte();
                    writer.WriteNumber($"[{ jT808InflectionPointProperty.SectionOverspeedDuration.Value.ReadNumber()}]超速持续时间", jT808InflectionPointProperty.SectionOverspeedDuration.Value);
                    if (reader.Version == JT808Version.JTT2019)
                    {
                        jT808InflectionPointProperty.NightMaximumSpeed = reader.ReadUInt16();
                        writer.WriteNumber($"[{ jT808InflectionPointProperty.NightMaximumSpeed.Value.ReadNumber()}]夜间最高速度", jT808InflectionPointProperty.NightMaximumSpeed.Value);
                    }
                }
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            if (reader.Version == JT808Version.JTT2019)
            {
                value.RouteNameLength = reader.ReadUInt16();
                writer.WriteNumber($"[{ value.RouteNameLength.ReadNumber()}]名称长度", value.RouteNameLength);
                var routeNameBuffer = reader.ReadVirtualArray(value.RouteNameLength);
                value.RouteName = reader.ReadString(value.RouteNameLength);
                writer.WriteString($"[{ routeNameBuffer.ToArray().ToHexString()}]路线名称", value.RouteName);
            }
        }
    }
}
