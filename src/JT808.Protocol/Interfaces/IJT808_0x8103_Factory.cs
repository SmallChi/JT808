using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    public interface IJT808_0x8103_Factory
    {
        IDictionary<uint, Type> ParamMethods { get; set; }
    }
}
