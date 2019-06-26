using System;

namespace JT808.Protocol.Metadata
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
            Bit0 = alarmStr[0];
            Bit1 = alarmStr[1];
            Bit2 = alarmStr[2];
            Bit3 = alarmStr[3];
            Bit4 = alarmStr[4];
            Bit5 = alarmStr[5];
            Bit6 = alarmStr[6];
            Bit7 = alarmStr[7];
            Bit8 = alarmStr[8];
            Bit9 = alarmStr[9];
            Bit10 = alarmStr[10];
            Bit11 = alarmStr[11];
            Bit12 = alarmStr[12];
            Bit13 = alarmStr[13];
            Bit14 = alarmStr[14];
            Bit15 = alarmStr[15];
            Bit16 = alarmStr[16];
            Bit17 = alarmStr[17];
            Bit18 = alarmStr[18];
            Bit19 = alarmStr[19];
            Bit20 = alarmStr[20];
            Bit21 = alarmStr[21];
            Bit22 = alarmStr[22];
            Bit23 = alarmStr[23];
            Bit24 = alarmStr[24];
            Bit25 = alarmStr[25];
            Bit26 = alarmStr[26];
            Bit27 = alarmStr[27];
            Bit28 = alarmStr[28];
            Bit29 = alarmStr[29];
            Bit30 = alarmStr[30];
            Bit31 = alarmStr[31];
        }

        /// <summary>
        /// 写入报警标志位
        /// 从左开始写入，不满32位自动补'0'
        /// </summary>
        /// <param name="alarmChar"></param>
        public JT808AlarmProperty(params char[] alarmChar)
        {
            alarmChar = alarmChar ?? new char[32];
            ReadOnlySpan<char> span = alarmChar.ToString().PadRight(32, '0').AsSpan();
            Bit0 = span[0];
            Bit1 = span[1];
            Bit2 = span[2];
            Bit3 = span[3];
            Bit4 = span[4];
            Bit5 = span[5];
            Bit6 = span[6];
            Bit7 = span[7];
            Bit8 = span[8];
            Bit9 = span[9];
            Bit10 = span[10];
            Bit11 = span[11];
            Bit12 = span[12];
            Bit13 = span[13];
            Bit14 = span[14];
            Bit15 = span[15];
            Bit16 = span[16];
            Bit17 = span[17];
            Bit18 = span[18];
            Bit19 = span[19];
            Bit20 = span[20];
            Bit21 = span[21];
            Bit22 = span[22];
            Bit23 = span[23];
            Bit24 = span[24];
            Bit25 = span[25];
            Bit26 = span[26];
            Bit27 = span[27];
            Bit28 = span[28];
            Bit29 = span[29];
            Bit30 = span[30];
            Bit31 = span[31];
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
