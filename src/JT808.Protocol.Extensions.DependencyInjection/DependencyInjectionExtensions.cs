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
        public static IServiceCollection AddJT808Configure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JT808Options>(configuration);
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            JT808Options options = serviceProvider.GetRequiredService<IOptions<JT808Options>>().Value;
            JT808GlobalConfig.Instance.SetSkipCRCCode(options.SkipCRCCode);
            var servicesProvider = services.BuildServiceProvider();
            try
            {
                var msgSNDistributedImpl = servicesProvider.GetRequiredService<IMsgSNDistributed>();
                JT808GlobalConfig.Instance.SetMsgSNDistributed(msgSNDistributedImpl);
            } catch {}
            try
            {
                var compressImpl = servicesProvider.GetRequiredService<JT808ICompress>();
                JT808GlobalConfig.Instance.SetCompress(compressImpl);
            }
            catch { }
            try
            {
                var jT808LocationAttachImpls = servicesProvider.GetServices<JT808LocationAttachBase>();
                foreach (var impl in jT808LocationAttachImpls)
                {
                    JT808GlobalConfig.Instance.Register_0x0200_Attach(impl.AttachInfoId, impl.GetType());
                }
            }
            catch { }

            return services;
        }

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
            try
            {
                var jT808LocationAttachImpls = servicesProvider.GetServices<JT808LocationAttachBase>();
                foreach (var impl in jT808LocationAttachImpls)
                {
                    JT808GlobalConfig.Instance.Register_0x0200_Attach(impl.AttachInfoId, impl.GetType());
                }
            }
            catch { }
            try
            {
                var JT808_0x0900_BodyBaseExts = servicesProvider.GetServices<JT808_0x0900_BodyBase>();
                foreach (var impl in JT808_0x0900_BodyBaseExts)
                {
                    JT808GlobalConfig.Instance.Register_0x0900_Ext(impl.JT808_0x0900_ExtId, impl.GetType());
                }
            }
            catch { }
            try
            {
                var JT808_0x8900_BodyBaseExts = servicesProvider.GetServices<JT808_0x8900_BodyBase>();
                foreach (var impl in JT808_0x8900_BodyBaseExts)
                {
                    JT808GlobalConfig.Instance.Register_0x8900_Ext(impl.JT808_0x8900_ExtId, impl.GetType());
                }
            }
            catch { }
            try
            {
                var Register_JT808_0x0701BodyImpl = servicesProvider.GetServices<JT808_0x0701.JT808_0x0701Body>();
                JT808GlobalConfig.Instance.Register_JT808_0x0701Body(Register_JT808_0x0701BodyImpl.GetType());
            }
            catch { }

            return services;
        }
    }
}
