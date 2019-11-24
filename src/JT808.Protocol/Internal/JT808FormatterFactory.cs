using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Internal
{
   internal class JT808FormatterFactory : IJT808FormatterFactory
    {
        public IDictionary<Guid, object> FormatterDict { get; }

        public JT808FormatterFactory()
        {
            FormatterDict = new Dictionary<Guid, object>();
            Init(Assembly.GetExecutingAssembly());
        }

        private void Init(Assembly assembly)
        {
           foreach(var type in assembly.GetTypes().Where(w=>w.GetInterfaces().Contains(typeof(IJT808Formatter))))
           {
                var implTypes = type.GetInterfaces();
                if(implTypes!=null && implTypes .Length>1)
                {
                    var firstType = implTypes.FirstOrDefault(f=>f.Name== typeof(IJT808MessagePackFormatter<>).Name);
                    var genericImplType = firstType.GetGenericArguments().FirstOrDefault();
                    if (genericImplType != null)
                    {
                        if (!FormatterDict.ContainsKey(genericImplType.GUID))
                        {
                            FormatterDict.Add(genericImplType.GUID, Activator.CreateInstance(genericImplType));
                        }
                    }
                }
            }
        }

        public void Register(Assembly externalAssembly)
        {
            Init(externalAssembly);
        }

        public IJT808FormatterFactory SetMap<TIJT808Formatter>() where TIJT808Formatter : IJT808Formatter
        {
            Type type = typeof(TIJT808Formatter);
            if (!FormatterDict.ContainsKey(type.GUID))
            {
                FormatterDict.Add(type.GUID, Activator.CreateInstance(type));
            }
            return this;
        }
    }
}
