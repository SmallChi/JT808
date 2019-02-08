using System;

namespace JT808.Protocol.JT808Properties
{
    /// <summary>
    /// 状态位
    /// </summary>
    public class JT808StatusProperty
    {
        /// <summary>
        /// 初始化读取状态位
        /// </summary>
        /// <param name="alarmStr"></param>
        public JT808StatusProperty(string alarmStr)
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
        /// 写入状态位
        /// 从左开始写入，不满32位自动补'0'
        /// </summary>
        /// <param name="alarmChar"></param>
        public JT808StatusProperty(params char[] alarmChar)
        {
            if (alarmChar != null)
            {
                ReadOnlySpan<char> span = alarmChar.ToString().PadRight(32, '0').AsSpan();
                for (int i = 0; i < span.Length; i++)
                {
                    this.GetType().GetProperty("Bit" + i.ToString()).SetValue(this, span[i] == '1');
                }
            }
        }

        /// <summary>
        /// 0：ACC 关；1： ACC 开
        /// </summary>
        public char Bit0 { get; set; }
        /// <summary>
        /// 0：未定位；1：定位
        /// </summary>
        public char Bit1 { get; set; }
        /// <summary>
        /// 0：北纬；1：南纬
        /// </summary>
        public char Bit2 { get; set; }
        /// <summary>
        /// 0：东经；1：西经
        /// </summary>
        public char Bit3 { get; set; }
        /// <summary>
        /// 0：运营状态；1：停运状态
        /// </summary>
        public char Bit4 { get; set; }
        /// <summary>
        /// 0：经纬度未经保密插件加密；1：经纬度已经保密插件加密
        /// </summary>
        public char Bit5 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit6 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit7 { get; set; }
        /// <summary>
        /// 00：空车；01：半载；10：保留；11：满载（可用于客车的空、重车及货车的空载、满载状态表示，人工输入或传感器获取）
        /// </summary>
        public char Bit8 { get; set; }
        /// <summary>
        /// 00：空车；01：半载；10：保留；11：满载（可用于客车的空、重车及货车的空载、满载状态表示，人工输入或传感器获取）
        /// </summary>
        public char Bit9 { get; set; }
        /// <summary>
        /// 0：车辆油路正常；1：车辆油路断开
        /// </summary>
        public char Bit10 { get; set; }
        /// <summary>
        /// 0：车辆电路正常；1：车辆电路断开
        /// </summary>
        public char Bit11 { get; set; }
        /// <summary>
        /// 0：车门解锁；1：车门加锁
        /// </summary>
        public char Bit12 { get; set; }
        /// <summary>
        /// 0：门 1 关；1：门 1 开（前门）
        /// </summary>
        public char Bit13 { get; set; }
        /// <summary>
        /// 0：门 2 关；1：门 2 开（中门）
        /// </summary>
        public char Bit14 { get; set; }
        /// <summary>
        /// 0：门 3 关；1：门 3 开（后门）
        /// </summary>
        public char Bit15 { get; set; }
        /// <summary>
        /// 0：门 4 关；1：门 4 开（驾驶席门）
        /// </summary>
        public char Bit16 { get; set; }
        /// <summary>
        /// 0：门 5 关；1：门 5 开（自定义）
        /// </summary>
        public char Bit17 { get; set; }
        /// <summary>
        /// 0：未使用 GPS 卫星进行定位；1：使用 GPS 卫星进行定位
        /// </summary>
        public char Bit18 { get; set; }
        /// <summary>
        /// 0：未使用北斗卫星进行定位；1：使用北斗卫星进行定位
        /// </summary>
        public char Bit19 { get; set; }
        /// <summary>
        /// 0：未使用 GLONASS 卫星进行定位；1：使用 GLONASS 卫星进行定位
        /// </summary>
        public char Bit20 { get; set; }
        /// <summary>
        /// 0：未使用 Galileo 卫星进行定位；1：使用 Galileo 卫星进行定位
        /// </summary>
        public char Bit21 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit22 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit23 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit24 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit25 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit26 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit27 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit28 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit29 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit30 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit31 { get; set; }

        /// <summary>
        /// 状态位
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Span<char> span = new char[32];
            for (int i = 0; i < span.Length; i++)
            {
                span[i] = (char)this.GetType().GetProperty("Bit" + i.ToString()).GetValue(this);
            }
            return span.ToString();
        }
    }
}
