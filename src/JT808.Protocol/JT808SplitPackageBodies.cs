using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;

namespace JT808.Protocol
{
    /// <summary>
    /// 统一分包数据体
    /// </summary>
    [JT808Formatter(typeof(JT808SplitPackageBodiesFormatter))]
    public class JT808SplitPackageBodies : JT808Bodies
    {
        public byte[] Data { get; set; }
    }
}
