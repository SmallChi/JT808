using System;

namespace JT808.Protocol.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class JT808BodiesTypeAttribute : Attribute
    {
        public JT808BodiesTypeAttribute(Type jT808BodiesType)
        {
            JT808BodiesType = jT808BodiesType;
        }
        public Type JT808BodiesType { get; }
    }
}
