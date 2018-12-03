using JT808.Protocol.Extensions.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using JT808.Protocol.MessageBody.JT808LocationAttach;
using System.Linq;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Extensions.DependencyInjection.Test.JT808LocationAttach;
using JT808.Protocol.Extensions.DependencyInjection.Test.JT808_0x0701BodiesImpl;

namespace JT808.Protocol.Extensions.DependencyInjection.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serverHostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddJT808Configure(new JT808Options
                    {
                        SkipCRCCode = false
                    }
                    .Register_0x0200_Attach<JT808LocationAttachImpl0x06>(0x06)
                    .Register_JT808_0x0701Body<JT808_0x0701TestBodiesImpl>());
                });    
            await serverHostBuilder.RunConsoleAsync();
        }
    }
}

