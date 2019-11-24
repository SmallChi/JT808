using System;
using System.Collections.Generic;

namespace JT808.Protocol.Interfaces
{
    public interface IJT808MsgIdFactory:IJT808ExternalRegister
    {
        IDictionary<ushort, object> Map { get; }
        bool TryGetValue(ushort msgId, out object instance);
        IJT808MsgIdFactory SetMap<TJT808Bodies>() where TJT808Bodies : JT808Bodies;
    }
}
