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
    /// 设置初始里程
    /// 返回：记录仪初次安装时车辆已行驶的总里程
    /// </summary>
    public class JT808_CarDVR_Up_0xC4 : JT808CarDVRUpBodies
    {
        /// <summary>
        /// 0xC4
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.set_init_mileage.ToByteValue();
        /// <summary>
        /// 车辆识别代号、机动车号牌号码和机动车号牌分类
        /// </summary>
        public string Description => "车辆识别代号、机动车号牌号码和机动车号牌分类";
        /// <summary>
        /// 
        /// </summary>
        public bool SkipSerialization =>true;
    }
}
