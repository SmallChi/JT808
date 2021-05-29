using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.YueBiao.Metadata
{
    /// <summary>
    /// 
    /// </summary>
    public class JT808_0x0900_0xF7_USB
    {
        /// <summary>
        /// 外设ID
        /// <see cref="JT808.Protocol.Extensions.YueBiao.Enums.USBIDType"/>
        /// </summary>
        public byte USBID { get; set; }
        /// <summary>
        /// 消息长度
        /// </summary>
        public byte MessageLength { get; set; }
        /// <summary>
        /// 工作状态
        /// <see cref="JT808.Protocol.Extensions.YueBiao.Enums.WorkingConditionType"/>
        /// </summary>
        public byte WorkingCondition { get; set; }
        /// <summary>
        /// 报警状态
        /// </summary>
        public uint AlarmStatus { get; set; }
    }
}
