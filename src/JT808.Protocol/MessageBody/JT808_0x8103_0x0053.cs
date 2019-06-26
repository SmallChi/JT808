using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 报警拍摄存储标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警时拍的照片进行存储，否则实时上传
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0053_Formatter))]
    public class JT808_0x8103_0x0053 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0053;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 报警拍摄存储标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警时拍的照片进行存储，否则实时上传
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
