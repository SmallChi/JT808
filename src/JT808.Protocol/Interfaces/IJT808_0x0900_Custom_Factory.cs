using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 自定义数据上行透传
    /// </summary>
    public interface IJT808_0x0900_Custom_Factory : IJT808ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<byte, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT808_0x0900_BodyBase"></typeparam>
        /// <returns></returns>
        IJT808_0x0900_Custom_Factory SetMap<TJT808_0x0900_BodyBase>() where TJT808_0x0900_BodyBase : JT808_0x0900_BodyBase;
    }
}
