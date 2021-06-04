using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.SuBiao.Metadata
{
    /// <summary>
    /// 报警标识号
    /// </summary>
    public class AlarmIdentificationProperty
    {
        /// <summary>
        /// 终端ID
        /// </summary>
        public string TerminalID { get; set; }
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
        /// 预留
        /// </summary>
        public byte Retain { get; set; }
    }
}
