using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端升级结果通知
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0108Formatter))]
    public class JT808_0x0108 : JT808Bodies
    {
        /// <summary>
        /// 升级类型
        /// 0：终端，12：道路运输证 IC 卡读卡器，52：北斗卫星定位模块
        /// </summary>
        public JT808UpgradeType UpgradeType { get; set; }

        /// <summary>
        /// 升级结果
        /// 0：成功，1：失败，2：取消
        /// </summary>
        public JT808UpgradeResult UpgradeResult { get; set; }
    }
}
