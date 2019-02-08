using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 设置终端参数
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103Formatter))]
    public class JT808_0x8103 : JT808Bodies
    {
        /// <summary>
        /// 参数总数
        /// </summary>
        internal byte ParamCount
        {
            get
            {
                return (byte)ParamList.Count;
            }
        }
        /// <summary>
        /// 参数列表
        /// </summary>
        public List<JT808_0x8103_BodyBase> ParamList { get; set; }
    }
}
