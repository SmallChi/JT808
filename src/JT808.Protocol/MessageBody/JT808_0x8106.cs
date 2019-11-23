using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询指定终端参数
    /// 0x8106
    /// </summary>
    public class JT808_0x8106 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8106>
    {
        public override ushort MsgId { get; } = 0x8106;
        /// <summary>
        /// 参数总数
        /// 参数总数为 n
        /// </summary>
        public byte ParameterCount { get; set; }
        /// <summary>
        /// 参数 ID 列表
        /// 参数顺序排列，如“参数 ID1 参数 ID2......参数IDn”。
        /// </summary>
        public uint[] Parameters { get; set; }

        public JT808_0x8106 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8106 jT808_0X8106 = new JT808_0x8106();
            jT808_0X8106.ParameterCount = reader.ReadByte();
            jT808_0X8106.Parameters = new uint[jT808_0X8106.ParameterCount];
            for (int i = 0; i < jT808_0X8106.ParameterCount; i++)
            {
                jT808_0X8106.Parameters.SetValue(reader.ReadUInt32(), i);
            }
            return jT808_0X8106;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8106 value, IJT808Config config)
        {
            writer.WriteByte(value.ParameterCount);
            for (int i = 0; i < value.ParameterCount; i++)
            {
                writer.WriteUInt32(value.Parameters[i]);
            }
        }

    }
}
