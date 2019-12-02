using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Metadata;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 存储多媒体数据检索应答
    /// 0x0802
    /// </summary>
    public class JT808_0x0802 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0802>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x0802;
        /// <summary>
        /// 应答流水号
        /// 对应的多媒体数据检索消息的流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 多媒体数据总项数
        /// 满足检索条件的多媒体数据总项数
        /// </summary>
        public ushort MultimediaItemCount { get; set; }
        /// <summary>
        /// 检索项集合
        /// </summary>
        public List<JT808MultimediaSearchProperty> MultimediaSearchItems { get; set; }

        public JT808_0x0802 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0802 JT808_0x0802 = new JT808_0x0802();
            JT808_0x0802.MsgNum = reader.ReadUInt16();
            JT808_0x0802.MultimediaItemCount = reader.ReadUInt16();
            JT808_0x0802.MultimediaSearchItems = new List<JT808MultimediaSearchProperty>();
            for (var i = 0; i < JT808_0x0802.MultimediaItemCount; i++)
            {
                JT808MultimediaSearchProperty jT808MultimediaSearchProperty = new JT808MultimediaSearchProperty();
                jT808MultimediaSearchProperty.MultimediaId = reader.ReadUInt32();
                jT808MultimediaSearchProperty.MultimediaType = reader.ReadByte();
                jT808MultimediaSearchProperty.ChannelId = reader.ReadByte();
                jT808MultimediaSearchProperty.EventItemCoding = reader.ReadByte();
                JT808MessagePackReader positionReader = new JT808MessagePackReader(reader.ReadArray(28));
                jT808MultimediaSearchProperty.Position = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref positionReader, config);
                JT808_0x0802.MultimediaSearchItems.Add(jT808MultimediaSearchProperty);
            }
            return JT808_0x0802;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0802 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteUInt16((ushort)value.MultimediaSearchItems.Count);
            foreach (var item in value.MultimediaSearchItems)
            {
                writer.WriteUInt32(item.MultimediaId);
                writer.WriteByte(item.MultimediaType);
                writer.WriteByte(item.ChannelId);
                writer.WriteByte(item.EventItemCoding);
                config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, item.Position, config);
            }
        }
    }
}
