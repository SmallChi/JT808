using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Internal
{
    class JT808_0x8900_Custom_Factory : IJT808_0x8900_Custom_Factory
    {
        public IDictionary<byte, object> Map { get; }

        public JT808_0x8900_Custom_Factory()
        {
            Map = new Dictionary<byte, object>();
        }

        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.GetInterface(nameof(JT808_0x0200_CustomBodyBase)) == typeof(JT808_0x0200_CustomBodyBase)).ToList();
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

        public IJT808_0x8900_Custom_Factory SetMap<TJT808_0x8900_Custom_Factory>() where TJT808_0x8900_Custom_Factory : JT808_0x8900_BodyBase
        {
            Type type = typeof(TJT808_0x8900_Custom_Factory);
            var instance = Activator.CreateInstance(type);
            var passthroughType = (byte)type.GetProperty(nameof(JT808_0x8900_BodyBase.PassthroughType)).GetValue(instance);
            if (Map.ContainsKey(passthroughType))
            {
                throw new ArgumentException($"{type.FullName} {passthroughType} An element with the same key already exists.");
            }
            else
            {
                Map.Add(passthroughType, instance);
            }
            return this;
        }
    }
}
