using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Extensions.GPS51
{
    /// <summary>
    /// 主动安全常量
    /// </summary>
    public static class JT808_GPS51_Constants
    {
        /// <summary>
        /// 附加信息ID 多路油耗模拟量,
        /// 2*N
        /// 例子报文:2b049203a46f 
        /// 解析结果为：2路 模拟量分别为37379 42095
        /// </summary>
        public const byte JT808_0x0200_0x2B = 0x2b;
        /// <summary>
        /// 附加信息ID 2个字节一组温度 0.1度 ffff 代表无效，
        /// 2*N
        /// 04F6代表未接或者传感器掉电，
        /// 第16位代表正负温度 51080134011A04F604F6 
        /// 308=30.8度 282=28.2度 8050=-80=-8度
        /// </summary>
        public const byte JT808_0x0200_0x51 = 0x51;
        /// <summary>
        /// 附加信息ID 
        /// 1
        /// 正反转 0:未知；1：正转（空车）2:反转（重车）；3：停转 
        /// 例子解析为：03
        /// </summary>
        public const byte JT808_0x0200_0x52 = 0x52;
        /// <summary>
        /// 附加信息ID 
        /// 1+7*N
        /// Wifi数据：第1个字节wifi个数，后面为n个wifi数据；
        /// WIFI数据：6字节 wifiMac 1字节 信号强度
        /// </summary>
        public const byte JT808_0x0200_0x54 = 0x54;
        /// <summary>
        /// 载重扩展 8
        /// 1/10千克 8 字节 
        /// </summary>
        public const uint JT808_0x0200_0x55 = 0x55;
        /// <summary>
        /// 湿度，
        /// 2*N
        /// 精度0.1，0fff 代表无效数据，例子数据： 0012 表示：1.8%
        /// </summary>
        public const uint JT808_0x0200_0x58 = 0x58;
        /// <summary>
        /// 电压,
        /// 2
        /// 单位0.01V,例子报文：61021d74，解析结果7540，最终电压75.40V
        /// </summary>
        public const uint JT808_0x0200_0x61 = 0x61;
        /// <summary>
        /// 基站编码
        /// 4+7*N
        /// 的格式为 MCC MNC LAC CI Signal 2-2-2-4-1-2-4-1，
        /// 其中MCC 2个字节国家编码，MNC 为 2个字节网络编码，LAC为 2个字节地区编码,
        /// CI 为 4个字节蜂窝 ID ,
        /// 信号强度 1字节,
        /// 单基站可以不用信号强度 1cc-0-696a-863a8d0-0
        /// </summary>
        public const uint JT808_0x0200_0xe1 = 0xe1;
        /// <summary>
        /// 版本号,
        /// N 
        /// 开机或者重连第一条上报,例子结果:GB201-GSM-21001-1.1.1
        /// </summary>
        public const byte JT808_0x0200_0xe2 = 0xe2;
        /// <summary>
        /// iccid,
        /// 20
        /// 一般开机或者重连第一条0200位置信息上报,
        /// 例子报文：f1143839383630343032313032303930393737303032,
        /// 解析结果为:89860402102090977002
        /// </summary>
        public const byte JT808_0x0200_0xf1 = 0xf1;
        /// <summary>
        /// IMEI数据：
        /// 8字节，
        /// 第1位为0，后面15位为imei的16进制数据 
        /// </summary>
        public const byte JT808_0x0200_0xf6 = 0xf6;
        /// <summary>
        /// 4
        /// 第0位:震动报警 
        /// 第1位:拆除报警 例子:第0位:震动报警 fa0400000001 第1位:拆除报警 fa0400000002 
        /// 第2位:进入深度休眠 fa0400000004 
        /// 第3位:急加速 fa0400000008 
        /// 第4位:急减速 fa0400000010 
        /// 第5位:急转弯 fa0400000020 
        /// 第6位:acc开报警 fa0400000040 
        /// 第7位:acc关报警 fa0400000080 
        /// 第8位:内部电池电量低fa0400000100 
        /// 第9位:人为关机
        /// 第10位:低电关机
        /// </summary>
        public const byte JT808_0x0200_0xfa = 0xfa;
        /// <summary>
        /// 4
        /// 电量百分比和外部电压,
        /// 电压精度0.01V,充电状态0未充电 1充电中,没有的数据传00 
        /// 例子:fb045F050701 
        /// 解析结果:电量百分比5F=95 电压:0507=1287 最终显示为12.87V 01:充电中
        /// </summary>
        public const byte JT808_0x0200_0xfb = 0xfb;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Assembly GetCurrentAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
