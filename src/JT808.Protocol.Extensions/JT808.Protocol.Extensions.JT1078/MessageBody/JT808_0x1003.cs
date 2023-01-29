using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 终端上传音视频属性
    /// </summary>
    public class JT808_0x1003 : JT808MessagePackFormatter<JT808_0x1003>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 输入音频编码方式
        /// </summary>
        public byte EnterAudioEncoding { get; set; }
        /// <summary>
        /// 输入音频声道数
        /// </summary>
        public byte EnterAudioChannelsNumber { get; set; }
        /// <summary>
        /// 输入音频采样率
        /// </summary>
        public byte EnterAudioSampleRate  { get; set; }
        /// <summary>
        /// 输入音频采样位数
        /// </summary>
        public byte EnterAudioSampleDigits { get; set; }
        /// <summary>
        /// 音频帧长度
        /// </summary>
        public ushort AudioFrameLength { get; set; }
        /// <summary>
        /// 是否支持音频输出
        /// </summary>
        public byte IsSupportedAudioOutput { get; set; }
        /// <summary>
        /// 视频编码方式
        /// </summary>
        public byte VideoEncoding { get; set; }
        /// <summary>
        /// 终端支持的最大音频物理通道数量
        /// </summary>
        public byte TerminalSupportedMaxNumberOfAudioPhysicalChannels{ get; set; }
        /// <summary>
        /// 终端支持的最大视频物理通道数量
        /// </summary>
        public byte TerminalSupportedMaxNumberOfVideoPhysicalChannels { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ushort MsgId => 0x1003;
        /// <summary>
        /// 
        /// </summary>
        public string Description => "终端上传音视频属性";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x1003 value = new JT808_0x1003();
            value.EnterAudioEncoding = reader.ReadByte();
            writer.WriteString($"[{value.EnterAudioEncoding.ReadNumber()}]输入音频编码方式", AudioVideoEncodingDisplay(value.EnterAudioEncoding));
            value.EnterAudioChannelsNumber = reader.ReadByte();
            writer.WriteNumber($"[{value.EnterAudioChannelsNumber.ReadNumber()}]输入音频声道数", value.EnterAudioChannelsNumber);
            value.EnterAudioSampleRate = reader.ReadByte();
            writer.WriteString($"[{value.EnterAudioSampleRate.ReadNumber()}]输入音频采样率", AudioSampleRateDisplay(value.EnterAudioSampleRate));

            value.EnterAudioSampleDigits = reader.ReadByte();
            writer.WriteString($"[{value.EnterAudioSampleDigits.ReadNumber()}]输入音频采样位数", AudioSampleDigitsDisplay(value.EnterAudioSampleDigits));
            value.AudioFrameLength = reader.ReadUInt16();
            writer.WriteNumber($"[{value.AudioFrameLength.ReadNumber()}]音频帧长度", value.AudioFrameLength);
            value.IsSupportedAudioOutput = reader.ReadByte();
            writer.WriteString($"[{value.IsSupportedAudioOutput.ReadNumber()}]是否支持音频输出", value.IsSupportedAudioOutput==0?"不支持":"支持");

            value.VideoEncoding = reader.ReadByte();
            writer.WriteString($"[{value.VideoEncoding.ReadNumber()}]视频编码方式", AudioVideoEncodingDisplay(value.VideoEncoding));
            value.TerminalSupportedMaxNumberOfAudioPhysicalChannels = reader.ReadByte();
            writer.WriteNumber($"[{value.TerminalSupportedMaxNumberOfAudioPhysicalChannels.ReadNumber()}]终端支持的最大音频物理通道数量", value.TerminalSupportedMaxNumberOfAudioPhysicalChannels);
            value.TerminalSupportedMaxNumberOfVideoPhysicalChannels = reader.ReadByte();
            writer.WriteNumber($"[{value.TerminalSupportedMaxNumberOfVideoPhysicalChannels.ReadNumber()}]终端支持的最大视频物理通道数量", value.TerminalSupportedMaxNumberOfVideoPhysicalChannels);
            
            string AudioVideoEncodingDisplay(byte AudioVideoEncoding) {
                switch (AudioVideoEncoding)
                {
                    case 19:
                        return "AAC";
                    case 25:
                        return "MP3";
                    case 91:
                        return "透传";
                    case 98:
                        return "H.264";
                    case 99:
                        return "H.265";
                    default:
                        break;
                }
                return "未解析";
            }
            string AudioSampleRateDisplay(byte AudioSampleRate) {
                switch (AudioSampleRate)
                {
                    case 0:
                        return "8 kHz";
                    case 1:
                        return "22.05 kHz";
                    case 2:
                        return "44.1 kHz";
                    case 3:
                        return "48 kHz";
                    default:
                        break;
                }
                return "未知";
            }
            string AudioSampleDigitsDisplay(byte AudioSampleDigits) {
                switch (AudioSampleDigits)
                {
                    case 0:
                        return "8 位";
                    case 1:
                        return "16 位";
                    case 2:
                        return "32 位";
                    default:
                        break;
                }
                return "未知";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x1003 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x1003 jT808_0x1003 = new JT808_0x1003();
            jT808_0x1003.EnterAudioEncoding = reader.ReadByte();
            jT808_0x1003.EnterAudioChannelsNumber = reader.ReadByte();
            jT808_0x1003.EnterAudioSampleRate = reader.ReadByte();
            jT808_0x1003.EnterAudioSampleDigits = reader.ReadByte();
            jT808_0x1003.AudioFrameLength = reader.ReadUInt16();
            jT808_0x1003.IsSupportedAudioOutput = reader.ReadByte();
            jT808_0x1003.VideoEncoding = reader.ReadByte();
            jT808_0x1003.TerminalSupportedMaxNumberOfAudioPhysicalChannels = reader.ReadByte();
            jT808_0x1003.TerminalSupportedMaxNumberOfVideoPhysicalChannels = reader.ReadByte();
            return jT808_0x1003;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x1003 value, IJT808Config config)
        {
            writer.WriteByte(value.EnterAudioEncoding);
            writer.WriteByte(value.EnterAudioChannelsNumber);
            writer.WriteByte(value.EnterAudioSampleRate);
            writer.WriteByte(value.EnterAudioSampleDigits);
            writer.WriteUInt16(value.AudioFrameLength);
            writer.WriteByte(value.IsSupportedAudioOutput);
            writer.WriteByte(value.VideoEncoding);
            writer.WriteByte(value.TerminalSupportedMaxNumberOfAudioPhysicalChannels);
            writer.WriteByte(value.TerminalSupportedMaxNumberOfVideoPhysicalChannels);
        }
    }
}
