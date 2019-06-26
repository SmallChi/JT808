using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    class JT808_0x8103_Custom_Factory : IJT808_0x8103_Custom_Factory
    {
        public JT808_0x8103_Custom_Factory()
        {
            ParamMethods = new Dictionary<uint, Type>();
        }

        public IDictionary<uint, Type> ParamMethods { get;}

        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.BaseType == typeof(JT808_0x8103_CustomBodyBase)).ToList();
            foreach (var type in types)
            {
                var paramId = (uint)type.GetProperty(nameof(JT808_0x8103_CustomBodyBase.ParamId)).GetValue(Activator.CreateInstance(type));
                if (!ParamMethods.ContainsKey(paramId))
                {
                    ParamMethods.Add(paramId, type);
                }
            }
        }
    }
}
