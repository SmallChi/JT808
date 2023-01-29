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
    /// 设置多边形区域
    /// 0x8604
    /// </summary>
    public class JT808_0x8604 : JT808MessagePackFormatter<JT808_0x8604>, JT808Bodies,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8604
        /// </summary>
        public ushort MsgId => 0x8604;
        /// <summary>
        /// 设置多边形区域
        /// </summary>
        public string Description => "设置多边形区域";
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8604 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8604 jT808_0X8604 = new JT808_0x8604();
            jT808_0X8604.AreaId = reader.ReadUInt32();
            jT808_0X8604.AreaProperty = reader.ReadUInt16();
            ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(jT808_0X8604.AreaProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                jT808_0X8604.StartTime = reader.ReadDateTime_yyMMddHHmmss();
                jT808_0X8604.EndTime = reader.ReadDateTime_yyMMddHHmmss();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8604 value, IJT808Config config)
        {
            writer.WriteUInt32(value.AreaId);
            writer.WriteUInt16(value.AreaProperty);
            ReadOnlySpan<char> areaProperty16Bit = Convert.ToString(value.AreaProperty, 2).PadLeft(16, '0').AsSpan();
            bool bit0Flag = areaProperty16Bit.Slice(areaProperty16Bit.Length - 1).ToString().Equals("0");
            if (!bit0Flag)
            {
                if (value.StartTime.HasValue)
                {
                    writer.WriteDateTime_yyMMddHHmmss(value.StartTime.Value);
                }
                if (value.EndTime.HasValue)
                {
                    writer.WriteDateTime_yyMMddHHmmss(value.EndTime.Value);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8604 value = new JT808_0x8604();
            value.AreaId = reader.ReadUInt32();
            writer.WriteNumber($"[{ value.AreaId.ReadNumber()}]区域ID", value.AreaId);
            value.AreaProperty = reader.ReadUInt16();
            writer.WriteNumber($"[{ value.AreaProperty.ReadNumber()}]区域属性", value.AreaProperty);
            ReadOnlySpan<char> areaPropertyBits =string.Join("", Convert.ToString(value.AreaProperty, 2).PadLeft(16, '0').Reverse()).AsSpan();
            writer.WriteStartObject($"区域属性对象[{areaPropertyBits.ToString()}]");
            if (reader.Version == JT808Version.JTT2019)
            {
                writer.WriteString($"[bit15]{areaPropertyBits[15]}", areaPropertyBits[15] == '0' ? "进区域不采集GNSS详细定位数据" : "进区域采集GNSS详细定位数据");
                writer.WriteString($"[bit14]{areaPropertyBits[14]}", areaPropertyBits[14] == '0' ? "进区域开启通信模块" : "进区域关闭通信模块");
                writer.WriteString("[bit9~bit13]保留", areaPropertyBits.Slice(9, 5));
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
            bool bit0Flag = areaPropertyBits.Slice(0,1).ToString().Equals("0");
            if (!bit0Flag)
            {
                value.StartTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{ value.StartTime.Value.ToString("yyMMddHHmmss")}]起始时间", value.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                value.EndTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{ value.EndTime.Value.ToString("yyMMddHHmmss")}]结束时间", value.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            bool bit1Flag = areaPropertyBits.Slice(1, 1).ToString().Equals("0");
            if (!bit1Flag)
            {
                value.HighestSpeed = reader.ReadUInt16();
                writer.WriteNumber($"[{ value.HighestSpeed.Value.ReadNumber()}]最高速度", value.HighestSpeed.Value);
                value.OverspeedDuration = reader.ReadByte();
                writer.WriteNumber($"[{ value.OverspeedDuration.Value.ReadNumber()}]超速持续时间", value.OverspeedDuration.Value);
            }
            value.PeakCount = reader.ReadUInt16();
            writer.WriteNumber($"[{ value.PeakCount.ReadNumber()}]区域总顶点数", value.PeakCount);
            writer.WriteStartArray("区域顶");
            for (var i = 0; i < value.PeakCount; i++)
            {
                writer.WriteStartObject();
                var item = new JT808PeakProperty();
                item.Lat = reader.ReadUInt32();
                writer.WriteNumber($"[{item.Lat.ReadNumber()}]顶点纬度", item.Lat);
                item.Lng = reader.ReadUInt32();
                writer.WriteNumber($"[{item.Lng.ReadNumber()}]顶点经度", item.Lng);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            if (reader.Version == JT808Version.JTT2019)
            {
                if (!bit1Flag)
                {
                    value.NightMaximumSpeed = reader.ReadUInt16();
                    writer.WriteNumber($"[{value.NightMaximumSpeed .ReadNumber()}]夜间最高速度", value.NightMaximumSpeed);
                }
                value.NameLength = reader.ReadUInt16();
                writer.WriteNumber($"[{value.NameLength.ReadNumber()}]名称长度", value.NameLength);
                var nameBuffer = reader.ReadVirtualArray(value.NameLength);
                value.AreaName = reader.ReadString(value.NameLength);
                writer.WriteString($"[{ nameBuffer.ToArray().ToHexString()}]区域名称", value.AreaName);
            }
        }
    }
}
