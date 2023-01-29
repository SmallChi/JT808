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
    /// 设置圆形区域
    /// 0x8600
    /// 注：本条消息协议支持周期时间范围，如要限制每天的8:30-18:00，起始/结束时间设为：00-00-00-08-30-00/00-00-00-18-00-00，其他以此类推
    /// </summary>
    public class JT808_0x8600 : JT808MessagePackFormatter<JT808_0x8600>, JT808Bodies,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8600
        /// </summary>
        public ushort MsgId => 0x8600;
        /// <summary>
        /// 设置圆形区域
        /// </summary>
        public string Description => "设置圆形区域";
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8600 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
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
                    jT808CircleAreaProperty.StartTime = reader.ReadDateTime_yyMMddHHmmss();
                    jT808CircleAreaProperty.EndTime = reader.ReadDateTime_yyMMddHHmmss();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8600 value, IJT808Config config)
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
                            writer.WriteDateTime_yyMMddHHmmss(item.StartTime.Value);
                        }
                        if (item.EndTime.HasValue)
                        {
                            writer.WriteDateTime_yyMMddHHmmss(item.EndTime.Value);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8600 value = new JT808_0x8600();
            value.SettingAreaProperty = reader.ReadByte();
            JT808SettingProperty jT808SettingProperty = (JT808SettingProperty)value.SettingAreaProperty;
            writer.WriteNumber($"[{ value.SettingAreaProperty.ReadNumber()}]设置属性-{jT808SettingProperty.ToString()}", value.SettingAreaProperty);
            value.AreaCount = reader.ReadByte();
            writer.WriteNumber($"[{ value.AreaCount.ReadNumber()}]区域总数", value.AreaCount);
            writer.WriteStartArray("区域项");
            for (var i = 0; i < value.AreaCount; i++)
            {
                writer.WriteStartObject();
                JT808CircleAreaProperty jT808CircleAreaProperty = new JT808CircleAreaProperty();
                jT808CircleAreaProperty.AreaId = reader.ReadUInt32();
                writer.WriteNumber($"[{ jT808CircleAreaProperty.AreaId.ReadNumber()}]区域ID", jT808CircleAreaProperty.AreaId);
                jT808CircleAreaProperty.AreaProperty = reader.ReadUInt16();
                writer.WriteNumber($"[{ jT808CircleAreaProperty.AreaProperty.ReadNumber()}]区域属性", jT808CircleAreaProperty.AreaProperty);
                ReadOnlySpan<char> areaPropertyBits =string.Join("", Convert.ToString(jT808CircleAreaProperty.AreaProperty, 2).PadLeft(16, '0').Reverse()).AsSpan();
                writer.WriteStartObject($"区域属性对象[{areaPropertyBits.ToString()}]");
                if (reader.Version == JT808Version.JTT2019)
                {
                    writer.WriteString($"[bit15]{areaPropertyBits[15]}", areaPropertyBits[15]=='0'?"进区域不采集GNSS详细定位数据":"进区域采集GNSS详细定位数据");
                    writer.WriteString($"[bit14]{areaPropertyBits[14]}", areaPropertyBits[14]=='0'?"进区域开启通信模块":"进区域关闭通信模块");
                    writer.WriteString("[bit9~bit13]保留", areaPropertyBits.Slice(9,5));
                    writer.WriteString($"[bit8]{areaPropertyBits[8]}", areaPropertyBits[8] == '0' ? "允许开门" : "禁止开门");
                    writer.WriteString($"[bit7]{areaPropertyBits[7]}", areaPropertyBits[7] == '0' ? "东经" : "西经");
                    writer.WriteString($"[bit6]{areaPropertyBits[6]}", areaPropertyBits[6] == '0' ? "北纬" : "南纬");
                    writer.WriteString($"[bit5]出区域是否报警给平台-{areaPropertyBits[5]}", areaPropertyBits[5] == '0' ? "否" : "是");
                    writer.WriteString($"[bit4]出区域是否报警给平驾驶员-{areaPropertyBits[4]}", areaPropertyBits[4] == '0' ? "否" : "是");
                    writer.WriteString($"[bit3]进区域是否报警给平台-{areaPropertyBits[3]}", areaPropertyBits[3] == '0' ? "否" : "是");
                    writer.WriteString($"[bit2]进区域是否报警给驾驶员-{areaPropertyBits[2]}", areaPropertyBits[2] == '0' ? "否" : "是");
                    writer.WriteString($"[bit1]是否启用最高速度、超速持续时间和夜间最高速度的判断规则-{areaPropertyBits[1]}", areaPropertyBits[1] == '0' ? "否" : "是");
                    writer.WriteString($"[bit0]是否启用起始时间与结束时间的判断规则-{areaPropertyBits[0]}", areaPropertyBits[0] == '0' ? "否" : "是");
                }
                else
                {
                    writer.WriteString($"[bit15]{areaPropertyBits[15]}", areaPropertyBits[15] == '0' ? "进区域不采集GNSS详细定位数据" : "进区域采集GNSS详细定位数据");
                    writer.WriteString($"[bit14]{areaPropertyBits[14]}", areaPropertyBits[14] == '0' ? "进区域开启通信模块" : "进区域关闭通信模块");
                    writer.WriteString("[bit9~bit13]保留", areaPropertyBits.Slice(9, 5));
                    writer.WriteString($"[bit8]{areaPropertyBits[8]}", areaPropertyBits[8] == '0' ? "允许开门" : "禁止开门");
                    writer.WriteString($"[bit7]{areaPropertyBits[7]}", areaPropertyBits[7] == '0' ? "东经" : "西经");
                    writer.WriteString($"[bit6]{areaPropertyBits[6]}", areaPropertyBits[6] == '0' ? "北纬" : "南纬");
                    writer.WriteString($"[bit5]{areaPropertyBits[5]}", areaPropertyBits[5] == '1' ? "出区域报警给平台" : "无");
                    writer.WriteString($"[bit4]{areaPropertyBits[4]}", areaPropertyBits[4] == '1' ? "出区域报警给驾驶员" : "无");
                    writer.WriteString($"[bit3]{areaPropertyBits[3]}", areaPropertyBits[3] == '1' ? "进区域报警给平台" : "无");
                    writer.WriteString($"[bit2]{areaPropertyBits[2]}", areaPropertyBits[2] == '1' ? "进区域报警给驾驶员" : "无");
                    writer.WriteString($"[bit1]{areaPropertyBits[1]}", areaPropertyBits[1] == '1' ? "限速" : "无");
                    writer.WriteString($"[bit0]{areaPropertyBits[0]}", areaPropertyBits[0] == '1' ? "根据时间" : "无");
                }
                writer.WriteEndObject();
                jT808CircleAreaProperty.CenterPointLat = reader.ReadUInt32();
                writer.WriteNumber($"[{ jT808CircleAreaProperty.CenterPointLat.ReadNumber()}]中心点纬度", jT808CircleAreaProperty.CenterPointLat);
                jT808CircleAreaProperty.CenterPointLng = reader.ReadUInt32();
                writer.WriteNumber($"[{ jT808CircleAreaProperty.CenterPointLng.ReadNumber()}]中心点经度", jT808CircleAreaProperty.CenterPointLng);
                jT808CircleAreaProperty.Radius = reader.ReadUInt32();
                writer.WriteNumber($"[{ jT808CircleAreaProperty.Radius.ReadNumber()}]半径", jT808CircleAreaProperty.Radius);
                ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(jT808CircleAreaProperty.AreaProperty, 2).PadLeft(16, '0').AsSpan();
                bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
                if (!bit0Flag)
                {
                    jT808CircleAreaProperty.StartTime = reader.ReadDateTime_yyMMddHHmmss();
                    writer.WriteString($"[{ jT808CircleAreaProperty.StartTime.Value.ToString("yyMMddHHmmss")}]起始时间", jT808CircleAreaProperty.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    jT808CircleAreaProperty.EndTime = reader.ReadDateTime_yyMMddHHmmss();
                    writer.WriteString($"[{ jT808CircleAreaProperty.EndTime.Value.ToString("yyMMddHHmmss")}]结束时间", jT808CircleAreaProperty.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                bool bit1Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 2, 1).ToString().Equals("0");
                if (!bit1Flag)
                {
                    jT808CircleAreaProperty.HighestSpeed = reader.ReadUInt16();
                    writer.WriteNumber($"[{ jT808CircleAreaProperty.HighestSpeed.Value.ReadNumber()}]最高速度", jT808CircleAreaProperty.HighestSpeed.Value);
                    jT808CircleAreaProperty.OverspeedDuration = reader.ReadByte();
                    writer.WriteNumber($"[{ jT808CircleAreaProperty.OverspeedDuration.Value.ReadNumber()}]超速持续时间", jT808CircleAreaProperty.OverspeedDuration.Value);
                    if (reader.Version == JT808Version.JTT2019)
                    {
                        jT808CircleAreaProperty.NightMaximumSpeed = reader.ReadUInt16();
                        writer.WriteNumber($"[{ jT808CircleAreaProperty.NightMaximumSpeed.ReadNumber()}]夜间最高速度", jT808CircleAreaProperty.NightMaximumSpeed);
                    }
                }
                if (reader.Version == JT808Version.JTT2019)
                {
                    jT808CircleAreaProperty.NameLength = reader.ReadUInt16();
                    writer.WriteNumber($"[{ jT808CircleAreaProperty.NameLength.ReadNumber()}]名称长度", jT808CircleAreaProperty.NameLength);
                    var areaNameBuffer = reader.ReadVirtualArray(jT808CircleAreaProperty.NameLength);
                    jT808CircleAreaProperty.AreaName = reader.ReadString(jT808CircleAreaProperty.NameLength);
                    writer.WriteString($"[{ areaNameBuffer.ToArray().ToHexString()}]区域名称", jT808CircleAreaProperty.AreaName);
                }
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
