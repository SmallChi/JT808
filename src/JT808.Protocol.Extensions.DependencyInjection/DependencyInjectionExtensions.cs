using JT808.Protocol.Extensions.DependencyInjection.Options;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body;
using JT808.Protocol.MessageBody.JT808LocationAttach;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace JT808.Protocol.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddJT808Configure(this IServiceCollection services, JT808Options jT808Options)
        {
            JT808GlobalConfig.Instance.SetSkipCRCCode(jT808Options.SkipCRCCode);
            var servicesProvider = services.BuildServiceProvider();
            try
            {
                var msgSNDistributedImpl = servicesProvider.GetRequiredService<IMsgSNDistributed>();
                JT808GlobalConfig.Instance.SetMsgSNDistributed(msgSNDistributedImpl);
            }
            catch { }
            try
            {
                var compressImpl = servicesProvider.GetRequiredService<JT808ICompress>();
                JT808GlobalConfig.Instance.SetCompress(compressImpl);
            }
            catch { }
            foreach (var impl in jT808Options.JT808LocationAttachMethod)
            {
                JT808GlobalConfig.Instance.Register_0x0200_Attach(impl.Key, impl.Value);
            }
            foreach (var impl in jT808Options.JT808_0x0900Method)
            {
                JT808GlobalConfig.Instance.Register_0x0900_Ext(impl.Key, impl.Value);
            }
            foreach (var impl in jT808Options.JT808_0x8900Method)
            {
                JT808GlobalConfig.Instance.Register_0x8900_Ext(impl.Key, impl.Value);
            }
            return services;
        }
    }
}
