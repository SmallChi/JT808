using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 自定义数据下行透传消息工厂
    /// </summary>
    public interface IJT808_0x8900_Custom_Factory : IJT808ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<byte, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT808_0x8900_BodyBase"></typeparam>
        /// <returns></returns>
        IJT808_0x8900_Custom_Factory SetMap<TJT808_0x8900_BodyBase>() where TJT808_0x8900_BodyBase : JT808_0x8900_BodyBase;
    }
}
