using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    public  class JT808CarDVRDownPackage
    {
        public const ushort BeginFlag = 0x557A;
        /// <summary>
        /// 起始字头
        /// </summary>
        public ushort Begin { get; set; } = BeginFlag;
        /// <summary>
        /// 命令字
        /// </summary>
        public byte CommandId { get; set; }
        /// <summary>
        /// 保留字段
        /// </summary>
        public byte KeepFields { get; set; } = 0x00;
        /// <summary>
        /// 数据块长度
        /// </summary>
        public ushort DataLength { get; set; }
        /// <summary>
        /// 记录仪体下行数据体
        /// </summary>
        public JT808CarDVRDownBodies Bodies { get; set; }
        /// <summary>
        /// 校验字
        /// </summary>
        public byte CheckCode { get; set; }
    }
}
