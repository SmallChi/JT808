using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static JT808.Protocol.MessageBody.JT808_0x8105;

namespace JT808.Protocol.Extensions
{
    /// <summary>
    /// 终端控制 命令参数扩展
    /// </summary>
    public static class JT808_0x8105_CommandParameterExtensions
    {
        /// <summary>
        /// 创建标准命令参数
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static ICommandParameter CreateCommandParameter(in int order)
        {
            ICommandParameter commandParameter = default;
            switch (order)
            {
                case 0:
                    commandParameter = new ConnectionControlCommandParameter();
                    break;
                case 1:
                    commandParameter = new DialPointNameCommandParameter();
                    break;
                case 2:
                    commandParameter = new DialUserNameCommandParameter();
                    break;
                case 3:
                    commandParameter = new DialPwdCommandParameter();
                    break;
                case 4:
                    commandParameter = new ServerUrlCommandParameter();
                    break;
                case 5:
                    commandParameter = new TcpPortCommandParameter();
                    break;
                case 6:
                    commandParameter = new UdpPortCommandParameter();
                    break;
                case 7:
                    commandParameter = new MakerIdCommandParameter();
                    break;
                case 8:
                    commandParameter = new MonitorPlatformAuthCodeCommandParameter();
                    break;
                case 9:
                    commandParameter = new HardwareVersionCommandParameter();
                    break;
                case 10:
                    commandParameter = new FirmwareVersionCommandParameter();
                    break;
                case 11:
                    commandParameter = new UrlCommandParameter();
                    break;
                case 12:
                    commandParameter = new ConnectTimeLimitCommandParameter();
                    break;
            }
            return commandParameter;
        }

        /// <summary>
        /// 获取-连接控制
        /// </summary>
        /// <param name="CommandParameters"></param>
        /// <returns></returns>
        public static TICommandParameter GetCommandParameter<TICommandParameter>(this List<ICommandParameter> CommandParameters)
            where TICommandParameter : ICommandParameter
        {
            var value = CommandParameters.FirstOrDefault(f => f.GetType() == typeof(TICommandParameter));
            return (TICommandParameter)value;
        }

        /// <summary>
        /// 获取-连接控制
        /// </summary>
        /// <param name="commandParameter"></param>
        /// <returns></returns>
        public static string ToValueString(this ICommandParameter commandParameter)
        {
            var name = nameof(ICommandParameterValue<object>.Value);
            var property = commandParameter.GetType().GetProperty(name);
            if (property == null) return "空值";
            var value= property.GetValue(commandParameter);
            if (value == null) return "空值";
            return value.ToString();
        }
    }
}
