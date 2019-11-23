using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 录音开始命令
    /// </summary>
    public class JT808_0x8804 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8804>
    {
        public override ushort MsgId { get; } = 0x8804;
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

        public JT808_0x8804 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8804 jT808_0X8804 = new JT808_0x8804();
            jT808_0X8804.RecordCmd = (JT808RecordCmd)reader.ReadByte();
            jT808_0X8804.RecordTime = reader.ReadUInt16();
            jT808_0X8804.RecordSave = (JT808RecordSave)reader.ReadByte();
            jT808_0X8804.AudioSampleRate = reader.ReadByte();
            return jT808_0X8804;
        }
        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8804 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.RecordCmd);
            writer.WriteUInt16(value.RecordTime);
            writer.WriteByte((byte)value.RecordSave);
            writer.WriteByte(value.AudioSampleRate);
        }
    }
}
