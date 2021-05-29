using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.YueBiao.Metadata
{
    /// <summary>
    /// 附件信息
    /// </summary>
    public class AttachProperty
    {
        /// <summary>
        /// 文件名称长度
        /// </summary>
        public byte FileNameLength { get; set; }
        /// <summary>
        /// 文件名称
        /// 形如：文件类型_通道号_报警类型_序号_报警编号.后缀名
        /// </summary>
        public string FileName{ get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public uint FileSize { get; set; }
    }
}
