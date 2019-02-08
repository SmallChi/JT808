using JT808.Protocol.Extensions.DependencyInjection.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JT808.Protocol.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddJT808Configure(this IServiceCollection services, IOptions<JT808Options> jT808Options)
        {
            JT808GlobalConfig.Instance.SetSkipCRCCode(jT808Options.Value.SkipCRCCode);
            JT808GlobalConfig.Instance.Register_0x0200_Attach(jT808Options.Value.JT808LocationAttachIds.ToArray());
            JT808GlobalConfig.Instance.Register_0x0200_Attach();
            foreach (var item in jT808Options.Value.JT808_0x8103Method)
            {
                JT808GlobalConfig.Instance.Register_0x8103_ParamId(item.Key, item.Value);
            }
            var servicesProvider = services.BuildServiceProvider();
            try
            {
                var msgSNDistributedImpl = servicesProvider.GetRequiredService<IMsgSNDistributed>();
                JT808GlobalConfig.Instance.SetMsgSNDistributed(msgSNDistributedImpl);
            }
            catch { }
            try
            {
                var compressImpl = servicesProvider.GetRequiredService<IJT808ICompress>();
                JT808GlobalConfig.Instance.SetCompress(compressImpl);
            }
            catch { }
            return services;
        }
    }
}
