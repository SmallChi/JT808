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
    /// 设置记录仪初次安装日期
    /// 返回：初次安装日期
    /// </summary>
    public class JT808_CarDVR_Up_0x83 : JT808CarDVRUpBodies
    {
        public override byte CommandId =>  JT808CarDVRCommandID.设置记录仪初次安装日期.ToByteValue();

        public override string Description => "初次安装日期";

        public override bool SkipSerialization { get; set; } = true;
    }
}
