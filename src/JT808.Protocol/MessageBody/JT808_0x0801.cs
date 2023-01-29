using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 多媒体数据上传
    /// 0x0801
    /// </summary>
    public class JT808_0x0801 : JT808MessagePackFormatter<JT808_0x0801>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x0801
        /// </summary>
        public ushort MsgId  => 0x0801;
        /// <summary>
        /// 多媒体数据上传
        /// </summary>
        public string Description => "多媒体数据上传";
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
        /// 定位错误
        /// </summary>
        public bool PositionError { get; set; }
        /// <summary>
        /// 位置信息汇报(0x0200)消息体
        /// 表示拍摄或录制的起始时刻的位置基本信息数据
        /// </summary>
        public JT808_0x0200 Position { get; set; }
        /// <summary>
        /// 多媒体数据包
        /// </summary>
        public byte[] MultimediaDataPackage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0801 value = new JT808_0x0801();
            value.MultimediaId = reader.ReadUInt32();
            writer.WriteNumber($"[{value.MultimediaId.ReadNumber()}]多媒体ID", value.MultimediaId);
            value.MultimediaType = reader.ReadByte();
            writer.WriteNumber($"[{value.MultimediaType.ReadNumber()}]多媒体类型-{((JT808MultimediaType)value.MultimediaType).ToString()}", value.MultimediaType);
            value.MultimediaCodingFormat = reader.ReadByte();
            writer.WriteNumber($"[{value.MultimediaCodingFormat.ReadNumber()}]多媒体格式编码-{((JT808MultimediaCodingFormat)value.MultimediaCodingFormat).ToString()}", value.MultimediaCodingFormat);
            value.EventItemCoding = reader.ReadByte();
            writer.WriteNumber($"[{value.EventItemCoding.ReadNumber()}]事件项编码-{((JT808EventItemCoding)value.EventItemCoding).ToString()}", value.MultimediaCodingFormat);
            value.ChannelId = reader.ReadByte();
            writer.WriteNumber($"[{value.ChannelId.ReadNumber()}]通道ID", value.ChannelId);
            if (reader.ReadCurrentRemainContentLength() >= 28)
            {
                var tempData = reader.ReadVirtualArray(28);
                try
                {
                    JT808MessagePackReader positionReader = new JT808MessagePackReader(tempData, reader.Version);
                    writer.WriteStartObject("位置基本信息");
                    config.GetAnalyze<JT808_0x0200>().Analyze(ref positionReader, writer, config);
                    writer.WriteEndObject();
                    reader.Skip(28);
                }
                catch
                {
                    PositionError = true;
                }
                value.MultimediaDataPackage = reader.ReadContent().ToArray();
                writer.WriteString($"多媒体数据包", value.MultimediaDataPackage.ToHexString());
            }
            else {
                value.MultimediaDataPackage = reader.ReadContent().ToArray();
                writer.WriteString($"多媒体数据包", value.MultimediaDataPackage.ToHexString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0801 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0801 value = new JT808_0x0801();
            value.MultimediaId = reader.ReadUInt32();
            value.MultimediaType = reader.ReadByte();
            value.MultimediaCodingFormat = reader.ReadByte();
            value.EventItemCoding = reader.ReadByte();
            value.ChannelId = reader.ReadByte();
            if (reader.ReadCurrentRemainContentLength() >= 28)
            {
                var tempData = reader.ReadVirtualArray(28);
                try
                {
                    JT808MessagePackReader positionReader = new JT808MessagePackReader(tempData, reader.Version);
                    value.Position = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref positionReader, config);
                    reader.Skip(28);
                }
                catch
                {
                    PositionError = true;
                }
                value.MultimediaDataPackage = reader.ReadContent().ToArray();
            }
            else {
                value.MultimediaDataPackage = reader.ReadContent().ToArray();
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0801 value, IJT808Config config)
        {
            writer.WriteUInt32(value.MultimediaId);
            writer.WriteByte(value.MultimediaType);
            writer.WriteByte(value.MultimediaCodingFormat);
            writer.WriteByte(value.EventItemCoding);
            writer.WriteByte(value.ChannelId);
            if (writer.Version != JT808Version.JTT2011) {
                config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, value.Position, config);
            }
            writer.WriteArray(value.MultimediaDataPackage);
        }
    }
}
