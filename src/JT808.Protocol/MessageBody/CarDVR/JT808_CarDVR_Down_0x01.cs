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
        public override byte CommandId => JT808CarDVRCommandID.采集当前驾驶人信息.ToByteValue();

        public override string Description => "采集机动车驾驶证号码";

        public override bool SkipSerialization { get; set; } = true;
    }
}
