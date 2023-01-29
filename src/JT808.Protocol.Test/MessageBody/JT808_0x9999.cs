using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x9999 : JT808MessagePackFormatter<JT808_0x9999>, JT808Bodies
    {
        public byte Sex { get; set; }

        public ushort MsgId => 0x9999;

        public string Description => "自定义消息";

        public override JT808_0x9999 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9999 jT808_0X9999 = new JT808_0x9999();
            jT808_0X9999.Sex = reader.ReadByte();
            return jT808_0X9999;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9999 value, IJT808Config config)
        {
            writer.WriteByte(value.Sex);
        }
    }
}
