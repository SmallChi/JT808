using JT808.Protocol.Attributes;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Test.MessageBody.JT808Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Test.MessageBody.JT808_0x8103CustomIdExtensions
{
    /// <summary>
    /// 音视频参数设置
    /// 0x8103_0x0075
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0075_Formatter))]
    public class JT808_0x8103_0x0075 : JT808_0x8103_CustomBodyBase
    {
        public override uint ParamId { get; set; } = 0x0075;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 21;
        /// <summary>
        /// 实时流编码模式
        /// </summary>
        public byte RTS_EncodeMode { get; set; }
        /// <summary>
        /// 实时流分辨率
        /// </summary>
        public byte RTS_Resolution { get; set; }
        /// <summary>
        /// 实时流关键帧间隔
        /// （范围1-1000）帧
        /// </summary>
        public ushort RTS_KF_Interval { get; set; }
        /// <summary>
        /// 实时流目标帧率
        /// </summary>
        public byte RTS_Target_FPS { get; set; }
        /// <summary>
        /// 实时流目标码率
        /// 单位未千位每秒（kbps）
        /// </summary>
        public uint RTS_Target_CodeRate { get; set; }
        /// <summary>
        /// 存储流编码模式
        /// </summary>
        public byte StreamStore_EncodeMode { get; set; }
        /// <summary>
        /// 存储流分辨率
        /// </summary>
        public byte StreamStore_Resolution { get; set; }
        /// <summary>
        /// 存储流关键帧间隔
        /// （范围1-1000）帧
        /// </summary>
        public ushort StreamStore_KF_Interval { get; set; }
        /// <summary>
        /// 存储流目标帧率
        /// </summary>
        public byte StreamStore_Target_FPS { get; set; }
        /// <summary>
        /// 存储流目标码率
        /// 单位未千位每秒（kbps）
        /// </summary>
        public uint StreamStore_Target_CodeRate { get; set; }
        /// <summary>
        ///OSD字幕叠加设置
        /// </summary>
        public ushort OSD { get; set; }
        /// <summary>
        ///是否启用音频输出
        ///0:不启用
        ///1：启用
        /// </summary>
        public byte AudioOutputEnabled { get; set; }
    }
}