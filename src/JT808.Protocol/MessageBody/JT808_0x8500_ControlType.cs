using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 控制类型
    /// </summary>
    public abstract class JT808_0x8500_ControlType : IJT808_2019_Version
    {
        public abstract ushort ControlTypeId { get; set; }
    }
}
