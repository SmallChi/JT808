using JT808.Protocol.Interfaces;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808抽象数据体
    /// </summary>
    public abstract class JT808Bodies: IJT808Description
    {
        /// <summary>
        /// 跳过数据体序列化
        /// 默认不跳过
        /// 当数据体为空的时候，使用null作为空包感觉不适合，所以就算使用空包也需要new一下来表达意思。
        /// </summary>
        public virtual bool SkipSerialization { get; set; } = false;
        /// <summary>
        /// 消息Id
        /// </summary>
        public abstract ushort MsgId { get;}
        /// <summary>
        /// 消息描述
        /// </summary>
        public abstract string Description { get; }
    }
}
