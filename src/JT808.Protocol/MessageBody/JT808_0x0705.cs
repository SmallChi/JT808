using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Properties;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// CAN 总线数据上传
    /// 0x0705
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0705Formatter))]
    public class JT808_0x0705 : JT808Bodies
    {
        /// <summary>
        /// 数据项个数
        /// 包含的 CAN 总线数据项个数，>0
        /// </summary>
        public ushort CanItemCount { get; set; }
        /// <summary>
        /// CAN 总线数据接收时间
        /// 第 1 条 CAN 总线数据的接收时间，hh-mm-ss-msms
        /// </summary>
        public DateTime FirstCanReceiveTime { get; set; }
        /// <summary>
        /// CAN 总线数据项
        /// </summary>
        public List<JT808CanProperty> CanItems { get; set; }
    }
}
