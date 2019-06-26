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
    class JT808FormatterFactory : IJT808FormatterFactory
    {
        public Dictionary<Guid, object> FormatterDict { get; }

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
                    var firstType = implTypes.FirstOrDefault();
                    var genericImplType = firstType.GetGenericArguments().FirstOrDefault();
                    if (genericImplType == null)
                    {
                        throw new JT808Exception(JT808ErrorCode.GetFormatterAttributeError, genericImplType.FullName);
                    }
                    var attr = genericImplType.GetCustomAttribute<JT808FormatterAttribute>();
                    if (attr != null)
                    {
                        if (!FormatterDict.ContainsKey(genericImplType.GUID))
                        {
                            FormatterDict.Add(genericImplType.GUID, Activator.CreateInstance(attr.FormatterType));
                        }
                    }
                }
            }
        }

        public IJT808FormatterFactory SetMap<TJT808Bodies>() where TJT808Bodies : JT808Bodies
        {
            Type bodiesType = typeof(TJT808Bodies);
            var attr = bodiesType.GetTypeInfo().GetCustomAttribute<JT808FormatterAttribute>();
            if (attr == null)
            {
                throw new JT808Exception(JT808ErrorCode.GetFormatterAttributeError, bodiesType.FullName);
            }
            if (!FormatterDict.ContainsKey(bodiesType.GUID))
            {
                FormatterDict.Add(bodiesType.GUID, Activator.CreateInstance(attr.FormatterType));
            }
            return this;
        }

        public void Register(Assembly externalAssembly)
        {
            Init(externalAssembly);
        }
    }
}
