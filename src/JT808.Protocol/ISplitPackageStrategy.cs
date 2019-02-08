using JT808.Protocol.JT808Properties;
using System;
using System.Collections.Generic;

namespace JT808.Protocol
{
    /// <summary>
    /// 分包策略
    /// 注意:处理808的分包读取完流需要先进行转义在进行分包
    /// </summary>
    public interface ISplitPackageStrategy
    {
        IEnumerable<JT808SplitPackageProperty> Processor(ReadOnlySpan<byte> bigData);
    }
}
