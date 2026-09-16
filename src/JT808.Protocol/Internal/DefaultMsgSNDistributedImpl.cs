using System.Collections.Concurrent;
using JT808.Protocol.Interfaces;

namespace JT808.Protocol.Internal
{
    internal class DefaultMsgSNDistributedImpl : IJT808MsgSNDistributed
    {
        private readonly ConcurrentDictionary<string, int> counterDict = new(StringComparer.OrdinalIgnoreCase);

        public ushort Increment(string terminalPhoneNo) => (ushort)counterDict.AddOrUpdate(terminalPhoneNo, static _ => 0, static (_, value) => (value + 1) & 0xFFFF);
    }
}
