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
            ParamMethods = new Dictionary<uint, Type>();
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0001, typeof(JT808_0x8103_0x0001));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0002, typeof(JT808_0x8103_0x0002));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0003, typeof(JT808_0x8103_0x0003));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0004, typeof(JT808_0x8103_0x0004));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0005, typeof(JT808_0x8103_0x0005));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0006, typeof(JT808_0x8103_0x0006));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0007, typeof(JT808_0x8103_0x0007));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0010, typeof(JT808_0x8103_0x0010));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0011, typeof(JT808_0x8103_0x0011));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0012, typeof(JT808_0x8103_0x0012));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0013, typeof(JT808_0x8103_0x0013));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0014, typeof(JT808_0x8103_0x0014));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0015, typeof(JT808_0x8103_0x0015));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0016, typeof(JT808_0x8103_0x0016));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0017, typeof(JT808_0x8103_0x0017));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0018, typeof(JT808_0x8103_0x0018));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0019, typeof(JT808_0x8103_0x0019));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x001A, typeof(JT808_0x8103_0x001A));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x001B, typeof(JT808_0x8103_0x001B));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x001C, typeof(JT808_0x8103_0x001C));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x001D, typeof(JT808_0x8103_0x001D));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0020, typeof(JT808_0x8103_0x0020));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0021, typeof(JT808_0x8103_0x0021));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0022, typeof(JT808_0x8103_0x0022));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0027, typeof(JT808_0x8103_0x0027));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0028, typeof(JT808_0x8103_0x0028));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0029, typeof(JT808_0x8103_0x0029));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x002C, typeof(JT808_0x8103_0x002C));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x002D, typeof(JT808_0x8103_0x002D));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x002E, typeof(JT808_0x8103_0x002E));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x002F, typeof(JT808_0x8103_0x002F));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0030, typeof(JT808_0x8103_0x0030));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0031, typeof(JT808_0x8103_0x0031));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0040, typeof(JT808_0x8103_0x0040));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0041, typeof(JT808_0x8103_0x0041));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0042, typeof(JT808_0x8103_0x0042));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0043, typeof(JT808_0x8103_0x0043));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0044, typeof(JT808_0x8103_0x0044));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0045, typeof(JT808_0x8103_0x0045));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0046, typeof(JT808_0x8103_0x0046));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0047, typeof(JT808_0x8103_0x0047));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0048, typeof(JT808_0x8103_0x0048));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0049, typeof(JT808_0x8103_0x0049));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0050, typeof(JT808_0x8103_0x0050));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0051, typeof(JT808_0x8103_0x0051));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0052, typeof(JT808_0x8103_0x0052));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0053, typeof(JT808_0x8103_0x0053));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0054, typeof(JT808_0x8103_0x0054));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0055, typeof(JT808_0x8103_0x0055));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0056, typeof(JT808_0x8103_0x0056));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0057, typeof(JT808_0x8103_0x0057));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0058, typeof(JT808_0x8103_0x0058));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0059, typeof(JT808_0x8103_0x0059));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x005A, typeof(JT808_0x8103_0x005A));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x005B, typeof(JT808_0x8103_0x005B));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x005C, typeof(JT808_0x8103_0x005C));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x005D, typeof(JT808_0x8103_0x005D));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x005E, typeof(JT808_0x8103_0x005E));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0064, typeof(JT808_0x8103_0x0064));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0065, typeof(JT808_0x8103_0x0065));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0070, typeof(JT808_0x8103_0x0070));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0071, typeof(JT808_0x8103_0x0081));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0072, typeof(JT808_0x8103_0x0072));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0073, typeof(JT808_0x8103_0x0073));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0074, typeof(JT808_0x8103_0x0074));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0080, typeof(JT808_0x8103_0x0080));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0081, typeof(JT808_0x8103_0x0081));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0082, typeof(JT808_0x8103_0x0082));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0083, typeof(JT808_0x8103_0x0083));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0084, typeof(JT808_0x8103_0x0084));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0090, typeof(JT808_0x8103_0x0090));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0091, typeof(JT808_0x8103_0x0091));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0092, typeof(JT808_0x8103_0x0092));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0093, typeof(JT808_0x8103_0x0093));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0094, typeof(JT808_0x8103_0x0094));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0095, typeof(JT808_0x8103_0x0095));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0100, typeof(JT808_0x8103_0x0100));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0101, typeof(JT808_0x8103_0x0101));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0102, typeof(JT808_0x8103_0x0102));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0103, typeof(JT808_0x8103_0x0103));
            ParamMethods.Add(JT808Constants.JT808_0x8103_0x0110, typeof(JT808_0x8103_0x0110));
        }

        public IDictionary<uint, Type> ParamMethods { get; set; }
    }
}
