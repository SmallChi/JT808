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
    /// </summary>
    public class JT808_CarDVR_Down_0xE0 : JT808CarDVRDownBodies
    {
        public override byte CommandId =>  JT808CarDVRCommandID.进入或保持检定状态.ToByteValue();

        public override string Description => "进入或保持检定状态";
        public override bool SkipSerialization { get; set; } = true;

    }
}
