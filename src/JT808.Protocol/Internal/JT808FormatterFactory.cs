
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
        public IDictionary<Guid, IJT808MessagePackFormatter> FormatterDict { get; }

        public JT808FormatterFactory()
        {
            FormatterDict = new Dictionary<Guid, IJT808MessagePackFormatter>();
            Init(Assembly.GetExecutingAssembly());
        }

        private void Init(Assembly assembly)
        {
           foreach(var type in assembly.GetTypes().Where(w=>w.GetInterfaces().Contains(typeof(IJT808MessagePackFormatter))))
           {
                var implTypes = type.GetInterfaces();
                if(implTypes!=null && implTypes .Length>1)
                {
                    var firstType = implTypes.FirstOrDefault(f=>f.Name== typeof(IJT808MessagePackFormatter<>).Name && !string.IsNullOrEmpty(f.FullName));
                    if (firstType != null)
                    {
                        var genericImplType = firstType.GetGenericArguments().FirstOrDefault();
                        if (genericImplType != null)
                        {
                            if (!FormatterDict.ContainsKey(genericImplType.GUID))
                            {
                                FormatterDict.Add(genericImplType.GUID, (IJT808MessagePackFormatter)Activator.CreateInstance(genericImplType));
                            }
                        }
                    }
                }
            }
        }

        public void Register(Assembly externalAssembly)
        {
            Init(externalAssembly);
        }

        public IJT808FormatterFactory SetMap<TJT808MessagePackFormatter>() where TJT808MessagePackFormatter : IJT808MessagePackFormatter
        {
            Type type = typeof(TJT808MessagePackFormatter);
            if (!FormatterDict.ContainsKey(type.GUID))
            {
                FormatterDict.Add(type.GUID, (IJT808MessagePackFormatter)Activator.CreateInstance(type));
            }
            return this;
        }
    }
}
