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
    /// 进入或保持检定状态
    /// 返回：进入或保持检定状态
    /// 在检定状态下，检定装置以不大于2秒的时间间隔发送包含本命令字的命令帧，记录仪在6秒内未收到该命令帧，则自动返回正常工作状态。
    /// </summary>
    public class JT808_CarDVR_Up_0xE0 : JT808CarDVRUpBodies
    {
        /// <summary>
        /// 0xE0
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.enters_maintains_check_state.ToByteValue();
        /// <summary>
        /// 进入或保持检定状态
        /// </summary>
        public string Description => "进入或保持检定状态";
        /// <summary>
        /// 
        /// </summary>
        public bool SkipSerialization => true;
    }
}
