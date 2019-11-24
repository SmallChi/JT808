using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Formatters
{
    public interface IJT808FormatterFactory: IJT808ExternalRegister
    {
        IDictionary<Guid,object> FormatterDict { get;}
        IJT808FormatterFactory SetMap<TIJT808Formatter>()
                    where TIJT808Formatter : IJT808Formatter;
    }
}
