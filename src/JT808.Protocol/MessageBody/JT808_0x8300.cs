
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Linq;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 文本信息下发
    /// </summary>
    public class JT808_0x8300 : JT808MessagePackFormatter<JT808_0x8300>, JT808Bodies,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8300
        /// </summary>
        public ushort MsgId  => 0x8300;
        /// <summary>
        /// 文本信息下发
        /// </summary>
        public string Description => "文本信息下发";
        /// <summary>
        /// 文本信息标志位含义见 表 38
        /// </summary>
        public byte TextFlag { get; set; }
        /// <summary>
        /// 文本类型
        /// 1=通知,2=服务
        /// 2019版本
        /// </summary>
        public byte TextType { get; set; }
        /// <summary>
        /// 文本信息
        /// 最长为 1024 字节，经GBK编码
        /// </summary>
        public string TextInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8300 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8300 jT808_0X8300 = new JT808_0x8300();
            jT808_0X8300.TextFlag = reader.ReadByte();
            if(reader.Version== JT808Version.JTT2019)
            {
                jT808_0X8300.TextType = reader.ReadByte();
            }
            jT808_0X8300.TextInfo = reader.ReadRemainStringContent();
            return jT808_0X8300;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8300 value, IJT808Config config)
        {
            writer.WriteByte(value.TextFlag);
            if (writer.Version == JT808Version.JTT2019)
            {
                writer.WriteByte(value.TextType);
            }
            writer.WriteString(value.TextInfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8300 value = new JT808_0x8300();
            value.TextFlag = reader.ReadByte();
            writer.WriteNumber($"[{ value.TextFlag.ReadNumber()}]文本信息标志位", value.TextFlag);
            ReadOnlySpan<char> textFlagBits =string.Join("",Convert.ToString(value.TextFlag, 2).PadLeft(8, '0').Reverse()).AsSpan();
            if (reader.Version == JT808Version.JTT2019)
            {
                writer.WriteStartObject($"文本信息标志对象[{textFlagBits.ToString()}]");
                writer.WriteString($"[bit6~-bit7]保留", textFlagBits.Slice(6, 2).ToString());
                writer.WriteString($"[bit5]{textFlagBits[5]}", textFlagBits[5] == '0' ? "中心导航信息" : "CAN故障码信息");
                writer.WriteString($"[bit4]{textFlagBits[4]}", "-");
                writer.WriteString($"[bit3]{textFlagBits[3]}", "终端TTS播读");
                writer.WriteString($"[bit2]{textFlagBits[2]}", "终端显示器显示");
                var bit0And1= textFlagBits.Slice(0, 2).ToString().Reverse().ToArray().AsSpan().ToString();
                switch (bit0And1)
                {
                    case "01":
                        writer.WriteString($"[bit0~1]{textFlagBits[0]}", "服务");
                        break;
                    case "10":
                        writer.WriteString($"[bit0~1]{textFlagBits[0]}", "紧急");
                        break;
                    case "11":
                        writer.WriteString($"[bit0~1]{textFlagBits[0]}", "通知");
                        break;
                    case "00":
                        writer.WriteString($"[bit0~1]{textFlagBits[0]}", "保留");
                        break;
                }
                writer.WriteEndObject();
                value.TextType = reader.ReadByte();
                if (value.TextType == 1)
                {
                    writer.WriteNumber($"[{ value.TextType.ReadNumber()}]文本类型-通知", value.TextType);
                }
                else if (value.TextType == 2)
                {
                    writer.WriteNumber($"[{ value.TextType.ReadNumber()}]文本类型-服务", value.TextType);
                }
                else {
                    writer.WriteNumber($"[{ value.TextType.ReadNumber()}]文本类型-未设置", value.TextType);
                }      
            }
            else
            {
                writer.WriteStartObject($"文本信息标志对象[{textFlagBits.ToString()}]");
                writer.WriteString($"[bit6~-bit7]保留", textFlagBits.Slice(6, 2).ToString());
                writer.WriteString($"[bit5]{textFlagBits[5]}", textFlagBits[5] == '0' ? "中心导航信息" : "CAN故障码信息");
                writer.WriteString($"[bit4]{textFlagBits[4]}", "广告屏显示");
                writer.WriteString($"[bit3]{textFlagBits[3]}", "终端TTS播读");
                writer.WriteString($"[bit2]{textFlagBits[2]}", "终端显示器显示");
                writer.WriteString($"[bit1]{textFlagBits[1]}", "保留");
                writer.WriteString($"[bit0]{textFlagBits[0]}", "紧急");
                writer.WriteEndObject();
            }
            var txtBuffer = reader.ReadVirtualArray(reader.ReadCurrentRemainContentLength()).ToArray();
            value.TextInfo = reader.ReadRemainStringContent();
            writer.WriteString($"[{txtBuffer.ToHexString()}]答案内容", value.TextInfo);
        }
    }
}
