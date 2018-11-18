using JT808.Protocol.Extensions.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using JT808.Protocol.MessageBody.JT808LocationAttach;
using System.Linq;
using JT808.Protocol.MessageBody;

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
                    //注释掉协议包中的 JT808LocationAttachImpl0x01，JT808LocationAttachImpl0x02进行测试
                    services.AddSingleton<JT808LocationAttachBase, JT808LocationAttachImpl0x01>();
                    services.AddSingleton<JT808LocationAttachBase, JT808LocationAttachImpl0x02>();
                    // 方式1：
                    services.AddJT808Configure(hostContext.Configuration.GetSection("JT808Options"));
                    // 方式2:
                    //services.AddJT808Configure(new JT808Options
                    //{
                    //    SkipCRCCode = false
                    //});

                    byte[] bodys = "00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37".ToHexBytes();
                    var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
                });    
            await serverHostBuilder.RunConsoleAsync();
        }
    }
}

