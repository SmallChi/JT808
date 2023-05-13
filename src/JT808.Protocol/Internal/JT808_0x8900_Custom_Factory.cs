using System.Reflection;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Internal
{
    class JT808_0x8900_Custom_Factory : IJT808_0x8900_Custom_Factory
    {
        public IDictionary<byte, object> Map { get; }

        public JT808_0x8900_Custom_Factory()
        {
            Map = new Dictionary<byte, object>();
        }

        public void Register(Assembly externalAssembly)
        {
            foreach (var item in externalAssembly.GetTypes().Where(x => x.GetConstructor(Type.EmptyTypes) != default))
            {
                Register(item);
            }
        }

        public IJT808_0x8900_Custom_Factory SetMap<T>()
        {
            var type = typeof(T);
            if (type.GetConstructor(Type.EmptyTypes) != default)
            {
                Register(type);
            }
            else
            {
                throw new ArgumentException($"{type.FullName} must be parameterless constructor.");
            }
            return this;
        }

        void Register(Type type)
        {
            var instance = Activator.CreateInstance(type);
            if (instance is JT808_0x0200_CustomBodyBase jT808_0X0200_Custom)
            {
                var key = jT808_0X0200_Custom.AttachInfoId;
                if (Map.ContainsKey(key))
                {
                    throw new ArgumentException($"{type.FullName} {key} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(key, instance);
                }
            }
            else if (instance is JT808_0x8900_BodyBase jT808_0X8900_Custom)
            {
                var key = jT808_0X8900_Custom.PassthroughType;
                if (Map.ContainsKey(key))
                {
                    throw new ArgumentException($"{type.FullName} {key} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(key, instance);
                }
            }
        }
    }
}
