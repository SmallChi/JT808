using JT808.Protocol.Interfaces;

namespace JT808.Protocol
{
    /// <summary>
    /// 记录仪上行数据体
    /// </summary>
    public interface JT808CarDVRDownBodies : IJT808Description
    {
        /// <summary>
        /// 命令字
        /// </summary>
        byte CommandId { get; }
         /// <summary>
        /// 跳过数据体序列化
        /// 默认不跳过
        /// 当数据体为空的时候，使用null作为空包感觉不适合，所以就算使用空包也需要new一下来表达意思。
        /// </summary>
        bool SkipSerialization { get { return false; }}
    }
}
