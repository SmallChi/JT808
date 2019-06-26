using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据上传方式
    /// 0x00，本地存储，不上传（默认值）；
    /// 0x01，按时间间隔上传；
    /// 0x02，按距离间隔上传；
    /// 0x0B，按累计时间上传，达到传输时间后自动停止上传；
    /// 0x0C，按累计距离上传，达到距离后自动停止上传；
    /// 0x0D，按累计条数上传，达到上传条数后自动停止上传。
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0094_Formatter))]
    public class JT808_0x8103_0x0094 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0094;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// GNSS 模块详细定位数据上传方式
        /// 0x00，本地存储，不上传（默认值）；
        /// 0x01，按时间间隔上传；
        /// 0x02，按距离间隔上传；
        /// 0x0B，按累计时间上传，达到传输时间后自动停止上传；
        /// 0x0C，按累计距离上传，达到距离后自动停止上传；
        /// 0x0D，按累计条数上传，达到上传条数后自动停止上传。
        /// </summary>
        public byte ParamValue { get; set; }
    }
}
