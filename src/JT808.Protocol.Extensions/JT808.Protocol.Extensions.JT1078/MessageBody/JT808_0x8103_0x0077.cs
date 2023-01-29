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
    ///单独视频通道参数设置
    /// 0x8103_0x0077
    /// </summary>
    public class JT808_0x8103_0x0077 : JT808MessagePackFormatter<JT808_0x8103_0x0077>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public uint ParamId { get; set; } = 0x0077;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte ParamLength { get; set; }
        /// <summary>
        /// 需单独设置视频参数的通道数量 用n表示
        /// </summary>
        public byte NeedSetChannelTotal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<JT808_0x8103_0x0077_SignalChannel> SignalChannels { get; set; }
        /// <summary>
        /// 单独视频通道参数设置
        /// </summary>
        public string Description => "单独视频通道参数设置";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0077 value = new JT808_0x8103_0x0077();
            value.ParamId = reader.ReadUInt32();
            writer.WriteNumber($"[{value.ParamId.ReadNumber()}]参数 ID", value.ParamId);
            value.ParamLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]数据长度", value.ParamLength);
            value.NeedSetChannelTotal = reader.ReadByte();
            writer.WriteNumber($"[{value.NeedSetChannelTotal.ReadNumber()}]需单独设置视频参数的通道数量", value.NeedSetChannelTotal);
            writer.WriteStartArray("音视频通道对照表");
            for (int i = 0; i < value.NeedSetChannelTotal; i++)
            {
                writer.WriteStartObject();
                var formatter = config.GetMessagePackFormatter<JT808_0x8103_0x0077_SignalChannel>();
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
        public override JT808_0x8103_0x0077 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0077 jT808_0X8103_0X0077 = new JT808_0x8103_0x0077();
            jT808_0X8103_0X0077.ParamId = reader.ReadUInt32();
            jT808_0X8103_0X0077.ParamLength = reader.ReadByte();
            jT808_0X8103_0X0077.NeedSetChannelTotal = reader.ReadByte();
            if (jT808_0X8103_0X0077.NeedSetChannelTotal > 0)
            {
                jT808_0X8103_0X0077.SignalChannels = new List<JT808_0x8103_0x0077_SignalChannel>();
                var formatter = config.GetMessagePackFormatter<JT808_0x8103_0x0077_SignalChannel>();
                for (int i = 0; i < jT808_0X8103_0X0077.NeedSetChannelTotal; i++)
                {
                    jT808_0X8103_0X0077.SignalChannels.Add(formatter.Deserialize(ref reader, config));
                }
            }
            return jT808_0X8103_0X0077;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0077 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out var position);
            writer.WriteByte(value.NeedSetChannelTotal);
            if (value.SignalChannels.Any())
            {
                var formatter = config.GetMessagePackFormatter<JT808_0x8103_0x0077_SignalChannel>();
                foreach (var signalChannel in value.SignalChannels)
                {
                    formatter.Serialize(ref writer, signalChannel, config);
                }
            }
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position - 1), position);
        }
    }
}
