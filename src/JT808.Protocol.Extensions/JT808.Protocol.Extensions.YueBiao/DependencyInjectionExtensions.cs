using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Extensions.YueBiao
{
    /// <summary>
    /// 粤标扩展
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// 添加粤标扩展
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
