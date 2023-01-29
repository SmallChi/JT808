using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Internal
{
    class JT808_0x0200_Factory : IJT808_0x0200_Factory
    {
        public IDictionary<byte, object> Map { get; set; }

        public JT808_0x0200_Factory()
        {
            Map = new Dictionary<byte, object>();
            Map.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01());
            Map.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02());
            Map.Add(JT808Constants.JT808_0x0200_0x03, new JT808_0x0200_0x03());
            Map.Add(JT808Constants.JT808_0x0200_0x04, new JT808_0x0200_0x04());
            Map.Add(JT808Constants.JT808_0x0200_0x05, new JT808_0x0200_0x05());
            Map.Add(JT808Constants.JT808_0x0200_0x06, new JT808_0x0200_0x06());
            Map.Add(JT808Constants.JT808_0x0200_0x07, new JT808_0x0200_0x07());
            Map.Add(JT808Constants.JT808_0x0200_0x11, new JT808_0x0200_0x11());
            Map.Add(JT808Constants.JT808_0x0200_0x12, new JT808_0x0200_0x12());
            Map.Add(JT808Constants.JT808_0x0200_0x13, new JT808_0x0200_0x13());
            Map.Add(JT808Constants.JT808_0x0200_0x25, new JT808_0x0200_0x25());
            Map.Add(JT808Constants.JT808_0x0200_0x2A, new JT808_0x0200_0x2A());
            Map.Add(JT808Constants.JT808_0x0200_0x2B, new JT808_0x0200_0x2B());
            Map.Add(JT808Constants.JT808_0x0200_0x30, new JT808_0x0200_0x30());
            Map.Add(JT808Constants.JT808_0x0200_0x31, new JT808_0x0200_0x31());
        }

        public IJT808_0x0200_Factory SetMap<TJT808_0x0200_Body>() where TJT808_0x0200_Body : JT808_0x0200_BodyBase
        {
            Type type = typeof(TJT808_0x0200_Body);
            var instance = Activator.CreateInstance(type);
            var attachInfoId = (byte)type.GetProperty(nameof(JT808_0x0200_BodyBase.AttachInfoId)).GetValue(instance);
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

        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.GetInterface(nameof(JT808_0x0200_BodyBase)) == typeof(JT808_0x0200_BodyBase)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var attachid = (byte)type.GetProperty(nameof(JT808_0x0200_BodyBase.AttachInfoId)).GetValue(instance);
                if (Map.ContainsKey(attachid))
                {
                    throw new ArgumentException($"{type.FullName} {attachid} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(attachid, instance);
                }
            }
        }
    }
}
