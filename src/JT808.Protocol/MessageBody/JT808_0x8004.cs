using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询服务器时间应答
    /// 2019版本
    /// </summary>
    public class JT808_0x8004 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8004>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8004;

        public DateTime Time { get; set; } = DateTime.Now;

        public JT808_0x8004 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8004 value = new JT808_0x8004();
            value.Time = reader.ReadDateTime6();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8004 value, IJT808Config config)
        {
            writer.WriteDateTime6(value.Time);
        }
    }
}
