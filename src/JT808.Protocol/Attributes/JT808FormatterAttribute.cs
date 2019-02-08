using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class JT808FormatterAttribute : Attribute
    {
        public Type FormatterType { get; private set; }

        public object[] Arguments { get; private set; }

        public JT808FormatterAttribute(Type formatterType)
        {
            this.FormatterType = formatterType;
        }

        public JT808FormatterAttribute(Type formatterType, params object[] arguments)
        {
            this.FormatterType = formatterType;
            this.Arguments = arguments;
        }
    }
}
