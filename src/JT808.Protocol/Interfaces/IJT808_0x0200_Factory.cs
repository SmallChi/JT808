using JT808.Protocol.MessageBody;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 0x0200附加信息工厂
    /// </summary>
   public interface IJT808_0x0200_Factory: IJT808ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<byte, object> Map { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT808_0x0200_Body"></typeparam>
        /// <returns></returns>
        IJT808_0x0200_Factory SetMap<TJT808_0x0200_Body>() where TJT808_0x0200_Body : JT808_0x0200_BodyBase;
    }
}
