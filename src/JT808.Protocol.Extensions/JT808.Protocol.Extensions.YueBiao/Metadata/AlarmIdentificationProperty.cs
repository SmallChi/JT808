using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.YueBiao.Metadata
{
    /// <summary>
    /// 报警标识号
    /// 报警附件信息消息数据格式
    /// </summary>
    public class AlarmIdentificationProperty
    {
        /// <summary>
        /// 终端ID
        /// 30
        /// </summary>
        public string TerminalId { get; set; }
        /// <summary>
        /// YY-MM-DD-hh-mm-ss
        /// BCD[6]
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public byte SN { get; set; }
        /// <summary>
        /// 附件数量
        /// </summary>
        public byte AttachCount { get; set; }
        /// <summary>
        /// 预留1
        /// </summary>
        public byte Retain1 { get; set; }
        /// <summary>
        /// 预留2
        /// </summary>
        public byte Retain2 { get; set; }
    }
}
