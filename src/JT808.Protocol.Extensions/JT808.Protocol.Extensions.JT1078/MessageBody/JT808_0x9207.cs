using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 文件上传控制
    /// </summary>
    public class JT808_0x9207 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9207>, IJT808Analyze
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public override string Description => "文件上传控制";
        public override ushort MsgId => 0x9207;
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        /// <summary>
        /// 流水号
        /// </summary>
        public ushort MgsNum { get; set; }
        /// <summary>
        /// 上传控制
        /// </summary>
        public byte UploadControl { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9207 value = new JT808_0x9207();
            value.MgsNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.MgsNum.ReadNumber()}]流水号", value.MgsNum);
            value.UploadControl = reader.ReadByte();
            writer.WriteString($"[{value.UploadControl.ReadNumber()}]上传控制", UploadControlDisplay(value.UploadControl));
            static string UploadControlDisplay(byte UploadControl) {
                return UploadControl switch
                {
                    0 => "暂停",
                    1 => "继续",
                    2 => "取消",
                    _ => "未知",
                };
            }
        }

        public JT808_0x9207 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            var jT808_0x9207 = new JT808_0x9207();
            jT808_0x9207.MgsNum = reader.ReadUInt16();
            jT808_0x9207.UploadControl = reader.ReadByte();
            return jT808_0x9207;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9207 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MgsNum);
            writer.WriteByte(value.UploadControl);
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
