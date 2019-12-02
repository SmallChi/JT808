using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using System;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Internal
{
    class DefaultGlobalConfig : GlobalConfigBase
    {
        public override string ConfigId { get; protected set; }
        public DefaultGlobalConfig(string configId= "Default") 
        {
            ConfigId = configId;
        }
    }
}
