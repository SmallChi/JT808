using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Formatters
{
    public interface IJT808FormatterFactory: IJT808ExternalRegister
    {
        Dictionary<Guid,object> FormatterDict { get;}
        IJT808FormatterFactory SetMap<TJT808Bodies>()
                    where TJT808Bodies : JT808Bodies;
    }
}
