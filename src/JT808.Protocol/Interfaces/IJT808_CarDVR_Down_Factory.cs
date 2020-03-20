using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 记录仪工厂
    /// </summary>
   public  interface IJT808_CarDVR_Down_Factory : IJT808ExternalRegister
    {
        IDictionary<byte, JT808CarDVRDownBodies> Map { get; }
        IJT808_CarDVR_Down_Factory SetMap<JT808CarDVRDownBodies>() ;
    }
}
