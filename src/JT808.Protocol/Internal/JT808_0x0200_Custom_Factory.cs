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
        public IDictionary<byte, object> Map { get; }

        public JT808_0x0200_Custom_Factory()
        {
            Map = new Dictionary<byte, object>();
        }

        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.BaseType == typeof(JT808_0x0200_CustomBodyBase)).ToList();
            foreach(var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var attachid = (byte)type.GetProperty(nameof(JT808_0x0200_CustomBodyBase.AttachInfoId)).GetValue(instance);
                if (Map.ContainsKey(attachid))
                {
                    throw new ArgumentException($"{type.FullName} {attachid} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(attachid, instance);
                }
            }
        }

        public IJT808_0x0200_Custom_Factory SetMap<TJT808_0x0200_CustomBody>() where TJT808_0x0200_CustomBody : JT808_0x0200_CustomBodyBase
        {
            Type type = typeof(TJT808_0x0200_CustomBody);
            var instance = Activator.CreateInstance(type);
            var attachInfoId = (byte)type.GetProperty(nameof(JT808_0x0200_CustomBodyBase.AttachInfoId)).GetValue(instance);
            if (Map.ContainsKey(attachInfoId))
            {
                throw new ArgumentException($"{type.FullName} {attachInfoId} An element with the same key already exists.");
            }
            else
            {
                Map.Add(attachInfoId, instance);
            }
            return this;
        }
    }
}
