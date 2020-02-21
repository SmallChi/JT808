using JT808.Protocol.Interfaces;

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
