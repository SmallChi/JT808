using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 单条存储多媒体数据检索上传命令
    /// </summary>
    public class JT808_0x8805 : JT808MessagePackFormatter<JT808_0x8805>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8805
        /// </summary>
        public ushort MsgId => 0x8805;

        /// <summary>
        /// 单条存储多媒体数据检索上传命令
        /// </summary>
        public string Description => "单条存储多媒体数据检索上传命令";
        /// <summary>
        /// 多媒体ID
        /// </summary>
        public uint MultimediaId { get; set; }
        /// <summary>
        /// 删除标志
        /// <see cref="JT808.Protocol.Enums.JT808MultimediaDeleted"/>
        /// </summary>
        public byte MultimediaDeleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8805 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8805 jT808_0X8805 = new JT808_0x8805();
            jT808_0X8805.MultimediaId = reader.ReadUInt32();
            jT808_0X8805.MultimediaDeleted = reader.ReadByte();
            return jT808_0X8805;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8805 value, IJT808Config config)
        {
            writer.WriteUInt32(value.MultimediaId);
            writer.WriteByte(value.MultimediaDeleted);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8805 value = new JT808_0x8805();
            value.MultimediaId = reader.ReadUInt32();
            value.MultimediaDeleted = reader.ReadByte();
            JT808MultimediaDeleted multimediaDeleted = (JT808MultimediaDeleted)value.MultimediaDeleted;
            writer.WriteNumber($"[{value.MultimediaId.ReadNumber()}]多媒体ID", value.MultimediaId);
            writer.WriteNumber($"[{value.MultimediaDeleted.ReadNumber()}]删除标志-{multimediaDeleted}", value.MultimediaDeleted);
        }
    }
}
