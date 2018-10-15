using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JT808.Protocol.JT808Internal
{
    internal class DefaultMsgSNDistributedImpl : IMsgSNDistributed
    {
        int _counter = 0;

        public ushort Increment()
        {
            return (ushort)Interlocked.Increment(ref _counter);
        }
    }
}
