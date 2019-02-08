using System;

namespace JT808.Protocol.JT808Properties
{
    /// <summary>
    /// 报警标志位定义
    /// </summary>
    public class JT808AlarmProperty
    {
        private const int bitCount = 32;
        /// <summary>
        /// 初始化读取报警标志位
        /// </summary>
        /// <param name="alarmStr"></param>
        public JT808AlarmProperty(string alarmStr)
        {
            ReadOnlySpan<char> span = alarmStr.AsSpan();
            for (int i = 0; i < span.Length; i++)
            {
                this.GetType().GetProperty("Bit" + i.ToString()).SetValue(this, span[i]);
            }
        }

        /// <summary>
        /// 写入报警标志位
        /// 从左开始写入，不满32位自动补'0'
        /// </summary>
        /// <param name="alarmChar"></param>
        public JT808AlarmProperty(params char[] alarmChar)
        {
            if (alarmChar != null)
            {
                ReadOnlySpan<char> span = alarmChar.ToString().PadRight(bitCount, '0').AsSpan();
                for (int i = 0; i < span.Length; i++)
                {
                    this.GetType().GetProperty("Bit" + i.ToString()).SetValue(this, span[i] == '1');
                }
            }
        }

        /// <summary>
        /// 1:紧急报警，触动报警开关后触发    收到应答后清零
        /// </summary>
        public char Bit0 { get; set; }
        /// <summary>
        /// 1:超速报警  标志维持至报警条件解除
        /// </summary>
        public char Bit1 { get; set; }
        /// <summary>
        /// 1:疲劳驾驶  标志维持至报警条件解除
        /// </summary>
        public char Bit2 { get; set; }
        /// <summary>
        /// 1:危险预警  收到应答后清零
        /// </summary>
        public char Bit3 { get; set; }
        /// <summary>
        /// GNSS模块发生故障  标志维持至报警条件解除
        /// </summary>
        public char Bit4 { get; set; }
        /// <summary>
        /// GNSS天线未接或被剪断   标志维持至报警条件解除
        /// </summary>
        public char Bit5 { get; set; }
        /// <summary>
        /// GNSS天线短路    标志维持至报警条件解除
        /// </summary>
        public char Bit6 { get; set; }
        /// <summary>
        /// 终端主电源欠压 标志维持至报警条件解除
        /// </summary>
        public char Bit7 { get; set; }
        /// <summary>
        /// 终端主电源掉电 标志维持至报警条件解除
        /// </summary>
        public char Bit8 { get; set; }
        /// <summary>
        /// 终端LCD或显示器故障 标志维持至报警条件解除
        /// </summary>
        public char Bit9 { get; set; }
        /// <summary>
        /// TTS模块故障 标志维持至报警条件解除
        /// </summary>
        public char Bit10 { get; set; }
        /// <summary>
        /// 摄像头故障   标志维持至报警条件解除
        /// </summary>
        public char Bit11 { get; set; }
        /// <summary>
        /// 道路运输证IC卡模块故障    标志维持至报警条件解除
        /// </summary>
        public char Bit12 { get; set; }
        /// <summary>
        /// 超速预警    标志维持至报警条件解除
        /// </summary>
        public char Bit13 { get; set; }
        /// <summary>
        /// 疲劳驾驶预警  标志维持至报警条件解除
        /// </summary>
        public char Bit14 { get; set; }
        /// <summary>
        /// 保留15
        /// </summary>
        public char Bit15 { get; set; }
        /// <summary>
        /// 保留16
        /// </summary>
        public char Bit16 { get; set; }
        /// <summary>
        /// 保留17
        /// </summary>
        public char Bit17 { get; set; }
        /// <summary>
        /// 当天累计驾驶超时    标志维持至报警条件解除
        /// </summary>
        public char Bit18 { get; set; }
        /// <summary>
        /// 超时停车    标志维持至报警条件解除
        /// </summary>
        public char Bit19 { get; set; }
        /// <summary>
        /// 进出区域    收到应答后清零
        /// </summary>
        public char Bit20 { get; set; }
        /// <summary>
        /// 进出路线    收到应答后清零
        /// </summary>
        public char Bit21 { get; set; }
        /// <summary>
        /// 路段行驶时间不足或过长 收到应答后清零
        /// </summary>
        public char Bit22 { get; set; }
        /// <summary>
        /// 路线偏离报警  标志维持至报警条件解除
        /// </summary>
        public char Bit23 { get; set; }
        /// <summary>
        /// 车辆VSS故障 标志维持至报警条件解除
        /// </summary>
        public char Bit24 { get; set; }
        /// <summary>
        /// 车辆油量异常  标志维持至报警条件解除
        /// </summary>
        public char Bit25 { get; set; }
        /// <summary>
        /// 车辆被盗通过车辆防盗器 标志维持至报警条件解除
        /// </summary>
        public char Bit26 { get; set; }
        /// <summary>
        /// 车辆非法点火  
        /// </summary>
        public char Bit27 { get; set; }
        /// <summary>
        /// 车辆非法位移  收到应答后清零
        /// </summary>
        public char Bit28 { get; set; }
        /// <summary>
        /// 碰撞预警    标志维持至报警条件解除
        /// </summary>
        public char Bit29 { get; set; }
        /// <summary>
        /// 侧翻预警    标志维持至报警条件解除
        /// </summary>
        public char Bit30 { get; set; }
        /// <summary>
        /// 非法开门报警  （终端未设置区域时，不判断非法开门）收到应答后清零
        /// </summary>
        public char Bit31 { get; set; }

        /// <summary>
        /// 报警标志位
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Span<char> span = new char[bitCount];
            for (int i = 0; i < span.Length; i++)
            {
                span[i] = (char)this.GetType().GetProperty("Bit" + i.ToString()).GetValue(this);
            }
            return span.ToString();
        }
    }
}
