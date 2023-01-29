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
    /// 设置状态量配置信息
    /// 返回：状态量配置信息
    /// </summary>
    public class JT808_CarDVR_Up_0x84 : JT808CarDVRUpBodies
    {
        /// <summary>
        /// 0x84
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.set_state_quantity_configuration_information.ToByteValue();
        /// <summary>
        /// 状态量配置信息
        /// </summary>
        public string Description => "状态量配置信息";
        /// <summary>
        /// 
        /// </summary>
        public bool SkipSerialization => true;
    }
}
