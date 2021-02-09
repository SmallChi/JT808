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
    /// 设置记录仪时间
    /// 返回：北京时间的日期、时钟
    /// </summary>
    public class JT808_CarDVR_Up_0xC2 : JT808CarDVRUpBodies
    {
        /// <summary>
        /// 0xC2
        /// </summary>
        public override byte CommandId => JT808CarDVRCommandID.设置记录仪时间.ToByteValue();
        /// <summary>
        /// 北京时间的日期、时钟
        /// </summary>
        public override string Description => "北京时间的日期、时钟";
        /// <summary>
        /// 
        /// </summary>
        public override bool SkipSerialization { get; set; } = true;
    }
}
