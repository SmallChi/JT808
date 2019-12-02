using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 单条存储多媒体数据检索上传命令
    /// </summary>
    public class JT808_0x8805 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8805>,IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8805;
        /// <summary>
        /// 多媒体ID
        /// </summary>
        public uint MultimediaId { get; set; }
        /// <summary>
        /// 删除标志
        /// <see cref="JT808.Protocol.Enums.JT808MultimediaDeleted"/>
        /// </summary>
        public byte MultimediaDeleted { get; set; }
        public JT808_0x8805 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8805 jT808_0X8805 = new JT808_0x8805();
            jT808_0X8805.MultimediaId = reader.ReadUInt32();
            jT808_0X8805.MultimediaDeleted = reader.ReadByte();
            return jT808_0X8805;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8805 value, IJT808Config config)
        {
            writer.WriteUInt32(value.MultimediaId);
            writer.WriteByte(value.MultimediaDeleted);
        }
    }
}
