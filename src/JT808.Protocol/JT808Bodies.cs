using JT808.Protocol.Interfaces;

namespace JT808.Protocol
{
    public abstract class JT808Bodies: IJT808Description
    {
        /// <summary>
        /// 跳过数据体序列化
        /// 默认不跳过
        /// 当数据体为空的时候，使用null作为空包感觉不适合，所以就算使用空包也需要new一下来表达意思。
        /// </summary>
        public virtual bool SkipSerialization { get; set; } = false;

        public abstract ushort MsgId { get;}

        public abstract string Description { get; }
    }
}
