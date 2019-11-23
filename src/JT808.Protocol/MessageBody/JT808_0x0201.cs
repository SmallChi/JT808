using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置信息查询应答
    /// </summary>
    public class JT808_0x0201 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0201>
    {
        public override ushort MsgId { get; } = 0x0201;
        /// <summary>
        /// 应答流水号
        /// 对应的终端注册消息的流水号
        /// </summary>
        public ushort ReplyMsgNum { get; set; }

        /// <summary>
        /// 位置信息汇报见 8.12
        /// </summary>
        public JT808_0x0200 Position { get; set; }
        public JT808_0x0201 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0201 jT808_0X0201 = new JT808_0x0201();
            jT808_0X0201.ReplyMsgNum = reader.ReadUInt16();
            jT808_0X0201.Position = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref reader, config);
            return jT808_0X0201;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0201 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ReplyMsgNum);
            config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, value.Position, config);
        }
    }
}
