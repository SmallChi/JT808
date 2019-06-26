using JT808.Protocol.Metadata;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 分包策略
    /// 注意:处理808的分包读取完流需要先进行转义在进行分包
    /// </summary>
    public interface IJT808SplitPackageStrategy
    {
        IEnumerable<JT808SplitPackageProperty> Processor(ReadOnlySpan<byte> bigData);
    }
}
