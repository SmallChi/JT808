using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Internal
{
    class JT808_0x8500_2019_Factory : IJT808_0x8500_2019_Factory
    {
        public IDictionary<ushort, object> Map { get; }

        public JT808_0x8500_2019_Factory()
        {
            Map = new Dictionary<ushort, object>();
        }

        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.BaseType == typeof(JT808_0x8500_ControlType)).ToList();
            foreach(var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var controlTypeId = (ushort)type.GetProperty(nameof(JT808_0x8500_ControlType.ControlTypeId)).GetValue(instance);
                if (Map.ContainsKey(controlTypeId))
                {
                    throw new ArgumentException($"{type.FullName} {controlTypeId} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(controlTypeId, instance);
                }
            }
        }

        public IJT808_0x8500_2019_Factory SetMap<TJT808_0x8500_ControlType>() where TJT808_0x8500_ControlType : JT808_0x8500_ControlType
        {
            Type type = typeof(TJT808_0x8500_ControlType);
            var instance = Activator.CreateInstance(type);
            var controlTypeId = (ushort)type.GetProperty(nameof(JT808_0x8500_ControlType.ControlTypeId)).GetValue(instance);
            if (Map.ContainsKey(controlTypeId))
            {
                throw new ArgumentException($"{type.FullName} {controlTypeId} An element with the same key already exists.");
            }
            else
            {
                Map.Add(controlTypeId, instance);
            }
            return this;
        }
    }
}
