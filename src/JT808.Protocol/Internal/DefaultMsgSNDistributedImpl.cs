using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using JT808.Protocol.Interfaces;

namespace JT808.Protocol.Internal
{
    internal class DefaultMsgSNDistributedImpl : IJT808MsgSNDistributed
    {
        ConcurrentDictionary<string, ushort> counterDict;
        public DefaultMsgSNDistributedImpl()
        {
            counterDict = new ConcurrentDictionary<string, ushort>(StringComparer.OrdinalIgnoreCase);
        }
        public ushort Increment(string terminalPhoneNo)
        {
            if (counterDict.TryGetValue(terminalPhoneNo, out ushort value)) 
            {
                ushort newValue = ++value;
                counterDict.TryUpdate(terminalPhoneNo, newValue, value);
                return newValue;
            }
            else
            {
                counterDict.TryAdd(terminalPhoneNo, 1);
                return 1;
            }
        }
    }
}
