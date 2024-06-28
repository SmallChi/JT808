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
    /// 20
    ///  iccid,一般开机或者重连第一条0200位置信息上报,
    ///  例子报文：f1143839383630343032313032303930393737303032,解析结果为:89860402102090977002
    /// </summary>
    public class JT808_0x0200_0xf1 : JT808MessagePackFormatter<JT808_0x0200_0xf1>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_GPS51_Constants.JT808_0x0200_0xf1;
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// ICCID
        /// </summary>
        public string ICCID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0xf1 value = new JT808_0x0200_0xf1();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            var iccid = reader.ReadVirtualArray(value.AttachInfoLength);
            writer.WriteString($"[{iccid.ToArray().ToHexString()}]ICCID", reader.ReadRemainStringContent());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0xf1 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0xf1 value = new JT808_0x0200_0xf1();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.ICCID = reader.ReadRemainStringContent();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0xf1 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.Skip(1,out int position);
            writer.WriteString(value.ICCID);
            var length = writer.GetCurrentPosition() - position - 1;
            writer.WriteByteReturn((byte)length, position);
        }
    }
}
