using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据上传设置：
    /// 上传方式为 0x01 时，单位为秒；
    /// 上传方式为 0x02 时，单位为米；
    /// 上传方式为 0x0B 时，单位为秒；
    /// 上传方式为 0x0C 时，单位为米；
    /// 上传方式为 0x0D 时，单位为条。
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0095_Formatter))]
    public class JT808_0x8103_0x0095 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0095;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// GNSS 模块详细定位数据上传设置：
        /// 上传方式为 0x01 时，单位为秒；
        /// 上传方式为 0x02 时，单位为米；
        /// 上传方式为 0x0B 时，单位为秒；
        /// 上传方式为 0x0C 时，单位为米；
        /// 上传方式为 0x0D 时，单位为条。
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
