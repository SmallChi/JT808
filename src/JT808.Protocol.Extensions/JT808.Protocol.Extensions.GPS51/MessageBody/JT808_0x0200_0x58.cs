using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    /// 2*N
    /// 湿度，精度0.1，0fff 代表无效数据，例子数据： 0012 表示：1.8%
    /// </summary>
    public class JT808_0x0200_0x58 : JT808MessagePackFormatter<JT808_0x0200_0x58>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_GPS51_Constants.JT808_0x0200_0x58;
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// 湿度
        /// </summary>
        public List<ushort> Humiditys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x58 value = new JT808_0x0200_0x58();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            writer.WriteNumber($"[{reader.ReadContent(value.AttachInfoLength).ToArray().ToHexString()}]湿度列表", value.AttachInfoLength / 2);
            writer.WriteStartArray();
            while (reader.ReadCurrentRemainContentLength() > 0) { 
                writer.WriteNumber($"[{reader.ReadUInt16().ReadNumber()}]附加信息长度", reader.ReadUInt16());
            }
            writer.WriteStartArray();
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x58 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x58 value = new JT808_0x0200_0x58();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Humiditys = new List<ushort>();
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.Humiditys.Add(reader.ReadUInt16());
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x58 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.Skip(1,out int position);
            foreach (var humidity in value.Humiditys)
            {
                writer.WriteUInt16(humidity);
            }
            int length = writer.GetCurrentPosition() - position - 1;
            writer.WriteByteReturn((byte)length, position);
        }
    }


}
