using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 记录仪工厂
    /// </summary>
   public  class JT808_CarDVR_Down_Factory:IJT808_CarDVR_Down_Factory
    {
        public IDictionary<byte, object> Map { get; }

        public JT808_CarDVR_Down_Factory()
        {
            Map = new Dictionary<byte, object>();
            Register(Assembly.GetExecutingAssembly());
        }
        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.BaseType == typeof(JT808CarDVRDownBodies)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var commandId = (byte)type.GetProperty(nameof(JT808CarDVRDownBodies.CommandId)).GetValue(instance);
                if (Map.ContainsKey(commandId))
                {
                    throw new ArgumentException($"{type.FullName} {commandId} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(commandId, instance);
                }
            }
        }

        public IJT808_CarDVR_Down_Factory SetMap<TJT808CarDVRDownBodies>() where TJT808CarDVRDownBodies : JT808CarDVRDownBodies
        {
            Type type = typeof(TJT808CarDVRDownBodies);
            var instance = Activator.CreateInstance(type);
            var attachInfoId = (byte)type.GetProperty(nameof(JT808CarDVRDownBodies.CommandId)).GetValue(instance);
            if (Map.ContainsKey(attachInfoId))
            {
                throw new ArgumentException($"{type.FullName} {attachInfoId} An element with the same key already exists.");
            }
            else
            {
                Map.Add(attachInfoId, instance);
            }
            return this;
        }
    }
}
