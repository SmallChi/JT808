using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端注册
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0100_Formatter))]
    public class JT808_0x0100 : JT808Bodies
    {
        /// <summary>
        /// 省域 ID
        /// 标示终端安装车辆所在的省域，0 保留，由平台取默
        /// 认值。省域 ID 采用 GB/T 2260 中规定的行政区划代
        /// 码六位中前两位
        /// </summary>
        public ushort AreaID { get; set; }

        /// <summary>
        /// 市县域 ID
        /// 标示终端安装车辆所在的市域和县域，0 保留，由平
        /// 台取默认值。市县域 ID 采用 GB/T 2260 中规定的行
        /// 政区划代码六位中后四位。
        /// </summary>
        public ushort CityOrCountyId { get; set; }

        /// <summary>
        /// 制造商 ID
        /// 5 个字节，终端制造商编码
        /// </summary>
        public string MakerId { get; set; }

        /// <summary>
        /// 终端型号
        /// 20 个字节，此终端型号由制造商自行定义，位数不
        /// 足时，后补“0X00”。
        /// </summary>
        public string TerminalModel { get; set; }

        /// <summary>
        /// 终端 ID
        /// 7 个字节，由大写字母和数字组成，此终端 ID 由制
        /// 造商自行定义，位数不足时，后补“0X00”。
        /// </summary>
        public string TerminalId { get; set; }

        /// <summary>
        /// 车牌颜色
        /// 车牌颜色，按照 JT/T415-2006 的 5.4.12。
        /// 未上牌时，取值为 0。
        /// </summary>
        public byte PlateColor { get; set; }

        /// <summary>
        /// 车辆标识
        /// 车牌颜色为 0 时，表示车辆 VIN；
        /// 否则，表示公安交通管理部门颁发的机动车号牌。
        /// </summary>
        public string PlateNo { get; set; }
    }
}
