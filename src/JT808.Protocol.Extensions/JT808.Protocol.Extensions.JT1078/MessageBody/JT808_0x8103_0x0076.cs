using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 音视频通道列表设置
    /// 0x8103_0x0076
    /// </summary>
    public class JT808_0x8103_0x0076 : JT808MessagePackFormatter<JT808_0x8103_0x0076>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public uint ParamId { get; set; } = 0x0076;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte ParamLength { get; set; }
        /// <summary>
        /// 音视频通道总数
        /// l
        /// </summary>
        public byte AVChannelTotal { get; set; }
        /// <summary>
        /// 音频通道总数
        /// m
        /// </summary>
        public byte AudioChannelTotal { get; set; }
        /// <summary>
        /// 视频通道总数
        /// n
        /// </summary>
        public byte VudioChannelTotal { get; set; }
        /// <summary>
        /// 音视频通道对照表
        /// 4*(l+m+n)
        /// </summary>
        public List<JT808_0x8103_0x0076_AVChannelRefTable> AVChannelRefTables { get; set; }
        /// <summary>
        /// 音视频通道列表设置
        /// </summary>
        public string Description => "音视频通道列表设置";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0076 value = new JT808_0x8103_0x0076();
            value.ParamId = reader.ReadUInt32();
            writer.WriteNumber($"[{value.ParamId.ReadNumber()}]参数 ID", value.ParamId);
            value.ParamLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]数据长度", value.ParamLength);
            value.AVChannelTotal = reader.ReadByte();
            writer.WriteNumber($"[{value.AVChannelTotal.ReadNumber()}]音视频通道总数", value.AVChannelTotal);
            value.AudioChannelTotal = reader.ReadByte();
            writer.WriteNumber($"[{value.AudioChannelTotal.ReadNumber()}]音频通道总数", value.AudioChannelTotal);
            value.VudioChannelTotal = reader.ReadByte();
            writer.WriteNumber($"[{value.VudioChannelTotal.ReadNumber()}]视频通道总数", value.VudioChannelTotal);
            var channelTotal = value.AVChannelTotal + value.AudioChannelTotal + value.VudioChannelTotal;//通道总数

            writer.WriteStartArray("音视频通道对照表");
            for (int i = 0; i < channelTotal; i++)
            {
                writer.WriteStartObject();
                var formatter = config.GetMessagePackFormatter<JT808_0x8103_0x0076_AVChannelRefTable>();
                formatter.Analyze(ref reader, writer, config);
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
        public override JT808_0x8103_0x0076 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0076 jT808_0X8103_0X0076 = new JT808_0x8103_0x0076();
            jT808_0X8103_0X0076.ParamId = reader.ReadUInt32();
            jT808_0X8103_0X0076.ParamLength = reader.ReadByte();
            jT808_0X8103_0X0076.AVChannelTotal = reader.ReadByte();
            jT808_0X8103_0X0076.AudioChannelTotal = reader.ReadByte();
            jT808_0X8103_0X0076.VudioChannelTotal = reader.ReadByte();
            var channelTotal = jT808_0X8103_0X0076.AVChannelTotal + jT808_0X8103_0X0076.AudioChannelTotal + jT808_0X8103_0X0076.VudioChannelTotal;//通道总数
            if (channelTotal > 0)
            {
                jT808_0X8103_0X0076.AVChannelRefTables = new List<JT808_0x8103_0x0076_AVChannelRefTable>();
                var formatter = config.GetMessagePackFormatter<JT808_0x8103_0x0076_AVChannelRefTable>();
                for (int i = 0; i < channelTotal; i++)
                {
                    jT808_0X8103_0X0076.AVChannelRefTables.Add(formatter.Deserialize(ref reader, config));
                }
            }
            return jT808_0X8103_0X0076;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0076 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int position);
            writer.WriteByte(value.AVChannelTotal);
            writer.WriteByte(value.AudioChannelTotal);
            writer.WriteByte(value.VudioChannelTotal);
            if (value.AVChannelRefTables.Any())
            {
                var formatter = config.GetMessagePackFormatter<JT808_0x8103_0x0076_AVChannelRefTable>();
                foreach (var AVChannelRefTable in value.AVChannelRefTables)
                {
                    formatter.Serialize(ref writer, AVChannelRefTable, config);
                }
            }
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position - 1), position);
        }
    }
}
