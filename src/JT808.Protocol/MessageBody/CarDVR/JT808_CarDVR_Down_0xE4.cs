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
    /// 返回正常工作状态
    /// 返回：返回正常工作状态
    /// </summary>
    public class JT808_CarDVR_Down_0xE4 : JT808CarDVRDownBodies
    {
        /// <summary>
        /// 0xE4
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.return_normal_working_status.ToByteValue();
        /// <summary>
        /// 返回正常工作状态
        /// </summary>
        public string Description => "返回正常工作状态";
        /// <summary>
        /// 
        /// </summary>
        public bool SkipSerialization => true;
    }
}
