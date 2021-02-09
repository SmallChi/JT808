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
        /// <summary>
        /// 
        /// </summary>
        IDictionary<byte, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT808CarDVRDownBodies"></typeparam>
        /// <returns></returns>
        IJT808_CarDVR_Down_Factory SetMap<TJT808CarDVRDownBodies>() where TJT808CarDVRDownBodies : JT808CarDVRDownBodies; 
    }
}
