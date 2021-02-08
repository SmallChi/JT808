using System;
using System.Collections.Generic;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// JT808消息工厂接口
    /// </summary>
    public interface IJT808MsgIdFactory:IJT808ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<ushort, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool TryGetValue(ushort msgId, out object instance);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT808Bodies"></typeparam>
        /// <returns></returns>
        IJT808MsgIdFactory SetMap<TJT808Bodies>() where TJT808Bodies : JT808Bodies;
    }
}
