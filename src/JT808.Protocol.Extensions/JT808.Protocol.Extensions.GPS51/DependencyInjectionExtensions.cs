using JT808.Protocol.Extensions.GPS51.Enums;
using JT808.Protocol.Extensions.GPS51.MessageBody;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static System.Net.WebRequestMethods;

namespace JT808.Protocol.Extensions.GPS51
{
    /// <summary>
    /// GPS51 extension JT/T808
    /// <see cref="https://gps51.com/#/jt808add"/>
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// 注册GPS51扩展JT/T808
        /// Register GPS51 extension JT/T808
        /// </summary>
        /// <param name="jT808Builder"></param>
        /// <returns></returns>
        public static IJT808Builder AddGPS51Configure(this IJT808Builder jT808Builder) 
        {
            jT808Builder.Config.Register(Assembly.GetExecutingAssembly());
            return jT808Builder;
        }
    }
}
