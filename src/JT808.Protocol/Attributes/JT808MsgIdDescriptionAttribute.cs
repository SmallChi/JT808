using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class JT808MsgIdDescriptionAttribute : Attribute
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public JT808MsgIdDescriptionAttribute(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
