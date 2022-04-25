using JT808.Protocol.Enums;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static JT808.Protocol.MessageBody.JT808_0x8105;

namespace JT808.Protocol.Extensions
{
    /// <summary>
    /// 0200扩展
    /// </summary>
    public static class JT808_0X0200_FactoryExtensions
    {
        /// <summary>
        /// 根据不同的版本号进行附加保留位判断
        /// </summary>
        /// <returns></returns>
        public static bool TryGetValue(this IJT808_0x0200_Factory factory, JT808Version version, byte attachId, out object attachInstance)
        {
            if(factory == null)
            {
                attachInstance = default;
                return false;
            }
            switch (version)
            {
                case JT808Version.JTT2013:
                case JT808Version.JTT2013Force:
                    //协议保留
                    if (attachId>=0x5 && attachId <= 0x10)
                    {
                        attachInstance = default;
                        return false;
                    }
                    else if(attachId>=0x14 && attachId <= 0x24)
                    {
                        attachInstance = default;
                        return false;
                    }
                    else
                    {
                        return factory.Map.TryGetValue(attachId, out attachInstance);
                    }
                case JT808Version.JTT2011:
                    //协议保留
                    if (attachId >= 0x4 && attachId <= 0xF)
                    {
                        attachInstance = default;
                        return false;
                    }
                    else
                    {
                        return factory.Map.TryGetValue(attachId, out attachInstance);
                    }
                case JT808Version.JTT2019:
                    //协议保留
                    if (attachId >= 0x8 && attachId <= 0xF)
                    {
                        attachInstance = default;
                        return false;
                    }
                    else if (attachId >= 0x14 && attachId <= 0x24)
                    {
                        attachInstance = default;
                        return false;
                    }
                    else
                    {
                        return factory.Map.TryGetValue(attachId, out attachInstance);
                    }
                default:
                    attachInstance = default;
                    return false;
            }
        }
    }
}
