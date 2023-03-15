using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Extensions.YueBiao
{
    /// <summary>
    /// 添加粤标-主动安全
    /// JT/T808-2019
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// 添加粤标-主动安全
        /// </summary>
        /// <param name="jT808Builder"></param>
        /// <returns></returns>
        public static IJT808Builder AddYueBiaoConfigure(this IJT808Builder jT808Builder)
        {
            jT808Builder.Config.Register(Assembly.GetExecutingAssembly());
            return jT808Builder;
        }
    }
}
