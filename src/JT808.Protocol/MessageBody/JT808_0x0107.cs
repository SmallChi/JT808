using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询终端属性应答
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0107Formatter))]
    public class JT808_0x0107 : JT808Bodies
    {
        /// <summary>
        /// 终端类型
        /// bit0，0：不适用客运车辆，1：适用客运车辆；
        /// bit1，0：不适用危险品车辆，1：适用危险品车辆；
        /// bit2，0：不适用普通货运车辆，1：适用普通货运车辆；
        /// bit3，0：不适用出租车辆，1：适用出租车辆；
        /// bit6，0：不支持硬盘录像，1：支持硬盘录像；
        /// bit7，0：一体机，1：分体机
        /// </summary>
        public ushort TerminalType { get; set; }
        /// <summary>
        /// 制造商 ID
        /// 5 个字节，终端制造商编码
        /// </summary>
        public string MakerId { get; set; }
        /// <summary>
        /// 终端型号
        /// BYTE[20]
        /// 20 个字节，此终端型号由制造商自行定义，位数不足时，后补“0X00”。
        /// </summary>
        public string TerminalModel { get; set; }
        /// <summary>
        /// 终端ID 
        /// BYTE[7]
        /// 7 个字节，由大写字母和数字组成，此终端 ID 由制造商自行定义，位数不足时，后补“0X00”
        /// </summary>
        public string TerminalId { get; set; }
        /// <summary>
        /// 终端 SIM 卡 ICCID 
        /// BCD[10]
        /// </summary>
        public string Terminal_SIM_ICCID { get; set; }
        /// <summary>
        /// 终端硬件版本号长度
        /// </summary>
        public byte Terminal_Hardware_Version_Length { get; set; }
        /// <summary>
        /// 终端硬件版本号
        /// </summary>
        public string Terminal_Hardware_Version_Num { get; set; }
        /// <summary>
        /// 终端固件版本号长度
        /// </summary>
        public byte Terminal_Firmware_Version_Length { get; set; }
        /// <summary>
        /// 终端固件版本号
        /// </summary>
        public string Terminal_Firmware_Version_Num { get; set; }
        /// <summary>
        /// GNSS 模块属性
        /// bit0，0：不支持 GPS 定位， 1：支持 GPS 定位；
        /// bit1，0：不支持北斗定位， 1：支持北斗定位；
        /// bit2，0：不支持 GLONASS 定位， 1：支持 GLONASS 定位；
        /// bit3，0：不支持 Galileo 定位， 1：支持 Galileo 定位
        /// </summary>
        public byte GNSSModule { get; set; }
        /// <summary>
        /// 通信模块属性
        /// bit0，0：不支持GPRS通信， 1：支持GPRS通信；
        /// bit1，0：不支持CDMA通信， 1：支持CDMA通信；
        /// bit2，0：不支持TD-SCDMA通信， 1：支持TD-SCDMA通信；
        /// bit3，0：不支持WCDMA通信， 1：支持WCDMA通信；
        /// bit4，0：不支持CDMA2000通信， 1：支持CDMA2000通信。
        /// bit5，0：不支持TD-LTE通信， 1：支持TD-LTE通信；
        /// bit7，0：不支持其他通信方式， 1：支持其他通信方式
        /// </summary>
        public byte CommunicationModule { get; set; }
    }
}
