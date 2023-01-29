using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 录音开始命令
    /// </summary>
    public class JT808_0x8804 : JT808MessagePackFormatter<JT808_0x8804>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x8804
        /// </summary>
        public ushort MsgId => 0x8804;
        /// <summary>
        /// 录音开始命令
        /// </summary>
        public string Description => "录音开始命令";
        /// <summary>
        /// 录音命令
        /// 0：停止录音；0x01：开始录音；
        /// </summary>
        public JT808RecordCmd RecordCmd { get; set; }
        /// <summary>
        /// 单位为秒（s），0 表示一直录音
        /// </summary>
        public ushort RecordTime { get; set; }
        /// <summary>
        /// 保存标志
        /// 0：实时上传；1：保存
        /// </summary>
        public JT808RecordSave RecordSave { get; set; }
        /// <summary>
        /// 音频采样率
        /// 0：8K；1：11K；2：23K；3：32K；其他保留
        /// </summary>
        public byte AudioSampleRate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8804 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8804 jT808_0X8804 = new JT808_0x8804();
            jT808_0X8804.RecordCmd = (JT808RecordCmd)reader.ReadByte();
            jT808_0X8804.RecordTime = reader.ReadUInt16();
            jT808_0X8804.RecordSave = (JT808RecordSave)reader.ReadByte();
            jT808_0X8804.AudioSampleRate = reader.ReadByte();
            return jT808_0X8804;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8804 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.RecordCmd);
            writer.WriteUInt16(value.RecordTime);
            writer.WriteByte((byte)value.RecordSave);
            writer.WriteByte(value.AudioSampleRate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8804 value = new JT808_0x8804();
            value.RecordCmd = (JT808RecordCmd)reader.ReadByte();
            value.RecordTime = reader.ReadUInt16();
            value.RecordSave = (JT808RecordSave)reader.ReadByte();
            value.AudioSampleRate = reader.ReadByte();

            writer.WriteNumber($"[{ ((byte)(value.RecordCmd)).ReadNumber()}]录音命令-{value.RecordCmd.ToString()}", (byte)value.RecordCmd);
            writer.WriteNumber($"[{value.RecordTime.ReadNumber()}]单位为秒(s)", value.RecordTime);
            writer.WriteNumber($"[{((byte)value.RecordSave).ReadNumber()}]保存标志-{value.RecordSave.ToString()}", (byte)value.RecordSave);
            switch (value.AudioSampleRate)
            {
                case 0:
                    writer.WriteNumber($"[{value.AudioSampleRate.ReadNumber()}]音频采样率-8K", value.AudioSampleRate);
                    break;
                case 1:
                    writer.WriteNumber($"[{value.AudioSampleRate.ReadNumber()}]音频采样率-11K", value.AudioSampleRate);
                    break;
                case 2:
                    writer.WriteNumber($"[{value.AudioSampleRate.ReadNumber()}]音频采样率-23K", value.AudioSampleRate);
                    break;
                case 3:
                    writer.WriteNumber($"[{value.AudioSampleRate.ReadNumber()}]音频采样率-32K", value.AudioSampleRate);
                    break;
                default:
                    writer.WriteNumber($"[{value.AudioSampleRate.ReadNumber()}]音频采样率-保留", value.AudioSampleRate);
                    break;
            }
           
        }
    }
}
