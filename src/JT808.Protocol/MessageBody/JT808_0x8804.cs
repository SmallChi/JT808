using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 录音开始命令
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8804_Formatter))]
    public class JT808_0x8804 : JT808Bodies
    {
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
    }
}
