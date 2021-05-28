using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808常量
    /// </summary>
    public static class JT808Constants
    {
        static JT808Constants()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding = Encoding.GetEncoding("GBK");
        }
        /// <summary>
        /// 日期限制于2000年
        /// </summary>
        public const int DateLimitYear = 2000;
        /// <summary>
        /// 
        /// </summary>
        public static readonly DateTime UTCBaseTime = new DateTime(1970, 1, 1);
        /// <summary>
        /// 
        /// </summary>
        public static Encoding Encoding { get;}
        /// <summary>
        /// JT808_0x0200_0x01
        /// </summary>

        public const byte JT808_0x0200_0x01 = 0x01;
        /// <summary>
        /// JT808_0x0200_0x02
        /// </summary>
        public const byte JT808_0x0200_0x02 = 0x02;
        /// <summary>
        /// JT808_0x0200_0x03
        /// </summary>
        public const byte JT808_0x0200_0x03 = 0x03;
        /// <summary>
        /// JT808_0x0200_0x04
        /// </summary>
        public const byte JT808_0x0200_0x04 = 0x04;
        /// <summary>
        /// JT808_0x0200_0x05
        /// </summary>
        public const byte JT808_0x0200_0x05 = 0x05;
        /// <summary>
        /// JT808_0x0200_0x06
        /// </summary>
        public const byte JT808_0x0200_0x06 = 0x06;
        /// <summary>
        /// JT808_0x0200_0x07
        /// </summary>
        public const byte JT808_0x0200_0x07 = 0x07;
        /// <summary>
        /// JT808_0x0200_0x11
        /// </summary>
        public const byte JT808_0x0200_0x11 = 0x11;
        /// <summary>
        /// JT808_0x0200_0x12
        /// </summary>
        public const byte JT808_0x0200_0x12 = 0x12;
        /// <summary>
        /// JT808_0x0200_0x13
        /// </summary>
        public const byte JT808_0x0200_0x13 = 0x13;
        /// <summary>
        /// JT808_0x0200_0x25
        /// </summary>
        public const byte JT808_0x0200_0x25 = 0x25;
        /// <summary>
        /// JT808_0x0200_0x2A
        /// </summary>
        public const byte JT808_0x0200_0x2A = 0x2A;
        /// <summary>
        /// JT808_0x0200_0x2B
        /// </summary>
        public const byte JT808_0x0200_0x2B = 0x2B;
        /// <summary>
        /// JT808_0x0200_0x30
        /// </summary>
        public const byte JT808_0x0200_0x30 = 0x30;
        /// <summary>
        /// JT808_0x0200_0x31
        /// </summary>
        public const byte JT808_0x0200_0x31 = 0x31;
        /// <summary>
        /// 终端心跳发送间隔，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0001 = 0x0001;
        /// <summary>
        /// TCP 消息应答超时时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0002 = 0x0002;
        /// <summary>
        /// TCP 消息重传次数
        /// </summary>
        public const uint JT808_0x8103_0x0003 = 0x0003;
        /// <summary>
        /// UDP 消息应答超时时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0004 = 0x0004;
        /// <summary>
        ///  UDP 消息重传次数
        /// </summary>
        public const uint JT808_0x8103_0x0005 = 0x0005;
        /// <summary>
        /// SMS 消息应答超时时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0006 = 0x0006;
        /// <summary>
        /// SMS 消息重传次数
        /// </summary>
        public const uint JT808_0x8103_0x0007 = 0x0007;
        /// <summary>
        /// 主服务器 APN，无线通信拨号访问点。若网络制式为 CDMA，则该处为PPP 拨号号码
        /// </summary>
        public const uint JT808_0x8103_0x0010 = 0x0010;
        /// <summary>
        /// 主服务器无线通信拨号用户名
        /// </summary>
        public const uint JT808_0x8103_0x0011 = 0x0011;
        /// <summary>
        /// 主服务器无线通信拨号密码
        /// </summary>
        public const uint JT808_0x8103_0x0012 = 0x0012;
        /// <summary>
        /// 主服务器地址,IP 或域名
        /// </summary>
        public const uint JT808_0x8103_0x0013 = 0x0013;
        /// <summary>
        /// 主服务器地址,IP 或域名
        /// </summary>
        public const uint JT808_0x8103_0x0014 = 0x0014;
        /// <summary>
        /// 备份服务器无线通信拨号用户名
        /// </summary>
        public const uint JT808_0x8103_0x0015 = 0x0015;
        /// <summary>
        /// 备份服务器无线通信拨号密码
        /// </summary>
        public const uint JT808_0x8103_0x0016 = 0x0016;
        /// <summary>
        /// 备份服务器地址,IP 或域名
        /// </summary>
        public const uint JT808_0x8103_0x0017 = 0x0017;
        /// <summary>
        /// 服务器 TCP 端口
        /// </summary>
        public const uint JT808_0x8103_0x0018 = 0x0018;
        /// <summary>
        /// 服务器 UDP 端口
        /// </summary>
        public const uint JT808_0x8103_0x0019 = 0x0019;
        /// <summary>
        /// 道路运输证 IC 卡认证主服务器 IP 地址或域名
        /// </summary>
        public const uint JT808_0x8103_0x001A = 0x001A;
        /// <summary>
        /// 道路运输证 IC 卡认证主服务器 TCP 端口
        /// </summary>
        public const uint JT808_0x8103_0x001B = 0x001B;
        /// <summary>
        /// 道路运输证 IC 卡认证主服务器 UDP 端口
        /// </summary>
        public const uint JT808_0x8103_0x001C = 0x001C;
        /// <summary>
        /// 道路运输证 IC 卡认证备份服务器 IP 地址或域名，端口同主服务器
        /// </summary>
        public const uint JT808_0x8103_0x001D = 0x001D;
        /// <summary>
        /// 位置汇报策略，0：定时汇报；1：定距汇报；2：定时和定距汇报
        /// </summary>
        public const uint JT808_0x8103_0x0020 = 0x0020;
        /// <summary>
        /// 位置汇报方案，0：根据 ACC 状态； 1：根据登录状态和 ACC 状态，先判断登录状态，若登录再根据 ACC 状态
        /// </summary>
        public const uint JT808_0x8103_0x0021 = 0x0021;
        /// <summary>
        /// 驾驶员未登录汇报时间间隔，单位为秒（s），>0
        /// </summary>
        public const uint JT808_0x8103_0x0022 = 0x0022;
        /// <summary>
        /// 休眠时汇报时间间隔，单位为秒（s），>0
        /// </summary>
        public const uint JT808_0x8103_0x0027 = 0x0027;
        /// <summary>
        /// 紧急报警时汇报时间间隔，单位为秒（s），>0
        /// </summary>
        public const uint JT808_0x8103_0x0028 = 0x0028;
        /// <summary>
        /// 缺省时间汇报间隔，单位为秒（s），>0
        /// </summary>
        public const uint JT808_0x8103_0x0029 = 0x0029;
        /// <summary>
        /// 缺省距离汇报间隔，单位为米（m），>0
        /// </summary>
        public const uint JT808_0x8103_0x002C = 0x002C;
        /// <summary>
        /// 驾驶员未登录汇报距离间隔，单位为米（m），>0
        /// </summary>
        public const uint JT808_0x8103_0x002D = 0x002D;
        /// <summary>
        /// 休眠时汇报距离间隔，单位为米（m），>0
        /// </summary>
        public const uint JT808_0x8103_0x002E = 0x002E;
        /// <summary>
        /// 紧急报警时汇报距离间隔，单位为米（m）>0
        /// </summary>
        public const uint JT808_0x8103_0x002F = 0x002F;
        /// <summary>
        /// 拐点补传角度小于180
        /// </summary>
        public const uint JT808_0x8103_0x0030 = 0x0030;
        /// <summary>
        /// 电子围栏半径（非法位移阈值），单位为米
        /// </summary>
        public const uint JT808_0x8103_0x0031 = 0x0031;
        /// <summary>
        /// 电子围栏半径（非法位移阈值），单位为米
        /// </summary>
        public const uint JT808_0x8103_0x0032 = 0x0032;        
        /// <summary>
        /// 监控平台电话号码
        /// </summary>
        public const uint JT808_0x8103_0x0040 = 0x0040;
        /// <summary>
        /// 复位电话号码，可采用此电话号码拨打终端电话让终端复位
        /// </summary>
        public const uint JT808_0x8103_0x0041 = 0x0041;
        /// <summary>
        /// 恢复出厂设置电话号码，可采用此电话号码拨打终端电话让终端恢复出厂设置
        /// </summary>
        public const uint JT808_0x8103_0x0042 = 0x0042;
        /// <summary>
        /// 监控平台 SMS 电话号码
        /// </summary>
        public const uint JT808_0x8103_0x0043 = 0x0043;
        /// <summary>
        /// 接收终端 SMS 文本报警号码
        /// </summary>
        public const uint JT808_0x8103_0x0044 = 0x0044;
        /// <summary>
        /// 终端电话接听策略，0：自动接听；1：ACC ON 时自动接听，OFF 时手动接听
        /// </summary>
        public const uint JT808_0x8103_0x0045 = 0x0045;
        /// <summary>
        /// 每次最长通话时间，单位为秒（s），0 为不允许通话，0xFFFFFFFF 为不限制
        /// </summary>
        public const uint JT808_0x8103_0x0046 = 0x0046;
        /// <summary>
        /// 当月最长通话时间，单位为秒（s），0 为不允许通话，0xFFFFFFFF 为不限制
        /// </summary>
        public const uint JT808_0x8103_0x0047 = 0x0047;
        /// <summary>
        /// 监听电话号码
        /// </summary>
        public const uint JT808_0x8103_0x0048 = 0x0048;
        /// <summary>
        /// 监管平台特权短信号码
        /// </summary>
        public const uint JT808_0x8103_0x0049 = 0x0049;
        /// <summary>
        /// 报警屏蔽字，与位置信息汇报消息中的报警标志相对应，相应位为 1则相应报警被屏蔽
        /// </summary>
        public const uint JT808_0x8103_0x0050 = 0x0050;
        /// <summary>
        /// 报警发送文本 SMS 开关，与位置信息汇报消息中的报警标志相对应，相应位为 1 则相应报警时发送文本 SMS
        /// </summary>
        public const uint JT808_0x8103_0x0051 = 0x0051;
        /// <summary>
        /// 报警拍摄开关，与位置信息汇报消息中的报警标志相对应，相应位为1 则相应报警时摄像头拍摄
        /// </summary>
        public const uint JT808_0x8103_0x0052 = 0x0052;
        /// <summary>
        /// 报警拍摄存储标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警时拍的照片进行存储，否则实时上传
        /// </summary>
        public const uint JT808_0x8103_0x0053 = 0x0053;
        /// <summary>
        /// 关键标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警为关键报警
        /// </summary>
        public const uint JT808_0x8103_0x0054 = 0x0054;
        /// <summary>
        /// 最高速度，单位为公里每小时（km/h）
        /// </summary>
        public const uint JT808_0x8103_0x0055 = 0x0055;
        /// <summary>
        /// 超速持续时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0056 = 0x0056;
        /// <summary>
        /// 连续驾驶时间门限，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0057 = 0x0057;
        /// <summary>
        /// 当天累计驾驶时间门限，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0058 = 0x0058;
        /// <summary>
        /// 最小休息时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0059 = 0x0059;
        /// <summary>
        /// 最长停车时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x005A = 0x005A;
        /// <summary>
        /// 超速报警预警差值，单位为 1/10Km/h
        /// </summary>
        public const uint JT808_0x8103_0x005B = 0x005B;
        /// <summary>
        /// 疲劳驾驶预警差值，单位为秒（s），>0
        /// </summary>
        public const uint JT808_0x8103_0x005C = 0x005C;
        /// <summary>
        /// 碰撞报警参数设置
        /// b7-b0： 碰撞时间，单位 4ms；
        /// b15-b8：碰撞加速度，单位 0.1g，设置范围在：0-79 之间，默认为10。
        /// </summary>
        public const uint JT808_0x8103_0x005D = 0x005D;
        /// <summary>
        /// 侧翻报警参数设置：
        /// 侧翻角度，单位 1 度，默认为 30 度
        /// </summary>
        public const uint JT808_0x8103_0x005E = 0x005E;
        /// <summary>
        /// 定时拍照控制，见 表 13
        /// </summary>
        public const uint JT808_0x8103_0x0064 = 0x0064;
        /// <summary>
        /// 定距拍照控制，见 表 14
        /// </summary>
        public const uint JT808_0x8103_0x0065 = 0x0065;
        /// <summary>
        /// 图像/视频质量，1-10，1 最好
        /// </summary>
        public const uint JT808_0x8103_0x0070 = 0x0070;
        /// <summary>
        /// 亮度，0-255
        /// </summary>
        public const uint JT808_0x8103_0x0071 = 0x0071;
        /// <summary>
        /// 对比度，0-127
        /// </summary>
        public const uint JT808_0x8103_0x0072 = 0x0072;
        /// <summary>
        /// 饱和度，0-127
        /// </summary>
        public const uint JT808_0x8103_0x0073 = 0x0073;
        /// <summary>
        /// 色度，0-255
        /// </summary>
        public const uint JT808_0x8103_0x0074 = 0x0074;
        /// <summary>
        /// 车辆里程表读数，1/10km
        /// </summary>
        public const uint JT808_0x8103_0x0080 = 0x0080;
        /// <summary>
        /// 车辆所在的省域 ID
        /// </summary>
        public const uint JT808_0x8103_0x0081 = 0x0081;
        /// <summary>
        /// 车辆所在的市域 ID
        /// </summary>
        public const uint JT808_0x8103_0x0082 = 0x0082;
        /// <summary>
        /// 公安交通管理部门颁发的机动车号牌
        /// </summary>
        public const uint JT808_0x8103_0x0083 = 0x0083;
        /// <summary>
        /// 车牌颜色，按照 JT/T415-2006 的 5.4.12
        /// </summary>
        public const uint JT808_0x8103_0x0084 = 0x0084;
        /// <summary>
        /// GNSS 定位模式，定义如下：
        /// bit0，0：禁用 GPS 定位， 1：启用 GPS 定位；
        /// bit1，0：禁用北斗定位， 1：启用北斗定位；
        /// bit2，0：禁用 GLONASS 定位， 1：启用 GLONASS 定位；
        /// bit3，0：禁用 Galileo 定位， 1：启用 Galileo 定位。
        /// </summary>
        public const uint JT808_0x8103_0x0090 = 0x0090;
        /// <summary>
        /// GNSS 波特率，定义如下：
        /// 0x00：4800；0x01：9600；
        /// 0x02：19200；0x03：38400；
        /// 0x04：57600；0x05：115200。
        /// </summary>
        public const uint JT808_0x8103_0x0091 = 0x0091;
        /// <summary>
        /// GNSS 模块详细定位数据输出频率，定义如下：
        /// 0x00：500ms；0x01：1000ms（默认值）；
        /// 0x02：2000ms；0x03：3000ms；
        /// 0x04：4000ms。
        /// </summary>
        public const uint JT808_0x8103_0x0092 = 0x0092;
        /// <summary>
        /// GNSS 模块详细定位数据采集频率，单位为秒，默认为 1。
        /// </summary>
        public const uint JT808_0x8103_0x0093 = 0x0093;
        /// <summary>
        /// GNSS 模块详细定位数据上传方式
        /// 0x00，本地存储，不上传（默认值）；
        /// 0x01，按时间间隔上传；
        /// 0x02，按距离间隔上传；
        /// 0x0B，按累计时间上传，达到传输时间后自动停止上传；
        /// 0x0C，按累计距离上传，达到距离后自动停止上传；
        /// 0x0D，按累计条数上传，达到上传条数后自动停止上传。
        /// </summary>
        public const uint JT808_0x8103_0x0094 = 0x0094;
        /// <summary>
        /// GNSS 模块详细定位数据上传设置：
        /// 上传方式为 0x01 时，单位为秒；
        /// 上传方式为 0x02 时，单位为米；
        /// 上传方式为 0x0B 时，单位为秒；
        /// 上传方式为 0x0C 时，单位为米；
        /// 上传方式为 0x0D 时，单位为条。
        /// </summary>
        public const uint JT808_0x8103_0x0095 = 0x0095;
        /// <summary>
        /// CAN 总线通道 1 采集时间间隔(ms)，0 表示不采集
        /// </summary>
        public const uint JT808_0x8103_0x0100 = 0x0100;
        /// <summary>
        /// CAN 总线通道 1 上传时间间隔(s)，0 表示不上传
        /// </summary>
        public const uint JT808_0x8103_0x0101 = 0x0101;
        /// <summary>
        /// CAN 总线通道 2 采集时间间隔(ms)，0 表示不采集
        /// </summary>
        public const uint JT808_0x8103_0x0102 = 0x0102;
        /// <summary>
        /// CAN 总线通道 2 上传时间间隔(s)，0 表示不上传
        /// </summary>
        public const uint JT808_0x8103_0x0103 = 0x0103;
        /// <summary>
        /// CAN 总线 ID 单独采集设置：
        /// bit63-bit32 表示此 ID 采集时间间隔(ms)，0 表示不采集；
        /// bit31 表示 CAN 通道号，0：CAN1，1：CAN2；
        /// bit30 表示帧类型，0：标准帧，1：扩展帧；
        /// bit29 表示数据采集方式，0：原始数据，1：采集区间的计算值；
        /// bit28-bit0 表示 CAN 总线 ID。
        /// </summary>
        public const uint JT808_0x8103_0x0110 = 0x0110;
    }
}
