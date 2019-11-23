using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 车辆控制
    /// </summary>
    public class JT808_0x8500 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8500>
    {
        public override ushort MsgId { get; } = 0x8500;
        /// <summary>
        /// 控制标志 
        /// 控制指令标志位数据格式
        /// 0：车门解锁；1：车门加锁
        /// 1-7 保留
        /// </summary>
        public byte ControlFlag { get; set; }

        public JT808_0x8500 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8500 jT808_0X8500 = new JT808_0x8500();
            jT808_0X8500.ControlFlag = reader.ReadByte();
            return jT808_0X8500;
        }
        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8500 value, IJT808Config config)
        {
            writer.WriteByte(value.ControlFlag);
        }
    }
}
