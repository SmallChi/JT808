using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 记录仪工厂
    /// </summary>
   public  interface IJT808_CarDVR_Up_Factory : IJT808ExternalRegister
    {
        IDictionary<byte, JT808CarDVRUpBodies> Map { get; }
        IJT808_CarDVR_Up_Factory SetMap<JT808CarDVRUpBodies>() ;
    }
}
