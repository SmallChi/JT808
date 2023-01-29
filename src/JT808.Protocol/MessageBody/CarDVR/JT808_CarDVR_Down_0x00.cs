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
    /// 采集记录仪执行标准版本
    /// 返回：记录仪执行标准的年号及修改单号
    /// </summary>
    public class JT808_CarDVR_Down_0x00 : JT808CarDVRDownBodies
    {
        /// <summary>
        /// 0x00
        /// </summary>
        public byte CommandId => JT808CarDVRCommandID.collect_recorder_performs_standard_version.ToByteValue();
        /// <summary>
        /// 采集记录仪执行标准版本
        /// </summary>
        public string Description => "采集记录仪执行标准版本";
        /// <summary>
        /// 
        /// </summary>
        public bool SkipSerialization  => true;
    }
}
