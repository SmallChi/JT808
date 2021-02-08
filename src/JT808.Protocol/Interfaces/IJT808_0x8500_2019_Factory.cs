using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 车辆控制
    /// </summary>
    public interface IJT808_0x8500_2019_Factory : IJT808ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<ushort, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT808_0x8500_ControlType"></typeparam>
        /// <returns></returns>
        IJT808_0x8500_2019_Factory SetMap<TJT808_0x8500_ControlType>() where TJT808_0x8500_ControlType : JT808_0x8500_ControlType;
    }
}
