using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 自定义0x0200附加信息工厂
    /// </summary>
    public interface IJT808_0x0200_Custom_Factory: IJT808ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<byte, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT808_0x0200_CustomBody"></typeparam>
        /// <returns></returns>
        IJT808_0x0200_Custom_Factory SetMap<TJT808_0x0200_CustomBody>() where TJT808_0x0200_CustomBody : JT808_0x0200_CustomBodyBase;
    }
}
