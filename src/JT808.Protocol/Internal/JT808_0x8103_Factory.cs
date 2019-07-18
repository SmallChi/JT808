using JT808.Protocol.MessageBody;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    class JT808_0x8103_Factory : IJT808_0x8103_Factory
    {
        public JT808_0x8103_Factory()
        {
            ParamMethods = new ConcurrentDictionary<uint, Type>();
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0001, typeof(JT808_0x8103_0x0001));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0002, typeof(JT808_0x8103_0x0002));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0003, typeof(JT808_0x8103_0x0003));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0004, typeof(JT808_0x8103_0x0004));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0005, typeof(JT808_0x8103_0x0005));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0006, typeof(JT808_0x8103_0x0006));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0007, typeof(JT808_0x8103_0x0007));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0010, typeof(JT808_0x8103_0x0010));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0011, typeof(JT808_0x8103_0x0011));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0012, typeof(JT808_0x8103_0x0012));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0013, typeof(JT808_0x8103_0x0013));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0014, typeof(JT808_0x8103_0x0014));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0015, typeof(JT808_0x8103_0x0015));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0016, typeof(JT808_0x8103_0x0016));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0017, typeof(JT808_0x8103_0x0017));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0018, typeof(JT808_0x8103_0x0018));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0019, typeof(JT808_0x8103_0x0019));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x001A, typeof(JT808_0x8103_0x001A));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x001B, typeof(JT808_0x8103_0x001B));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x001C, typeof(JT808_0x8103_0x001C));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x001D, typeof(JT808_0x8103_0x001D));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0020, typeof(JT808_0x8103_0x0020));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0021, typeof(JT808_0x8103_0x0021));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0022, typeof(JT808_0x8103_0x0022));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0027, typeof(JT808_0x8103_0x0027));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0028, typeof(JT808_0x8103_0x0028));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0029, typeof(JT808_0x8103_0x0029));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x002C, typeof(JT808_0x8103_0x002C));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x002D, typeof(JT808_0x8103_0x002D));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x002E, typeof(JT808_0x8103_0x002E));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x002F, typeof(JT808_0x8103_0x002F));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0030, typeof(JT808_0x8103_0x0030));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0031, typeof(JT808_0x8103_0x0031));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0040, typeof(JT808_0x8103_0x0040));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0041, typeof(JT808_0x8103_0x0041));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0042, typeof(JT808_0x8103_0x0042));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0043, typeof(JT808_0x8103_0x0043));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0044, typeof(JT808_0x8103_0x0044));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0045, typeof(JT808_0x8103_0x0045));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0046, typeof(JT808_0x8103_0x0046));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0047, typeof(JT808_0x8103_0x0047));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0048, typeof(JT808_0x8103_0x0048));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0049, typeof(JT808_0x8103_0x0049));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0050, typeof(JT808_0x8103_0x0050));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0051, typeof(JT808_0x8103_0x0051));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0052, typeof(JT808_0x8103_0x0052));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0053, typeof(JT808_0x8103_0x0053));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0054, typeof(JT808_0x8103_0x0054));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0055, typeof(JT808_0x8103_0x0055));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0056, typeof(JT808_0x8103_0x0056));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0057, typeof(JT808_0x8103_0x0057));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0058, typeof(JT808_0x8103_0x0058));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0059, typeof(JT808_0x8103_0x0059));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x005A, typeof(JT808_0x8103_0x005A));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x005B, typeof(JT808_0x8103_0x005B));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x005C, typeof(JT808_0x8103_0x005C));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x005D, typeof(JT808_0x8103_0x005D));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x005E, typeof(JT808_0x8103_0x005E));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0064, typeof(JT808_0x8103_0x0064));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0065, typeof(JT808_0x8103_0x0065));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0070, typeof(JT808_0x8103_0x0070));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0071, typeof(JT808_0x8103_0x0081));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0072, typeof(JT808_0x8103_0x0072));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0073, typeof(JT808_0x8103_0x0073));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0074, typeof(JT808_0x8103_0x0074));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0080, typeof(JT808_0x8103_0x0080));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0081, typeof(JT808_0x8103_0x0081));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0082, typeof(JT808_0x8103_0x0082));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0083, typeof(JT808_0x8103_0x0083));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0084, typeof(JT808_0x8103_0x0084));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0090, typeof(JT808_0x8103_0x0090));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0091, typeof(JT808_0x8103_0x0091));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0092, typeof(JT808_0x8103_0x0092));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0093, typeof(JT808_0x8103_0x0093));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0094, typeof(JT808_0x8103_0x0094));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0095, typeof(JT808_0x8103_0x0095));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0100, typeof(JT808_0x8103_0x0100));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0101, typeof(JT808_0x8103_0x0101));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0102, typeof(JT808_0x8103_0x0102));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0103, typeof(JT808_0x8103_0x0103));
            ParamMethods.TryAdd(JT808Constants.JT808_0x8103_0x0110, typeof(JT808_0x8103_0x0110));
        }

        public ConcurrentDictionary<uint, Type> ParamMethods { get; set; }
    }
}
