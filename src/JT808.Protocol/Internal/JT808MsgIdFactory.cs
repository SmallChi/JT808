
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace JT808.Protocol.Internal
{
    internal class JT808MsgIdFactory: IJT808MsgIdFactory
    {
        public IDictionary<ushort, object> Map { get; }

        internal JT808MsgIdFactory()
        {
            Map = new Dictionary<ushort, object>();
            InitMap(Assembly.GetExecutingAssembly());
        }

        private void InitMap(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(w => w.GetInterface(nameof(JT808Bodies)) == typeof(JT808Bodies)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                ushort msgId = 0;
                try
                {
                    msgId = (ushort)type.GetProperty(nameof(JT808Bodies.MsgId)).GetValue(instance);
                }
                catch (Exception ex)
                {
                    continue;
                }
                if (Map.ContainsKey(msgId))
                {
                    throw new ArgumentException($"{type.FullName} {msgId} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(msgId, instance);
                }
            }
        }

        public bool TryGetValue(ushort msgId, out object instance)
        {
            return Map.TryGetValue(msgId, out instance);
        }

        public IJT808MsgIdFactory SetMap<TJT808Bodies>() where TJT808Bodies : JT808Bodies
        {
            Type type = typeof(TJT808Bodies);
            var instance = Activator.CreateInstance(type);
            var msgId = (ushort)type.GetProperty(nameof(JT808Bodies.MsgId)).GetValue(instance);
            if (Map.ContainsKey(msgId))
            {
                throw new ArgumentException($"{type.FullName} {msgId} An element with the same key already exists.");
            }
            else
            {
                Map.Add(msgId, instance);
            }
            return this;
        }

        public void Register(Assembly externalAssembly)
        {
            InitMap(externalAssembly);
        }
    }
}
