using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 多媒体数据上传应答
    /// 0x8800
    /// </summary>
    public class JT808_0x8800 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8800>
    {
        public override ushort MsgId { get; } = 0x8800;
        /// <summary>
        /// 多媒体ID
        /// </summary>
        public uint MultimediaId { get; set; }
        /// <summary>
        /// 重传包总数
        /// </summary>
        public byte RetransmitPackageCount { get; set; }
        /// <summary>
        /// 重传包 ID 列表
        /// 重传包序号顺序排列，如“包 ID1 包 ID2......包 IDn”。
        /// </summary>
        public byte[] RetransmitPackageIds { get; set; }
        public JT808_0x8800 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8800 jT808_0X8800 = new JT808_0x8800();
            jT808_0X8800.MultimediaId = reader.ReadUInt32();
            jT808_0X8800.RetransmitPackageCount = reader.ReadByte();
            jT808_0X8800.RetransmitPackageIds = reader.ReadArray(jT808_0X8800.RetransmitPackageCount * 2).ToArray();
            return jT808_0X8800;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8800 value, IJT808Config config)
        {
            writer.WriteUInt32(value.MultimediaId);
            writer.WriteByte((byte)(value.RetransmitPackageIds.Length / 2));
            writer.WriteArray(value.RetransmitPackageIds);
        }
    }
}
