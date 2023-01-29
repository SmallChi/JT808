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
    /// 进入里程误差测量
    /// 返回：通过 DB9 的 7 脚接收标准速度脉冲测量信号（TTL 电平）
    /// </summary>
    public class JT808_CarDVR_Up_0xE1 : JT808CarDVRUpBodies
    {
        /// <summary>
        /// 0xE1
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.enter_mileage_error_measurement.ToByteValue();
        /// <summary>
        /// 通过DB9的7脚接收标准速度脉冲测量信号（TTL 电平）
        /// </summary>
        public string Description => "通过 DB9的7脚接收标准速度脉冲测量信号（TTL 电平）";
        /// <summary>
        /// 
        /// </summary>
        public bool SkipSerialization =>true;
    }
}
