using JT808.Protocol.Interfaces;
using System.Runtime.CompilerServices;

namespace JT808.Protocol.Internal
{
    /// <summary>
    /// 默认全局配置
    /// </summary> 
    class DefaultGlobalConfig : GlobalConfigBase
    {
        /// <summary>
        /// 配置Id
        /// </summary>
        public override string ConfigId { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configId"></param>
        public DefaultGlobalConfig(string configId= "Default") 
        {
            ConfigId = configId;
        }
    }
}
