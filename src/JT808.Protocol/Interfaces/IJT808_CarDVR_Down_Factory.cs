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
        IDictionary<byte, object> Map { get; }
        IJT808_CarDVR_Down_Factory SetMap<TJT808CarDVRDownBodies>() where TJT808CarDVRDownBodies : JT808CarDVRDownBodies; 
    }
}
