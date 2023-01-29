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
   public  class JT808_CarDVR_Up_Factory:IJT808_CarDVR_Up_Factory
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<byte, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        public JT808_CarDVR_Up_Factory()
        {
            Map = new Dictionary<byte, object>();
            Register(Assembly.GetExecutingAssembly());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="externalAssembly"></param>
        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.GetInterface(nameof(JT808CarDVRUpBodies)) == typeof(JT808CarDVRUpBodies)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var commandId = (byte)type.GetProperty(nameof(JT808CarDVRUpBodies.CommandId)).GetValue(instance);
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
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT808CarDVRUpBodies"></typeparam>
        /// <returns></returns>
        public IJT808_CarDVR_Up_Factory SetMap<TJT808CarDVRUpBodies>() where TJT808CarDVRUpBodies : JT808CarDVRUpBodies
        {
            Type type = typeof(TJT808CarDVRUpBodies);
            var instance = Activator.CreateInstance(type);
            var attachInfoId = (byte)type.GetProperty(nameof(JT808CarDVRUpBodies.CommandId)).GetValue(instance);
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
