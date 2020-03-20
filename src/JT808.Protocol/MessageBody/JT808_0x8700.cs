using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x8700 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8700>, IJT808_2019_Version, IJT808Analyze
    {
        public override ushort MsgId => 0x8700;

        public override string Description => "行驶记录数据采集命令";
        /// <summary>
        /// 
        /// </summary>
        public byte CommandId { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            throw new NotImplementedException();
        }

        public JT808_0x8700 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8700 value, IJT808Config config)
        {
            throw new NotImplementedException();
        }
    }
}
