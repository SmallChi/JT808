using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.YueBiao.Metadata
{
    /// <summary>
    /// 补传数据包信息
    /// </summary>
    public class DataPackageProperty
    {
        /// <summary>
        /// 数据偏移量
        /// </summary>
        public uint Offset { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public uint Length { get; set; }
    }
}
