using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using JT808.Protocol.Extensions.GPS51.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions.GPS51.MessageBody
{
    /// <summary>
    /// 8
    //// IMEI数据：8字节，第1位为0，后面15位为imei的16进制数据
    /// </summary>
    public class JT808_0x0200_0xf6 : JT808MessagePackFormatter<JT808_0x0200_0xf6>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_GPS51_Constants.JT808_0x0200_0xf6;
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// IMEI
        /// </summary>
        public string IMEI { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0xf6 value = new JT808_0x0200_0xf6();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            var imei = reader.ReadVirtualArray(value.AttachInfoLength);
            writer.WriteString($"[{imei.ToArray().ToHexString()}]IMEI", reader.ReadRemainStringContent());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0xf6 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0xf6 value = new JT808_0x0200_0xf6();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.IMEI = reader.ReadRemainStringContent();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0xf6 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.Skip(1, out int position);
            writer.WriteString(value.IMEI);
            var length = writer.GetCurrentPosition() - position - 1;
            writer.WriteByteReturn((byte)length, position);
        }
    }
}
