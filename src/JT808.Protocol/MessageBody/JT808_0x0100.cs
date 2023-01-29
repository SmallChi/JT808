using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;
using System.Buffers;
using System;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端注册
    /// </summary>
    public class JT808_0x0100 : JT808MessagePackFormatter<JT808_0x0100>, JT808Bodies, IJT808_2019_Version,IJT808Analyze
    {
        /// <summary>
        /// 0x0100
        /// </summary>
        public ushort MsgId  => 0x0100;
        /// <summary>
        /// 终端注册
        /// </summary>
        public string Description => "终端注册";
        /// <summary>
        /// 省域 ID
        /// 标示终端安装车辆所在的省域，0 保留，由平台取默
        /// 认值。省域 ID 采用 GB/T 2260 中规定的行政区划代
        /// 码六位中前两位
        /// </summary>
        public ushort AreaID { get; set; }

        /// <summary>
        /// 市县域 ID
        /// 标示终端安装车辆所在的市域和县域，0 保留，由平
        /// 台取默认值。市县域 ID 采用 GB/T 2260 中规定的行
        /// 政区划代码六位中后四位。
        /// </summary>
        public ushort CityOrCountyId { get; set; }

        /// <summary>
        /// 制造商 ID
        /// 2013版本 5 个字节，终端制造商编码
        /// 2019版本 11 个字节，终端制造商编码
        /// </summary>
        public string MakerId { get; set; }

        /// <summary>
        /// 终端型号
        /// 2011版本   8个字节  ，此终端型号由制造商自行定义，位数不足时，后补“0X00”
        /// 2013版本   20 个字节，此终端型号由制造商自行定义，位数不足时，后补“0X00”。
        /// 2019版本   30 个字节，此终端型号由制造商自行定义，位数不足时，后补“0X00”。
        /// </summary>
        public string TerminalModel { get; set; }

        /// <summary>
        /// 终端 ID
        /// 2013版本  7个字节，由大写字母和数字组成，此终端 ID 由制造商自行定义，位数不足时，后补“0X00”。
        /// 2019版本  30个字节，由大写字母和数字组成，此终端 ID 由制造商自行定义，位数不足时，后补“0X00”。
        /// </summary>
        public string TerminalId { get; set; }

        /// <summary>
        /// 车牌颜色
        /// 车牌颜色，按照 JT/T415-2006 的 5.4.12。
        /// 未上牌时，取值为 0。
        /// </summary>
        public byte PlateColor { get; set; }

        /// <summary>
        /// 车辆标识
        /// 车牌颜色为 0 时，表示车辆 VIN；
        /// 否则，表示公安交通管理部门颁发的机动车号牌。
        /// </summary>
        public string PlateNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0100 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0100 jT808_0X0100 = new JT808_0x0100();
            jT808_0X0100.AreaID = reader.ReadUInt16();
            jT808_0X0100.CityOrCountyId = reader.ReadUInt16();
            if (reader.Version == JT808Version.JTT2019)
            {
                jT808_0X0100.MakerId = reader.ReadString(11);
                jT808_0X0100.TerminalModel = reader.ReadString(30);
                jT808_0X0100.TerminalId = reader.ReadString(30);
            }
            else if (reader.Version == JT808Version.JTT2013)
            {
                if (reader.ReadCurrentRemainContentLength() > 33)
                {
                    jT808_0X0100.MakerId = reader.ReadString(5);
                    jT808_0X0100.TerminalModel = reader.ReadString(20);
                    jT808_0X0100.TerminalId = reader.ReadString(7);
                }
                else {
                    jT808_0X0100.MakerId = reader.ReadString(5);
                    jT808_0X0100.TerminalModel = reader.ReadString(8);
                    jT808_0X0100.TerminalId = reader.ReadString(7);
                }
            }
            jT808_0X0100.PlateColor = reader.ReadByte();
            jT808_0X0100.PlateNo = reader.ReadRemainStringContent();
            return jT808_0X0100;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0100 value, IJT808Config config)
        {
            writer.WriteUInt16(value.AreaID);
            writer.WriteUInt16(value.CityOrCountyId);
            if (writer.Version == JT808Version.JTT2019)
            {
                writer.WriteString(value.MakerId.PadRight(11, '\0').ValiString(nameof(value.MakerId), 11));
                writer.WriteString(value.TerminalModel.PadRight(30, '\0').ValiString(nameof(value.TerminalModel), 30));
                writer.WriteString(value.TerminalId.PadRight(30, '\0').ValiString(nameof(value.TerminalId), 30));
            }
            else if (writer.Version == JT808Version.JTT2013)
            {
                writer.WriteString(value.MakerId.PadRight(5, '\0').ValiString(nameof(value.MakerId), 5));
                writer.WriteString(value.TerminalModel.PadRight(20, '\0').ValiString(nameof(value.TerminalModel), 20));
                writer.WriteString(value.TerminalId.PadRight(7, '\0').ValiString(nameof(value.TerminalId), 7));
            }
            else {
                writer.WriteString(value.MakerId.PadRight(5, '\0').ValiString(nameof(value.MakerId), 5));
                writer.WriteString(value.TerminalModel.PadRight(8, '\0').ValiString(nameof(value.TerminalModel), 8));
                writer.WriteString(value.TerminalId.PadRight(7, '\0').ValiString(nameof(value.TerminalId), 7));
            }
            writer.WriteByte(value.PlateColor);
            writer.WriteString(value.PlateNo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0100 jT808_0X0100 = new JT808_0x0100();
            jT808_0X0100.AreaID = reader.ReadUInt16();
            writer.WriteNumber($"[{jT808_0X0100.AreaID.ReadNumber()}]省域ID", jT808_0X0100.AreaID);
            jT808_0X0100.CityOrCountyId = reader.ReadUInt16();
            writer.WriteNumber($"[{jT808_0X0100.CityOrCountyId.ReadNumber()}]市县域ID", jT808_0X0100.CityOrCountyId);
            if (reader.Version == JT808Version.JTT2019)
            {
                ReadOnlySpan<byte> midSpan = reader.ReadVirtualArray(11);
                jT808_0X0100.MakerId = reader.ReadString(11);
                writer.WriteString($"[{midSpan.ToArray().ToHexString()}]制造商ID(11)", jT808_0X0100.MakerId);
                ReadOnlySpan<byte> tmSpan = reader.ReadVirtualArray(30);
                jT808_0X0100.TerminalModel = reader.ReadString(30);
                writer.WriteString($"[{tmSpan.ToArray().ToHexString()}]终端型号(30)", jT808_0X0100.TerminalModel);
                ReadOnlySpan<byte> tidSpan = reader.ReadVirtualArray(30);
                jT808_0X0100.TerminalId = reader.ReadString(30);
                writer.WriteString($"[{tidSpan.ToArray().ToHexString()}]终端ID(30)", jT808_0X0100.TerminalId);
            }
            else if (reader.Version == JT808Version.JTT2013)
            {
                var length = reader.ReadCurrentRemainContentLength();
                if (length > 33)
                {
                    ReadOnlySpan<byte> midSpan = reader.ReadVirtualArray(5);
                    jT808_0X0100.MakerId = reader.ReadString(5);
                    writer.WriteString($"[{midSpan.ToArray().ToHexString()}]制造商ID(5)", jT808_0X0100.MakerId);
                    ReadOnlySpan<byte> tmSpan = reader.ReadVirtualArray(20);
                    jT808_0X0100.TerminalModel = reader.ReadString(20);
                    writer.WriteString($"[{tmSpan.ToArray().ToHexString()}]终端型号(20)", jT808_0X0100.TerminalModel);
                    ReadOnlySpan<byte> tidSpan = reader.ReadVirtualArray(7);
                    jT808_0X0100.TerminalId = reader.ReadString(7);
                    writer.WriteString($"[{tidSpan.ToArray().ToHexString()}]终端ID(7)", jT808_0X0100.TerminalId);
                }
                else {
                    ReadOnlySpan<byte> midSpan = reader.ReadVirtualArray(5);
                    jT808_0X0100.MakerId = reader.ReadString(5);
                    writer.WriteString($"[{midSpan.ToArray().ToHexString()}]制造商ID(5)", jT808_0X0100.MakerId);
                    ReadOnlySpan<byte> tmSpan = reader.ReadVirtualArray(8);
                    jT808_0X0100.TerminalModel = reader.ReadString(8);
                    writer.WriteString($"[{tmSpan.ToArray().ToHexString()}]终端型号(8)", jT808_0X0100.TerminalModel);
                    ReadOnlySpan<byte> tidSpan = reader.ReadVirtualArray(7);
                    jT808_0X0100.TerminalId = reader.ReadString(7);
                    writer.WriteString($"[{tidSpan.ToArray().ToHexString()}]终端ID(7)", jT808_0X0100.TerminalId);
                }
            }
            jT808_0X0100.PlateColor = reader.ReadByte();
            writer.WriteNumber($"[{jT808_0X0100.PlateColor.ReadNumber()}]车牌颜色", jT808_0X0100.PlateColor);
            ReadOnlySpan<byte> vnoSpan = reader.ReadVirtualArray(reader.ReadCurrentRemainContentLength());
            jT808_0X0100.PlateNo = reader.ReadRemainStringContent();
            writer.WriteString($"[{vnoSpan.ToArray().ToHexString()}]车牌号码", jT808_0X0100.PlateNo);
        }
    }
}
