using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Properties
{
    /// <summary>
    /// Can属性
    /// </summary>
    public class JT808CanProperty
    {
        /// <summary>
        /// CAN ID
        /// </summary>
        public byte[] CanId { get; set; }
        /// <summary>
        /// CAN 数据
        /// </summary>
        public byte[] CanData{ get; set; }
    }
}
