using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 车辆控制应答
    /// </summary>
    public class JT808_0x0500 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0500>
    {
        public override ushort MsgId { get; } = 0x0500;
        /// <summary>
        /// 应答流水号
        /// 对应的终端注册消息的流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 位置信息汇报消息体
        /// </summary>
        public JT808_0x0200 JT808_0x0200 { get; set; }
        public JT808_0x0500 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0500 jT808_0X0500 = new JT808_0x0500();
            jT808_0X0500.MsgNum = reader.ReadUInt16();
            jT808_0X0500.JT808_0x0200 = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref reader, config);
            return jT808_0X0500;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0500 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, value.JT808_0x0200, config);
        }
    }
}
