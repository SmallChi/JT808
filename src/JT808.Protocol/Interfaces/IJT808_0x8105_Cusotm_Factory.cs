using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;
using static JT808.Protocol.MessageBody.JT808_0x8105;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 自定义0x8105命令参数扩展工厂
    /// </summary>
    public interface IJT808_0x8105_Cusotm_Factory : IJT808ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<int, Type> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TICusotmCommandParameter"></typeparam>
        /// <returns></returns>
        IJT808_0x8105_Cusotm_Factory SetMap<TICusotmCommandParameter>() where TICusotmCommandParameter : ICusotmCommandParameter;
    }
}
