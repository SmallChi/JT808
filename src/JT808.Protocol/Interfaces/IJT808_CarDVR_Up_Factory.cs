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
        /// <summary>
        /// 
        /// </summary>
        IDictionary<byte, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT808CarDVRUpBodies"></typeparam>
        /// <returns></returns>
        IJT808_CarDVR_Up_Factory SetMap<TJT808CarDVRUpBodies>() where TJT808CarDVRUpBodies : JT808CarDVRUpBodies;
    }
}
