using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    class JT808_0x8103_Custom_Factory : IJT808_0x8103_Custom_Factory
    {
        public JT808_0x8103_Custom_Factory()
        {
            Map = new Dictionary<uint, object>();
        }

        public IDictionary<uint, object> Map { get; }

        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.GetInterface(nameof(JT808_0x8103_CustomBodyBase)) == typeof(JT808_0x8103_CustomBodyBase)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var paramId = (uint)type.GetProperty(nameof(JT808_0x8103_CustomBodyBase.ParamId)).GetValue(instance);
                if (Map.ContainsKey(paramId))
                {
                    throw new ArgumentException($"{type.FullName} {paramId} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(paramId, instance);
                }
            }
        }

        public IJT808_0x8103_Custom_Factory SetMap<TJT808_0x8103_CustomBody>() where TJT808_0x8103_CustomBody : JT808_0x8103_CustomBodyBase
        {
            Type type = typeof(TJT808_0x8103_CustomBody);
            var instance = Activator.CreateInstance(type);
            var paramId = (uint)type.GetProperty(nameof(JT808_0x8103_CustomBodyBase.ParamId)).GetValue(instance);
            if (Map.ContainsKey(paramId))
            {
                throw new ArgumentException($"{type.FullName} {paramId} An element with the same key already exists.");
            }
            else
            {
                Map.Add(paramId, instance);
            }
            return this;
        }
    }
}
