using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Metadata;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 存储多媒体数据检索应答
    /// 0x0802
    /// </summary>
    public class JT808_0x0802 : JT808MessagePackFormatter<JT808_0x0802>, JT808Bodies,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x0802
        /// </summary>
        public ushort MsgId => 0x0802;
        /// <summary>
        /// 存储多媒体数据检索应答
        /// </summary>
        public string Description => "存储多媒体数据检索应答";
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0802 value = new JT808_0x0802();
            value.MsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.MsgNum.ReadNumber()}]应答流水号", value.MsgNum);
            value.MultimediaItemCount = reader.ReadUInt16();
            writer.WriteNumber($"[{value.MultimediaItemCount.ReadNumber()}]多媒体数据总项数", value.MultimediaItemCount);
            writer.WriteStartArray("多媒体数据集合");
            for (var i = 0; i < value.MultimediaItemCount; i++)
            {
                writer.WriteStartObject();
                JT808MultimediaSearchProperty jT808MultimediaSearchProperty = new JT808MultimediaSearchProperty();
                if (reader.ReadCurrentRemainContentLength() == (value.MultimediaItemCount - i) * (4 + 1 + 1 + 1 + 28))
                {
                    //2013 ,2019协议          
                    jT808MultimediaSearchProperty.MultimediaId = reader.ReadUInt32();
                    writer.WriteNumber($"[{jT808MultimediaSearchProperty.MultimediaId.ReadNumber()}]多媒体ID", jT808MultimediaSearchProperty.MultimediaId);
                }
                jT808MultimediaSearchProperty.MultimediaType = reader.ReadByte();
                writer.WriteNumber($"[{jT808MultimediaSearchProperty.MultimediaType.ReadNumber()}]多媒体类型-{((JT808MultimediaType)jT808MultimediaSearchProperty.MultimediaType).ToString()}", jT808MultimediaSearchProperty.MultimediaType);
                jT808MultimediaSearchProperty.ChannelId = reader.ReadByte();
                writer.WriteNumber($"[{jT808MultimediaSearchProperty.ChannelId.ReadNumber()}]通道ID", jT808MultimediaSearchProperty.ChannelId);
                jT808MultimediaSearchProperty.EventItemCoding = reader.ReadByte();
                writer.WriteNumber($"[{jT808MultimediaSearchProperty.EventItemCoding.ReadNumber()}]事件项编码-{((JT808EventItemCoding)jT808MultimediaSearchProperty.EventItemCoding).ToString()}", jT808MultimediaSearchProperty.EventItemCoding);
                JT808MessagePackReader positionReader = new JT808MessagePackReader(reader.ReadArray(28), reader.Version);
                writer.WriteStartObject($"位置基本信息");
                config.GetAnalyze<JT808_0x0200>().Analyze(ref positionReader, writer, config);
                writer.WriteEndObject();
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0802 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0802 value = new JT808_0x0802();
            value.MsgNum = reader.ReadUInt16();
            value.MultimediaItemCount = reader.ReadUInt16();
            value.MultimediaSearchItems = new List<JT808MultimediaSearchProperty>();
            for (var i = 0; i < value.MultimediaItemCount; i++)
            {
                JT808MultimediaSearchProperty jT808MultimediaSearchProperty = new JT808MultimediaSearchProperty();
                if (reader.ReadCurrentRemainContentLength() ==(value.MultimediaItemCount-i)*(4 + 1 + 1 + 1 + 28))
                {
                    //2013 ,2019协议                   
                    jT808MultimediaSearchProperty.MultimediaId = reader.ReadUInt32();

                }
                jT808MultimediaSearchProperty.MultimediaType = reader.ReadByte();
                jT808MultimediaSearchProperty.ChannelId = reader.ReadByte();
                jT808MultimediaSearchProperty.EventItemCoding = reader.ReadByte();
                JT808MessagePackReader positionReader = new JT808MessagePackReader(reader.ReadArray(28), reader.Version);
                jT808MultimediaSearchProperty.Position = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref positionReader, config);
                value.MultimediaSearchItems.Add(jT808MultimediaSearchProperty);
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0802 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteUInt16((ushort)value.MultimediaSearchItems.Count);
            foreach (var item in value.MultimediaSearchItems)
            {
                if (writer.Version != JT808Version.JTT2011)
                {
                    writer.WriteUInt32(item.MultimediaId);

                }
                writer.WriteByte(item.MultimediaType);
                writer.WriteByte(item.ChannelId);
                writer.WriteByte(item.EventItemCoding);
                config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, item.Position, config);
            }
        }
    }
}
