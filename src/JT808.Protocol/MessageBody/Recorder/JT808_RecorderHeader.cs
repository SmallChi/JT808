using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody.Recorder
{
    public class JT808_RecorderHeader: IJT808MessagePackFormatter<JT808_RecorderHeader>, IJT808Analyze
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public byte CommandId { get; set; }
        /// <summary>
        /// 数据块长度
        /// </summary>
        public ushort DataLength { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            throw new NotImplementedException();
        }

        public JT808_RecorderHeader Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_RecorderHeader value = new JT808_RecorderHeader();
            value.CommandId = reader.ReadByte();
            value.DataLength = reader.ReadUInt16();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_RecorderHeader value, IJT808Config config)
        {
            writer.WriteByte(value.CommandId);
            writer.WriteUInt16(value.DataLength);
        }
    }
}
