using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Internal
{
    class JT808_0x0200_Custom_Factory : IJT808_0x0200_Custom_Factory
    {
        public HashSet<byte> AttachIds { get; }

        public JT808_0x0200_Custom_Factory()
        {
            AttachIds = new HashSet<byte>();
        }

        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.BaseType == typeof(JT808_0x0200_CustomBodyBase)).ToList();
            foreach(var type in types)
            {
                var attachid = (byte)type.GetProperty(nameof(JT808_0x0200_CustomBodyBase.AttachInfoId)).GetValue(Activator.CreateInstance(type));
                if (!AttachIds.Contains(attachid))
                {
                    AttachIds.Add(attachid);
                }
            }
        }
    }
}
