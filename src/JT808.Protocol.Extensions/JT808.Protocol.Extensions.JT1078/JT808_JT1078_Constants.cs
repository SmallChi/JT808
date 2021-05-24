using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078
{
    /// <summary>
    /// 
    /// </summary>
    public static class JT808_JT1078_Constants
    {
        /// <summary>
        /// 视频相关报警
        /// </summary>
        public const byte JT808_0X0200_0x14 = 0x14;
        /// <summary>
        /// 视频信号丢失报警状态
        /// </summary>
        public const byte JT808_0X0200_0x15 = 0x15;
        /// <summary>
        /// 视频信号遮挡报警状态
        /// </summary>
        public const byte JT808_0X0200_0x16 = 0x16;
        /// <summary>
        /// 存储器故障报警状态
        /// </summary>
        public const byte JT808_0X0200_0x17 = 0x17;
        /// <summary>
        /// 异常驾驶行为报警详细描述
        /// </summary>
        public const byte JT808_0X0200_0x18 = 0x18;
        /// <summary>
        /// 音视频参数设置
        /// </summary>
        public const uint JT808_0X8103_0x0075 = 0x0075;
        /// <summary>
        /// 音视频通道列表设置
        /// </summary>
        public const uint JT808_0X8103_0x0076 = 0x0076;
        /// <summary>
        /// 单独视频通道参数设置
        /// </summary>
        public const uint JT808_0X8103_0x0077 = 0x0077;
        /// <summary>
        /// 特殊报警录像参数设置
        /// </summary>
        public const uint JT808_0X8103_0x0079 = 0x0079;
        /// <summary>
        /// 视频相关报警屏蔽字
        /// </summary>
        public const uint JT808_0X8103_0x007A = 0x007A;
        /// <summary>
        /// 图像分析报警参数设置
        /// </summary>
        public const uint JT808_0X8103_0x007B = 0x007B;
        /// <summary>
        /// 终端休眠模式唤醒设置
        /// </summary>
        public const uint JT808_0X8103_0x007C = 0x007C;
    }
}
