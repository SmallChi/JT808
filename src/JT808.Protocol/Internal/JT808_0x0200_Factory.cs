using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Internal
{
    class JT808_0x0200_Factory : IJT808_0x0200_Factory
    {
        public IDictionary<byte, Type> JT808LocationAttachMethod { get ; set; }

        public JT808_0x0200_Factory()
        {
            JT808LocationAttachMethod = new Dictionary<byte, Type>();
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x01, typeof(JT808_0x0200_0x01));
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x02, typeof(JT808_0x0200_0x02));
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x03, typeof(JT808_0x0200_0x03));
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x04, typeof(JT808_0x0200_0x04));
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x11, typeof(JT808_0x0200_0x11));
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x12, typeof(JT808_0x0200_0x12));
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x13, typeof(JT808_0x0200_0x13));
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x25, typeof(JT808_0x0200_0x25));
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x2A, typeof(JT808_0x0200_0x2A));
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x2B, typeof(JT808_0x0200_0x2B));
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x30, typeof(JT808_0x0200_0x30));
            JT808LocationAttachMethod.Add(JT808Constants.JT808_0x0200_0x31, typeof(JT808_0x0200_0x31));
        }
    }
}
