using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody.CarDVR
{
    /// <summary>
    /// 采集记录仪状态信号配置信息
    /// 返回：状态信号配置信息
    /// </summary>
    public class JT808_CarDVR_Down_0x06 : JT808CarDVRDownBodies
    {
        /// <summary>
        /// 0x06
        /// </summary>
        public byte CommandId => JT808CarDVRCommandID.collect_recorder_status_signal_configuration_information.ToByteValue();
        /// <summary>
        /// 状态信号配置信息
        /// </summary>
        public string Description => "状态信号配置信息";
        /// <summary>
        /// 
        /// </summary>
        public bool SkipSerialization =>true;
    }
}
