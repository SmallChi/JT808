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
    public class JT808_0x9207 : JT808MessagePackFormatter<JT808_0x9207>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 文件上传控制
        /// </summary>
        public string Description => "文件上传控制";
        /// <summary>
        /// 0x9207
        /// </summary>
        public ushort MsgId => 0x9207;
        /// <summary>
        /// 流水号
        /// </summary>
        public ushort MgsNum { get; set; }
        /// <summary>
        /// 上传控制
        /// </summary>
        public byte UploadControl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x9207 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            var jT808_0x9207 = new JT808_0x9207();
            jT808_0x9207.MgsNum = reader.ReadUInt16();
            jT808_0x9207.UploadControl = reader.ReadByte();
            return jT808_0x9207;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9207 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MgsNum);
            writer.WriteByte(value.UploadControl);
        }
    }
}
