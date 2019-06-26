using System.Threading;
using JT808.Protocol.Interfaces;

namespace JT808.Protocol.Internal
{
    internal class DefaultMsgSNDistributedImpl : IJT808MsgSNDistributed
    {
        int _counter = 0;

        public ushort Increment()
        {
            return (ushort)Interlocked.Increment(ref _counter);
        }
    }
}
