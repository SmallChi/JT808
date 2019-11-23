using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 补传分包请求
    /// 0x8003
    /// </summary>
    public class JT808_0x8003 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8003>
    {
        public override ushort MsgId { get; } = 0x8003;
        /// <summary>
        /// 原始消息流水号
        /// 对应要求补传的原始消息第一包的消息流水号
        /// </summary>
        public ushort OriginalMsgNum { get; set; }
        /// <summary>
        /// 重传包总数
        /// n
        /// </summary>
        public byte AgainPackageCount { get; set; }
        /// <summary>
        /// 重传包 ID 列表
        /// BYTE[2*n]
        /// 重传包序号顺序排列，如“包 ID1 包 ID2......包 IDn”。
        /// </summary>
        public byte[] AgainPackageData { get; set; }
        public JT808_0x8003 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8003 jT808_0X8003 = new JT808_0x8003();
            jT808_0X8003.OriginalMsgNum = reader.ReadUInt16();
            jT808_0X8003.AgainPackageCount = reader.ReadByte();
            jT808_0X8003.AgainPackageData = reader.ReadArray(jT808_0X8003.AgainPackageCount * 2).ToArray();
            return jT808_0X8003;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8003 value, IJT808Config config)
        {
            writer.WriteUInt16(value.OriginalMsgNum);
            writer.WriteByte((byte)(value.AgainPackageData.Length / 2));
            writer.WriteArray(value.AgainPackageData);
        }
    }
}
