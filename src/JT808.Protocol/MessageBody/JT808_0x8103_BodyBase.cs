using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    public abstract class JT808_0x8103_BodyBase
    {
        /// <summary>
        /// 终端心跳发送间隔，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0001_Type = 0x0001;
        /// <summary>
        /// TCP 消息应答超时时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0002_Type = 0x0002;
        /// <summary>
        /// TCP 消息重传次数
        /// </summary>
        public const uint JT808_0x8103_0x0003_Type = 0x0003;
        /// <summary>
        /// UDP 消息应答超时时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0004_Type = 0x0004;
        /// <summary>
        ///  UDP 消息重传次数
        /// </summary>
        public const uint JT808_0x8103_0x0005_Type = 0x0005;
        /// <summary>
        /// SMS 消息应答超时时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0006_Type = 0x0006;
        /// <summary>
        /// SMS 消息重传次数
        /// </summary>
        public const uint JT808_0x8103_0x0007_Type = 0x0007;
        /// <summary>
        /// 主服务器 APN，无线通信拨号访问点。若网络制式为 CDMA，则该处为PPP 拨号号码
        /// </summary>
        public const uint JT808_0x8103_0x0010_Type = 0x0010;
        /// <summary>
        /// 主服务器无线通信拨号用户名
        /// </summary>
        public const uint JT808_0x8103_0x0011_Type = 0x0011;
        /// <summary>
        /// 主服务器无线通信拨号密码
        /// </summary>
        public const uint JT808_0x8103_0x0012_Type = 0x0012;
        /// <summary>
        /// 主服务器地址,IP 或域名
        /// </summary>
        public const uint JT808_0x8103_0x0013_Type = 0x0013;
        /// <summary>
        /// 主服务器地址,IP 或域名
        /// </summary>
        public const uint JT808_0x8103_0x0014_Type = 0x0014;
        /// <summary>
        /// 备份服务器无线通信拨号用户名
        /// </summary>
        public const uint JT808_0x8103_0x0015_Type = 0x0015;
        /// <summary>
        /// 备份服务器无线通信拨号密码
        /// </summary>
        public const uint JT808_0x8103_0x0016_Type = 0x0016;
        /// <summary>
        /// 备份服务器地址,IP 或域名
        /// </summary>
        public const uint JT808_0x8103_0x0017_Type = 0x0017;
        /// <summary>
        /// 服务器 TCP 端口
        /// </summary>
        public const uint JT808_0x8103_0x0018_Type = 0x0018;
        /// <summary>
        /// 服务器 UDP 端口
        /// </summary>
        public const uint JT808_0x8103_0x0019_Type = 0x0019;
        /// <summary>
        /// 道路运输证 IC 卡认证主服务器 IP 地址或域名
        /// </summary>
        public const uint JT808_0x8103_0x001A_Type = 0x001A;
        /// <summary>
        /// 道路运输证 IC 卡认证主服务器 TCP 端口
        /// </summary>
        public const uint JT808_0x8103_0x001B_Type = 0x001B;
        /// <summary>
        /// 道路运输证 IC 卡认证主服务器 UDP 端口
        /// </summary>
        public const uint JT808_0x8103_0x001C_Type = 0x001C;
        /// <summary>
        /// 道路运输证 IC 卡认证备份服务器 IP 地址或域名，端口同主服务器
        /// </summary>
        public const uint JT808_0x8103_0x001D_Type = 0x001D;
        /// <summary>
        /// 位置汇报策略，0：定时汇报；1：定距汇报；2：定时和定距汇报
        /// </summary>
        public const uint JT808_0x8103_0x0020_Type = 0x0020;
        /// <summary>
        /// 位置汇报方案，0：根据 ACC 状态； 1：根据登录状态和 ACC 状态，先判断登录状态，若登录再根据 ACC 状态
        /// </summary>
        public const uint JT808_0x8103_0x0021_Type = 0x0021;
        /// <summary>
        /// 驾驶员未登录汇报时间间隔，单位为秒（s），>0
        /// </summary>
        public const uint JT808_0x8103_0x0022_Type = 0x0022;
        /// <summary>
        /// 休眠时汇报时间间隔，单位为秒（s），>0
        /// </summary>
        public const uint JT808_0x8103_0x0027_Type = 0x0027;
        /// <summary>
        /// 紧急报警时汇报时间间隔，单位为秒（s），>0
        /// </summary>
        public const uint JT808_0x8103_0x0028_Type = 0x0028;
        /// <summary>
        /// 缺省时间汇报间隔，单位为秒（s），>0
        /// </summary>
        public const uint JT808_0x8103_0x0029_Type = 0x0029;
        /// <summary>
        /// 缺省距离汇报间隔，单位为米（m），>0
        /// </summary>
        public const uint JT808_0x8103_0x002C_Type = 0x002C;
        /// <summary>
        /// 驾驶员未登录汇报距离间隔，单位为米（m），>0
        /// </summary>
        public const uint JT808_0x8103_0x002D_Type = 0x002D;
        /// <summary>
        /// 休眠时汇报距离间隔，单位为米（m），>0
        /// </summary>
        public const uint JT808_0x8103_0x002E_Type = 0x002E;
        /// <summary>
        /// 紧急报警时汇报距离间隔，单位为米（m），>0
        /// </summary>
        public const uint JT808_0x8103_0x002F_Type = 0x002F;
        /// <summary>
        /// 拐点补传角度，<180
        /// </summary>
        public const uint JT808_0x8103_0x0030_Type = 0x0030;
        /// <summary>
        /// 电子围栏半径（非法位移阈值），单位为米
        /// </summary>
        public const uint JT808_0x8103_0x0031_Type = 0x0031;
        /// <summary>
        /// 监控平台电话号码
        /// </summary>
        public const uint JT808_0x8103_0x0040_Type = 0x0040;
        /// <summary>
        /// 复位电话号码，可采用此电话号码拨打终端电话让终端复位
        /// </summary>
        public const uint JT808_0x8103_0x0041_Type = 0x0041;
        /// <summary>
        /// 恢复出厂设置电话号码，可采用此电话号码拨打终端电话让终端恢复出厂设置
        /// </summary>
        public const uint JT808_0x8103_0x0042_Type = 0x0042;
        /// <summary>
        /// 监控平台 SMS 电话号码
        /// </summary>
        public const uint JT808_0x8103_0x0043_Type = 0x0043;
        /// <summary>
        /// 接收终端 SMS 文本报警号码
        /// </summary>
        public const uint JT808_0x8103_0x0044_Type = 0x0044;
        /// <summary>
        /// 终端电话接听策略，0：自动接听；1：ACC ON 时自动接听，OFF 时手动接听
        /// </summary>
        public const uint JT808_0x8103_0x0045_Type = 0x0045;
        /// <summary>
        /// 每次最长通话时间，单位为秒（s），0 为不允许通话，0xFFFFFFFF 为不限制
        /// </summary>
        public const uint JT808_0x8103_0x0046_Type = 0x0046;
        /// <summary>
        /// 当月最长通话时间，单位为秒（s），0 为不允许通话，0xFFFFFFFF 为不限制
        /// </summary>
        public const uint JT808_0x8103_0x0047_Type = 0x0047;
        /// <summary>
        /// 监听电话号码
        /// </summary>
        public const uint JT808_0x8103_0x0048_Type = 0x0048;
        /// <summary>
        /// 监管平台特权短信号码
        /// </summary>
        public const uint JT808_0x8103_0x0049_Type = 0x0049;
        /// <summary>
        /// 报警屏蔽字，与位置信息汇报消息中的报警标志相对应，相应位为 1则相应报警被屏蔽
        /// </summary>
        public const uint JT808_0x8103_0x0050_Type = 0x0050;
        /// <summary>
        /// 报警发送文本 SMS 开关，与位置信息汇报消息中的报警标志相对应，相应位为 1 则相应报警时发送文本 SMS
        /// </summary>
        public const uint JT808_0x8103_0x0051_Type = 0x0051;
        /// <summary>
        /// 报警拍摄开关，与位置信息汇报消息中的报警标志相对应，相应位为1 则相应报警时摄像头拍摄
        /// </summary>
        public const uint JT808_0x8103_0x0052_Type = 0x0052;
        /// <summary>
        /// 报警拍摄存储标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警时拍的照片进行存储，否则实时上传
        /// </summary>
        public const uint JT808_0x8103_0x0053_Type = 0x0053;
        /// <summary>
        /// 关键标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警为关键报警
        /// </summary>
        public const uint JT808_0x8103_0x0054_Type = 0x0054;
        /// <summary>
        /// 最高速度，单位为公里每小时（km/h）
        /// </summary>
        public const uint JT808_0x8103_0x0055_Type = 0x0055;
        /// <summary>
        /// 超速持续时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0056_Type = 0x0056;
        /// <summary>
        /// 连续驾驶时间门限，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0057_Type = 0x0057;
        /// <summary>
        /// 当天累计驾驶时间门限，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0058_Type = 0x0058;
        /// <summary>
        /// 最小休息时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x0059_Type = 0x0059;
        /// <summary>
        /// 最长停车时间，单位为秒（s）
        /// </summary>
        public const uint JT808_0x8103_0x005A_Type = 0x005A;
        /// <summary>
        /// 超速报警预警差值，单位为 1/10Km/h
        /// </summary>
        public const uint JT808_0x8103_0x005B_Type = 0x005B;
        /// <summary>
        /// 疲劳驾驶预警差值，单位为秒（s），>0
        /// </summary>
        public const uint JT808_0x8103_0x005C_Type = 0x005C;
        /// <summary>
        /// 碰撞报警参数设置
        /// b7-b0： 碰撞时间，单位 4ms；
        /// b15-b8：碰撞加速度，单位 0.1g，设置范围在：0-79 之间，默认为10。
        /// </summary>
        public const uint JT808_0x8103_0x005D_Type = 0x005D;
        /// <summary>
        /// 侧翻报警参数设置：
        /// 侧翻角度，单位 1 度，默认为 30 度
        /// </summary>
        public const uint JT808_0x8103_0x005E_Type = 0x005E;
        /// <summary>
        /// 定时拍照控制，见 表 13
        /// </summary>
        public const uint JT808_0x8103_0x0064_Type = 0x0064;
        /// <summary>
        /// 定距拍照控制，见 表 14
        /// </summary>
        public const uint JT808_0x8103_0x0065_Type = 0x0065;
        /// <summary>
        /// 图像/视频质量，1-10，1 最好
        /// </summary>
        public const uint JT808_0x8103_0x0070_Type = 0x0070;
        /// <summary>
        /// 亮度，0-255
        /// </summary>
        public const uint JT808_0x8103_0x0071_Type = 0x0071;
        /// <summary>
        /// 对比度，0-127
        /// </summary>
        public const uint JT808_0x8103_0x0072_Type = 0x0072;
        /// <summary>
        /// 饱和度，0-127
        /// </summary>
        public const uint JT808_0x8103_0x0073_Type = 0x0073;
        /// <summary>
        /// 色度，0-255
        /// </summary>
        public const uint JT808_0x8103_0x0074_Type = 0x0074;
        /// <summary>
        /// 车辆里程表读数，1/10km
        /// </summary>
        public const uint JT808_0x8103_0x0080_Type = 0x0080;
        /// <summary>
        /// 车辆所在的省域 ID
        /// </summary>
        public const uint JT808_0x8103_0x0081_Type = 0x0081;
        /// <summary>
        /// 车辆所在的市域 ID
        /// </summary>
        public const uint JT808_0x8103_0x0082_Type = 0x0082;
        /// <summary>
        /// 公安交通管理部门颁发的机动车号牌
        /// </summary>
        public const uint JT808_0x8103_0x0083_Type = 0x0083;
        /// <summary>
        /// 车牌颜色，按照 JT/T415-2006 的 5.4.12
        /// </summary>
        public const uint JT808_0x8103_0x0084_Type = 0x0084;
        /// <summary>
        /// GNSS 定位模式，定义如下：
        /// bit0，0：禁用 GPS 定位， 1：启用 GPS 定位；
        /// bit1，0：禁用北斗定位， 1：启用北斗定位；
        /// bit2，0：禁用 GLONASS 定位， 1：启用 GLONASS 定位；
        /// bit3，0：禁用 Galileo 定位， 1：启用 Galileo 定位。
        /// </summary>
        public const uint JT808_0x8103_0x0090_Type = 0x0090;
        /// <summary>
        /// GNSS 波特率，定义如下：
        /// 0x00：4800；0x01：9600；
        /// 0x02：19200；0x03：38400；
        /// 0x04：57600；0x05：115200。
        /// </summary>
        public const uint JT808_0x8103_0x0091_Type = 0x0091;
        /// <summary>
        /// GNSS 模块详细定位数据输出频率，定义如下：
        /// 0x00：500ms；0x01：1000ms（默认值）；
        /// 0x02：2000ms；0x03：3000ms；
        /// 0x04：4000ms。
        /// </summary>
        public const uint JT808_0x8103_0x0092_Type = 0x0092;
        /// <summary>
        /// GNSS 模块详细定位数据采集频率，单位为秒，默认为 1。
        /// </summary>
        public const uint JT808_0x8103_0x0093_Type = 0x0093;
        /// <summary>
        /// GNSS 模块详细定位数据上传方式
        /// 0x00，本地存储，不上传（默认值）；
        /// 0x01，按时间间隔上传；
        /// 0x02，按距离间隔上传；
        /// 0x0B，按累计时间上传，达到传输时间后自动停止上传；
        /// 0x0C，按累计距离上传，达到距离后自动停止上传；
        /// 0x0D，按累计条数上传，达到上传条数后自动停止上传。
        /// </summary>
        public const uint JT808_0x8103_0x0094_Type = 0x0094;
        /// <summary>
        /// GNSS 模块详细定位数据上传设置：
        /// 上传方式为 0x01 时，单位为秒；
        /// 上传方式为 0x02 时，单位为米；
        /// 上传方式为 0x0B 时，单位为秒；
        /// 上传方式为 0x0C 时，单位为米；
        /// 上传方式为 0x0D 时，单位为条。
        /// </summary>
        public const uint JT808_0x8103_0x0095_Type = 0x0095;
        /// <summary>
        /// CAN 总线通道 1 采集时间间隔(ms)，0 表示不采集
        /// </summary>
        public const uint JT808_0x8103_0x0100_Type = 0x0100;
        /// <summary>
        /// CAN 总线通道 1 上传时间间隔(s)，0 表示不上传
        /// </summary>
        public const uint JT808_0x8103_0x0101_Type = 0x0101;
        /// <summary>
        /// CAN 总线通道 2 采集时间间隔(ms)，0 表示不采集
        /// </summary>
        public const uint JT808_0x8103_0x0102_Type = 0x0102;
        /// <summary>
        /// CAN 总线通道 2 上传时间间隔(s)，0 表示不上传
        /// </summary>
        public const uint JT808_0x8103_0x0103_Type = 0x0103;
        /// <summary>
        /// CAN 总线 ID 单独采集设置：
        /// bit63-bit32 表示此 ID 采集时间间隔(ms)，0 表示不采集；
        /// bit31 表示 CAN 通道号，0：CAN1，1：CAN2；
        /// bit30 表示帧类型，0：标准帧，1：扩展帧；
        /// bit29 表示数据采集方式，0：原始数据，1：采集区间的计算值；
        /// bit28-bit0 表示 CAN 总线 ID。
        /// </summary>
        public const uint JT808_0x8103_0x0110_Type = 0x0110;

        public static IDictionary<uint, Type> JT808_0x8103Method { get; private set; }
        /// <summary>
        /// 参数 ID
        /// </summary>
        public abstract uint ParamId { get; set; }

        /// <summary>
        /// 参数长度
        /// </summary>
        public abstract byte ParamLength { get; set; }

        static JT808_0x8103_BodyBase()
        {
            JT808_0x8103Method = new Dictionary<uint, Type>
            {
                { JT808_0x8103_0x0001_Type, typeof(JT808_0x8103_0x0001) },
                { JT808_0x8103_0x0002_Type, typeof(JT808_0x8103_0x0002) },
                { JT808_0x8103_0x0003_Type, typeof(JT808_0x8103_0x0003) },
                { JT808_0x8103_0x0004_Type, typeof(JT808_0x8103_0x0004) },
                { JT808_0x8103_0x0005_Type, typeof(JT808_0x8103_0x0005) },
                { JT808_0x8103_0x0006_Type, typeof(JT808_0x8103_0x0006) },
                { JT808_0x8103_0x0007_Type, typeof(JT808_0x8103_0x0007) },
                { JT808_0x8103_0x0010_Type, typeof(JT808_0x8103_0x0010) },
                { JT808_0x8103_0x0011_Type, typeof(JT808_0x8103_0x0011) },
                { JT808_0x8103_0x0012_Type, typeof(JT808_0x8103_0x0012) },
                { JT808_0x8103_0x0013_Type, typeof(JT808_0x8103_0x0013) },
                { JT808_0x8103_0x0014_Type, typeof(JT808_0x8103_0x0014) },
                { JT808_0x8103_0x0015_Type, typeof(JT808_0x8103_0x0015) },
                { JT808_0x8103_0x0016_Type, typeof(JT808_0x8103_0x0016) },
                { JT808_0x8103_0x0017_Type, typeof(JT808_0x8103_0x0017) },
                { JT808_0x8103_0x0018_Type, typeof(JT808_0x8103_0x0018) },
                { JT808_0x8103_0x0019_Type, typeof(JT808_0x8103_0x0019) },
                { JT808_0x8103_0x001A_Type, typeof(JT808_0x8103_0x001A) },
                { JT808_0x8103_0x001B_Type, typeof(JT808_0x8103_0x001B) },
                { JT808_0x8103_0x001C_Type, typeof(JT808_0x8103_0x001C) },
                { JT808_0x8103_0x001D_Type, typeof(JT808_0x8103_0x001D) },
                { JT808_0x8103_0x0020_Type, typeof(JT808_0x8103_0x0020) },
                { JT808_0x8103_0x0021_Type, typeof(JT808_0x8103_0x0021) },
                { JT808_0x8103_0x0022_Type, typeof(JT808_0x8103_0x0022) },
                { JT808_0x8103_0x0027_Type, typeof(JT808_0x8103_0x0027) },
                { JT808_0x8103_0x0028_Type, typeof(JT808_0x8103_0x0028) },
                { JT808_0x8103_0x0029_Type, typeof(JT808_0x8103_0x0029) },
                { JT808_0x8103_0x002C_Type, typeof(JT808_0x8103_0x002C) },
                { JT808_0x8103_0x002D_Type, typeof(JT808_0x8103_0x002D) },
                { JT808_0x8103_0x002E_Type, typeof(JT808_0x8103_0x002E) },
                { JT808_0x8103_0x002F_Type, typeof(JT808_0x8103_0x002F) },
                { JT808_0x8103_0x0030_Type, typeof(JT808_0x8103_0x0030) },
                { JT808_0x8103_0x0031_Type, typeof(JT808_0x8103_0x0031) },
                { JT808_0x8103_0x0040_Type, typeof(JT808_0x8103_0x0040) },
                { JT808_0x8103_0x0041_Type, typeof(JT808_0x8103_0x0041) },
                { JT808_0x8103_0x0042_Type, typeof(JT808_0x8103_0x0042) },
                { JT808_0x8103_0x0043_Type, typeof(JT808_0x8103_0x0043) },
                { JT808_0x8103_0x0044_Type, typeof(JT808_0x8103_0x0044) },
                { JT808_0x8103_0x0045_Type, typeof(JT808_0x8103_0x0045) },
                { JT808_0x8103_0x0046_Type, typeof(JT808_0x8103_0x0046) },
                { JT808_0x8103_0x0047_Type, typeof(JT808_0x8103_0x0047) },
                { JT808_0x8103_0x0048_Type, typeof(JT808_0x8103_0x0048) },
                { JT808_0x8103_0x0049_Type, typeof(JT808_0x8103_0x0049) },
                { JT808_0x8103_0x0050_Type, typeof(JT808_0x8103_0x0050) },
                { JT808_0x8103_0x0051_Type, typeof(JT808_0x8103_0x0051) },
                { JT808_0x8103_0x0052_Type, typeof(JT808_0x8103_0x0052) },
                { JT808_0x8103_0x0053_Type, typeof(JT808_0x8103_0x0053) },
                { JT808_0x8103_0x0054_Type, typeof(JT808_0x8103_0x0054) },
                { JT808_0x8103_0x0055_Type, typeof(JT808_0x8103_0x0055) },
                { JT808_0x8103_0x0056_Type, typeof(JT808_0x8103_0x0056) },
                { JT808_0x8103_0x0057_Type, typeof(JT808_0x8103_0x0057) },
                { JT808_0x8103_0x0058_Type, typeof(JT808_0x8103_0x0058) },
                { JT808_0x8103_0x0059_Type, typeof(JT808_0x8103_0x0059) },
                { JT808_0x8103_0x005A_Type, typeof(JT808_0x8103_0x005A) },
                { JT808_0x8103_0x005B_Type, typeof(JT808_0x8103_0x005B) },
                { JT808_0x8103_0x005C_Type, typeof(JT808_0x8103_0x005C) },
                { JT808_0x8103_0x005D_Type, typeof(JT808_0x8103_0x005D) },
                { JT808_0x8103_0x005E_Type, typeof(JT808_0x8103_0x005E) },
                { JT808_0x8103_0x0064_Type, typeof(JT808_0x8103_0x0064) },
                { JT808_0x8103_0x0065_Type, typeof(JT808_0x8103_0x0065) },
                { JT808_0x8103_0x0070_Type, typeof(JT808_0x8103_0x0070) },
                { JT808_0x8103_0x0071_Type, typeof(JT808_0x8103_0x0081) },
                { JT808_0x8103_0x0072_Type, typeof(JT808_0x8103_0x0072) },
                { JT808_0x8103_0x0073_Type, typeof(JT808_0x8103_0x0073) },
                { JT808_0x8103_0x0074_Type, typeof(JT808_0x8103_0x0074) },
                { JT808_0x8103_0x0080_Type, typeof(JT808_0x8103_0x0080) },
                { JT808_0x8103_0x0081_Type, typeof(JT808_0x8103_0x0081) },
                { JT808_0x8103_0x0082_Type, typeof(JT808_0x8103_0x0082) },
                { JT808_0x8103_0x0083_Type, typeof(JT808_0x8103_0x0083) },
                { JT808_0x8103_0x0084_Type, typeof(JT808_0x8103_0x0084) },
                { JT808_0x8103_0x0090_Type, typeof(JT808_0x8103_0x0090) },
                { JT808_0x8103_0x0091_Type, typeof(JT808_0x8103_0x0091) },
                { JT808_0x8103_0x0092_Type, typeof(JT808_0x8103_0x0092) },
                { JT808_0x8103_0x0093_Type, typeof(JT808_0x8103_0x0093) },
                { JT808_0x8103_0x0094_Type, typeof(JT808_0x8103_0x0094) },
                { JT808_0x8103_0x0095_Type, typeof(JT808_0x8103_0x0095) },
                { JT808_0x8103_0x0100_Type, typeof(JT808_0x8103_0x0100) },
                { JT808_0x8103_0x0101_Type, typeof(JT808_0x8103_0x0101) },
                { JT808_0x8103_0x0102_Type, typeof(JT808_0x8103_0x0102) },
                { JT808_0x8103_0x0103_Type, typeof(JT808_0x8103_0x0103) },
                { JT808_0x8103_0x0110_Type, typeof(JT808_0x8103_0x0110) }
            };
        }
    }
}
