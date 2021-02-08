using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 自定义设置终端参数消息工厂
    /// </summary>
    public interface IJT808_0x8103_Custom_Factory : IJT808ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<uint, object> Map { get;}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT808_0x8103_CustomBody"></typeparam>
        /// <returns></returns>

        IJT808_0x8103_Custom_Factory SetMap<TJT808_0x8103_CustomBody>() where TJT808_0x8103_CustomBody : JT808_0x8103_CustomBodyBase;
    }
}
