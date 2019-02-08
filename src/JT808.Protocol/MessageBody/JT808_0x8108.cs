using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 下发终端升级包
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8108Formatter))]
    public class JT808_0x8108 : JT808Bodies
    {
        /// <summary>
        /// 升级类型
        /// </summary>
        public JT808UpgradeType UpgradeType { get; set; }
        /// <summary>
        /// 制造商 ID
        /// 5 个字节，终端制造商编码
        /// </summary>
        public string MakerId { get; set; }
        /// <summary>
        /// 版本号长度
        /// </summary>
        public byte VersionNumLength { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string VersionNum { get; set; }
        /// <summary>
        /// 升级数据包长度
        /// </summary>
        public int UpgradePackageLength { get; set; }
        /// <summary>
        /// 升级数据包
        /// </summary>
        public byte[] UpgradePackage { get; set; }
    }
}
