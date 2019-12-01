using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 多媒体数据上传
    /// 0x0801
    /// </summary>
    public class JT808_0x0801 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0801>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x0801;
        /// <summary>
        /// 多媒体 ID
        /// </summary>
        public uint MultimediaId { get; set; }
        /// <summary>
        /// 多媒体类型
        /// <see cref="JT808.Protocol.Enums.JT808MultimediaType"/>
        /// </summary>
        public byte MultimediaType { get; set; }
        /// <summary>
        /// 多媒体格式编码 
        /// 0：JPEG；1：TIF；2：MP3；3：WAV；4：WMV；其他保留
        /// <see cref="JT808.Protocol.Enums.JT808MultimediaCodingFormat"/>
        /// </summary>
        public byte MultimediaCodingFormat { get; set; }
        /// <summary>
        /// 事件项编码
        /// <see cref="JT808.Protocol.Enums.JT808EventItemCoding"/>
        /// </summary>
        public byte EventItemCoding { get; set; }
        /// <summary>
        /// 通道 ID
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 位置信息汇报(0x0200)消息体
        /// 表示拍摄或录制的起始时刻的位置基本信息数据
        /// </summary>
        public JT808_0x0200 Position { get; set; }
        /// <summary>
        /// 多媒体数据包
        /// </summary>
        public byte[] MultimediaDataPackage { get; set; }

        public JT808_0x0801 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0801 jT808_0X0801 = new JT808_0x0801();
            jT808_0X0801.MultimediaId = reader.ReadUInt32();
            jT808_0X0801.MultimediaType = reader.ReadByte();
            jT808_0X0801.MultimediaCodingFormat = reader.ReadByte();
            jT808_0X0801.EventItemCoding = reader.ReadByte();
            jT808_0X0801.ChannelId = reader.ReadByte();
            JT808MessagePackReader positionReader = new JT808MessagePackReader(reader.ReadArray(28));
            jT808_0X0801.Position = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref positionReader, config);
            jT808_0X0801.MultimediaDataPackage = reader.ReadContent().ToArray();
            return jT808_0X0801;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0801 value, IJT808Config config)
        {
            writer.WriteUInt32(value.MultimediaId);
            writer.WriteByte(value.MultimediaType);
            writer.WriteByte(value.MultimediaCodingFormat);
            writer.WriteByte(value.EventItemCoding);
            writer.WriteByte(value.ChannelId);
            config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, value.Position, config);
            writer.WriteArray(value.MultimediaDataPackage);
        }
    }
}
