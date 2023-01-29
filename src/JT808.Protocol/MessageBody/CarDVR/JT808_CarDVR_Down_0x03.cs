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
    /// 采集累计行驶里程
    /// 返回：实时时间、安装时的初始里程及安装后的累计行驶里程
    /// </summary>
    public class JT808_CarDVR_Down_0x03 : JT808CarDVRDownBodies
    {
        /// <summary>
        /// 0x03
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.collect_accumulated_mileage.ToByteValue();
        /// <summary>
        /// 实时时间、安装时的初始里程及安装后的累计行驶里程
        /// </summary>
        public string Description => "实时时间、安装时的初始里程及安装后的累计行驶里程";
        /// <summary>
        /// 
        /// </summary>
        public bool SkipSerialization =>true;
    }
}
