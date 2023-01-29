using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端鉴权
    /// </summary>
    public class JT808_0x0102 : JT808MessagePackFormatter<JT808_0x0102>, JT808Bodies, IJT808_2019_Version, IJT808Analyze
    {
        /// <summary>
        /// 0x0102
        /// </summary>
        public ushort MsgId => 0x0102;
        /// <summary>
        /// 终端鉴权
        /// </summary>
        public string Description => "终端鉴权";
        /// <summary>
        /// 鉴权码
        /// 鉴权码内容 2019版本
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 鉴权码长度 2019版本
        /// </summary>
        public byte CodeLength { get; set; }
        /// <summary>
        /// 终端IMEI  长度15 2019版本
        /// </summary>
        public string IMEI { get; set; }
        /// <summary>
        /// 软件版本号 长度20 后补 "0x00" 2019版本
        /// </summary>
        public string SoftwareVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0102 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0102 jT808_0X0102 = new JT808_0x0102();
            if(reader.Version== JT808Version.JTT2019)
            {
                jT808_0X0102.CodeLength = reader.ReadByte();
                jT808_0X0102.Code = reader.ReadString(jT808_0X0102.CodeLength);
                jT808_0X0102.IMEI = reader.ReadString(15);
                jT808_0X0102.SoftwareVersion = reader.ReadString(20);
            }
            else
            {
                jT808_0X0102.Code = reader.ReadRemainStringContent();
            }

            return jT808_0X0102;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0102 value, IJT808Config config)
        {
            if (writer.Version == JT808Version.JTT2019)
            {
                writer.Skip(1, out int CodeLengthPosition);
                writer.WriteString(value.Code);
                writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - CodeLengthPosition - 1), CodeLengthPosition);
                writer.WriteString(value.IMEI);
                writer.WriteString(value.SoftwareVersion.PadRight(20,'\0').ValiString(nameof(value.SoftwareVersion),20));
            }
            else
            {
                writer.WriteString(value.Code);
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
            JT808_0x0102 jT808_0X0102 = new JT808_0x0102();
            if (reader.Version == JT808Version.JTT2019)
            {
                jT808_0X0102.CodeLength = reader.ReadByte();
                writer.WriteNumber($"[{ jT808_0X0102.CodeLength.ReadNumber()}]鉴权码长度", jT808_0X0102.CodeLength);
                ReadOnlySpan<byte> codeSpan = reader.ReadVirtualArray(jT808_0X0102.CodeLength);
                jT808_0X0102.Code = reader.ReadString(jT808_0X0102.CodeLength);
                writer.WriteString($"[{codeSpan.ToArray().ToHexString()}]鉴权码", jT808_0X0102.Code);
                ReadOnlySpan<byte> imeiSpan = reader.ReadVirtualArray(15);
                jT808_0X0102.IMEI = reader.ReadString(15);
                writer.WriteString($"[{imeiSpan.ToArray().ToHexString()}]IMEI", jT808_0X0102.IMEI);
                ReadOnlySpan<byte> svSpan = reader.ReadVirtualArray(20);
                jT808_0X0102.SoftwareVersion = reader.ReadString(20);
                writer.WriteString($"[{svSpan.ToArray().ToHexString()}]软件版本号", jT808_0X0102.SoftwareVersion);
            }
            else
            {
                ReadOnlySpan<byte> codeSpan = reader.ReadVirtualArray(reader.ReadCurrentRemainContentLength());
                jT808_0X0102.Code = reader.ReadRemainStringContent();
                writer.WriteString($"[{codeSpan.ToArray().ToHexString()}]鉴权码", jT808_0X0102.Code);
            }
        }
    }
}
