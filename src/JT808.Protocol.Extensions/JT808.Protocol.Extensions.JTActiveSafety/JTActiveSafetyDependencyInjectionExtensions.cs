using JT808.Protocol.Extensions.JTActiveSafety.Enums;
using JT808.Protocol.Extensions.JTActiveSafety.MessageBody;
using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Extensions.JTActiveSafety
{
    /// <summary>
    /// 主动安全扩展
    /// </summary>
    public static class JTActiveSafetyDependencyInjectionExtensions
    {
        /// <summary>
        /// 添加主动安全
        /// </summary>
        /// <param name="jT808Builder"></param>
        /// <returns></returns>
        public static IJT808Builder AddJTActiveSafetyConfigure(this IJT808Builder jT808Builder)
        {
            jT808Builder.Config.Register(Assembly.GetExecutingAssembly());
            return jT808Builder;
        }
    }
}
