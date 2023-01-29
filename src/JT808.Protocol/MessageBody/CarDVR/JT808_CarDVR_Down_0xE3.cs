using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody.CarDVR
{
    /// <summary>
    /// 进入实时时间误差测量
    /// 返回：通过 DB9 的 7 脚输出实时时钟的秒脉冲信号（TTL 电平）
    /// </summary>
    public class JT808_CarDVR_Down_0xE3 : JT808CarDVRDownBodies
    {
        /// <summary>
        /// 0xE3
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.enter_realtime_time_error_measurement.ToByteValue();
        /// <summary>
        /// 通过 DB9 的 7 脚输出实时时钟的秒脉冲信号（TTL 电平）
        /// </summary>
        public string Description => "通过DB9的7脚输出实时时钟的秒脉冲信号（TTL 电平）";
        /// <summary>
        /// 
        /// </summary>
        public bool SkipSerialization => true;
    }
}
