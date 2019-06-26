using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询终端参数应答
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0104_Formatter))]
    public class JT808_0x0104 : JT808Bodies
    {
        /// <summary>
        /// 应答流水号
        /// 查询指定终端参数的流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 应答参数个数
        /// </summary>
        public byte AnswerParamsCount { get; set; }
        /// <summary>
        /// 参数列表
        /// </summary>
        public IList<JT808_0x8103_BodyBase> ParamList { get; set; }
    }
}
