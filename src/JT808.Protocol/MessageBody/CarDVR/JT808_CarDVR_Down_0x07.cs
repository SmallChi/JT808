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
    /// 采集记录仪唯一性编号
    /// 返回：唯一性编号及初次安装日期
    /// </summary>
    public class JT808_CarDVR_Down_0x07 : JT808CarDVRDownBodies
    {
        public override byte CommandId => JT808CarDVRCommandID.采集记录仪唯一性编号.ToByteValue();
         
        public override string Description => "唯一性编号及初次安装日期";

        public override bool SkipSerialization { get; set; } = true;
    }
}
