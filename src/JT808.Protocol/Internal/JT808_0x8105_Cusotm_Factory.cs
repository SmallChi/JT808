using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using static JT808.Protocol.MessageBody.JT808_0x8105;

namespace JT808.Protocol.Internal
{
    class JT808_0x8105_Cusotm_Factory : IJT808_0x8105_Cusotm_Factory
    {
        public IDictionary<int, Type> Map { get; }

        public JT808_0x8105_Cusotm_Factory()
        {
            Map = new Dictionary<int, Type>();
        }

        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.GetInterface(nameof(ICusotmCommandParameter)) == typeof(ICusotmCommandParameter)).ToList();
            foreach(var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var order = (int)type.GetProperty(nameof(ICusotmCommandParameter.Order)).GetValue(instance);
                if (order < CommandParameterCount)
                {
                    throw new ArgumentException($"{type.FullName} {order} We're starting at 13 and we're incremying by 1.");
                }
                if (Map.ContainsKey(order))
                {
                    throw new ArgumentException($"{type.FullName} {order} An element with the same Order already exists.");
                }
                else
                {
                    Map.Add(order, type);
                }
            }
        }

        public IJT808_0x8105_Cusotm_Factory SetMap<TICusotmCommandParameter>() where TICusotmCommandParameter : ICusotmCommandParameter
        {
            Type type = typeof(TICusotmCommandParameter);
            var instance = Activator.CreateInstance(type);
            var order = (int)type.GetProperty(nameof(ICusotmCommandParameter.Order)).GetValue(instance);
            if(order < CommandParameterCount)
            {
                throw new ArgumentException($"{type.FullName} Order is {order}. We're starting at 13 and we're incremying by 1.");
            }
            if (Map.ContainsKey(order))
            {
                throw new ArgumentException($"{type.FullName} Order is {order}. An element with the same Order already exists.");
            }
            else
            {
                Map.Add(order, type);
            }
            return this;
        }
    }
}
