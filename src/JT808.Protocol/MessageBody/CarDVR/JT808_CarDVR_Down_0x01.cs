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
    /// 机动车驾驶证号码
    /// 返回：当前驾驶人的机动车驾驶证号码
    /// </summary>
    public class JT808_CarDVR_Down_0x01 : JT808CarDVRDownBodies
    {
        /// <summary>
        /// 0x01
        /// </summary>
        public byte CommandId => JT808CarDVRCommandID.collect_driver.ToByteValue();
        /// <summary>
        /// 采集机动车驾驶证号码
        /// </summary>
        public string Description => "采集机动车驾驶证号码";
        /// <summary>
        /// 
        /// </summary>
        public bool SkipSerialization  => true;
    }
}
