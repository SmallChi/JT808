using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 平台RSA公钥
    /// 0x8A00
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8A00_Formatter))]
    public class JT808_0x8A00 : JT808Bodies
    {
        /// <summary>
        /// e
        /// 平台 RSA 公钥{e,n}中的 e
        /// </summary>
        public uint E { get; set; }
        /// <summary>
        /// n
        /// RSA 公钥{e,n}中的 n
        /// </summary>
        public byte[] N { get; set; }
    }
}
