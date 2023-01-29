using JT808.Protocol.MessageBody;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    class JT808_0x8103_Factory : IJT808_0x8103_Factory
    {
        public JT808_0x8103_Factory()
        {
            Map = new Dictionary<uint, object>();
            Map.Add(JT808Constants.JT808_0x8103_0x0001, new JT808_0x8103_0x0001());
            Map.Add(JT808Constants.JT808_0x8103_0x0002, new JT808_0x8103_0x0002());
            Map.Add(JT808Constants.JT808_0x8103_0x0003, new JT808_0x8103_0x0003());
            Map.Add(JT808Constants.JT808_0x8103_0x0004, new JT808_0x8103_0x0004());
            Map.Add(JT808Constants.JT808_0x8103_0x0005, new JT808_0x8103_0x0005());
            Map.Add(JT808Constants.JT808_0x8103_0x0006, new JT808_0x8103_0x0006());
            Map.Add(JT808Constants.JT808_0x8103_0x0007, new JT808_0x8103_0x0007());
            Map.Add(JT808Constants.JT808_0x8103_0x0010, new JT808_0x8103_0x0010());
            Map.Add(JT808Constants.JT808_0x8103_0x0011, new JT808_0x8103_0x0011());
            Map.Add(JT808Constants.JT808_0x8103_0x0012, new JT808_0x8103_0x0012());
            Map.Add(JT808Constants.JT808_0x8103_0x0013, new JT808_0x8103_0x0013());
            Map.Add(JT808Constants.JT808_0x8103_0x0014, new JT808_0x8103_0x0014());
            Map.Add(JT808Constants.JT808_0x8103_0x0015, new JT808_0x8103_0x0015());
            Map.Add(JT808Constants.JT808_0x8103_0x0016, new JT808_0x8103_0x0016());
            Map.Add(JT808Constants.JT808_0x8103_0x0017, new JT808_0x8103_0x0017());
            Map.Add(JT808Constants.JT808_0x8103_0x0018, new JT808_0x8103_0x0018());
            Map.Add(JT808Constants.JT808_0x8103_0x0019, new JT808_0x8103_0x0019());
            Map.Add(JT808Constants.JT808_0x8103_0x001A, new JT808_0x8103_0x001A());
            Map.Add(JT808Constants.JT808_0x8103_0x001B, new JT808_0x8103_0x001B());
            Map.Add(JT808Constants.JT808_0x8103_0x001C, new JT808_0x8103_0x001C());
            Map.Add(JT808Constants.JT808_0x8103_0x001D, new JT808_0x8103_0x001D());
            Map.Add(JT808Constants.JT808_0x8103_0x0020, new JT808_0x8103_0x0020());
            Map.Add(JT808Constants.JT808_0x8103_0x0021, new JT808_0x8103_0x0021());
            Map.Add(JT808Constants.JT808_0x8103_0x0022, new JT808_0x8103_0x0022());
            Map.Add(JT808Constants.JT808_0x8103_0x0027, new JT808_0x8103_0x0027());
            Map.Add(JT808Constants.JT808_0x8103_0x0028, new JT808_0x8103_0x0028());
            Map.Add(JT808Constants.JT808_0x8103_0x0029, new JT808_0x8103_0x0029());
            Map.Add(JT808Constants.JT808_0x8103_0x002C, new JT808_0x8103_0x002C());
            Map.Add(JT808Constants.JT808_0x8103_0x002D, new JT808_0x8103_0x002D());
            Map.Add(JT808Constants.JT808_0x8103_0x002E, new JT808_0x8103_0x002E());
            Map.Add(JT808Constants.JT808_0x8103_0x002F, new JT808_0x8103_0x002F());
            Map.Add(JT808Constants.JT808_0x8103_0x0030, new JT808_0x8103_0x0030());
            Map.Add(JT808Constants.JT808_0x8103_0x0031, new JT808_0x8103_0x0031());
            Map.Add(JT808Constants.JT808_0x8103_0x0032, new JT808_0x8103_0x0032());
            Map.Add(JT808Constants.JT808_0x8103_0x0040, new JT808_0x8103_0x0040());
            Map.Add(JT808Constants.JT808_0x8103_0x0041, new JT808_0x8103_0x0041());
            Map.Add(JT808Constants.JT808_0x8103_0x0042, new JT808_0x8103_0x0042());
            Map.Add(JT808Constants.JT808_0x8103_0x0043, new JT808_0x8103_0x0043());
            Map.Add(JT808Constants.JT808_0x8103_0x0044, new JT808_0x8103_0x0044());
            Map.Add(JT808Constants.JT808_0x8103_0x0045, new JT808_0x8103_0x0045());
            Map.Add(JT808Constants.JT808_0x8103_0x0046, new JT808_0x8103_0x0046());
            Map.Add(JT808Constants.JT808_0x8103_0x0047, new JT808_0x8103_0x0047());
            Map.Add(JT808Constants.JT808_0x8103_0x0048, new JT808_0x8103_0x0048());
            Map.Add(JT808Constants.JT808_0x8103_0x0049, new JT808_0x8103_0x0049());
            Map.Add(JT808Constants.JT808_0x8103_0x0050, new JT808_0x8103_0x0050());
            Map.Add(JT808Constants.JT808_0x8103_0x0051, new JT808_0x8103_0x0051());
            Map.Add(JT808Constants.JT808_0x8103_0x0052, new JT808_0x8103_0x0052());
            Map.Add(JT808Constants.JT808_0x8103_0x0053, new JT808_0x8103_0x0053());
            Map.Add(JT808Constants.JT808_0x8103_0x0054, new JT808_0x8103_0x0054());
            Map.Add(JT808Constants.JT808_0x8103_0x0055, new JT808_0x8103_0x0055());
            Map.Add(JT808Constants.JT808_0x8103_0x0056, new JT808_0x8103_0x0056());
            Map.Add(JT808Constants.JT808_0x8103_0x0057, new JT808_0x8103_0x0057());
            Map.Add(JT808Constants.JT808_0x8103_0x0058, new JT808_0x8103_0x0058());
            Map.Add(JT808Constants.JT808_0x8103_0x0059, new JT808_0x8103_0x0059());
            Map.Add(JT808Constants.JT808_0x8103_0x005A, new JT808_0x8103_0x005A());
            Map.Add(JT808Constants.JT808_0x8103_0x005B, new JT808_0x8103_0x005B());
            Map.Add(JT808Constants.JT808_0x8103_0x005C, new JT808_0x8103_0x005C());
            Map.Add(JT808Constants.JT808_0x8103_0x005D, new JT808_0x8103_0x005D());
            Map.Add(JT808Constants.JT808_0x8103_0x005E, new JT808_0x8103_0x005E());
            Map.Add(JT808Constants.JT808_0x8103_0x0064, new JT808_0x8103_0x0064());
            Map.Add(JT808Constants.JT808_0x8103_0x0065, new JT808_0x8103_0x0065());
            Map.Add(JT808Constants.JT808_0x8103_0x0070, new JT808_0x8103_0x0070());
            Map.Add(JT808Constants.JT808_0x8103_0x0071, new JT808_0x8103_0x0071());
            Map.Add(JT808Constants.JT808_0x8103_0x0072, new JT808_0x8103_0x0072());
            Map.Add(JT808Constants.JT808_0x8103_0x0073, new JT808_0x8103_0x0073());
            Map.Add(JT808Constants.JT808_0x8103_0x0074, new JT808_0x8103_0x0074());
            Map.Add(JT808Constants.JT808_0x8103_0x0080, new JT808_0x8103_0x0080());
            Map.Add(JT808Constants.JT808_0x8103_0x0081, new JT808_0x8103_0x0081());
            Map.Add(JT808Constants.JT808_0x8103_0x0082, new JT808_0x8103_0x0082());
            Map.Add(JT808Constants.JT808_0x8103_0x0083, new JT808_0x8103_0x0083());
            Map.Add(JT808Constants.JT808_0x8103_0x0084, new JT808_0x8103_0x0084());
            Map.Add(JT808Constants.JT808_0x8103_0x0090, new JT808_0x8103_0x0090());
            Map.Add(JT808Constants.JT808_0x8103_0x0091, new JT808_0x8103_0x0091());
            Map.Add(JT808Constants.JT808_0x8103_0x0092, new JT808_0x8103_0x0092());
            Map.Add(JT808Constants.JT808_0x8103_0x0093, new JT808_0x8103_0x0093());
            Map.Add(JT808Constants.JT808_0x8103_0x0094, new JT808_0x8103_0x0094());
            Map.Add(JT808Constants.JT808_0x8103_0x0095, new JT808_0x8103_0x0095());
            Map.Add(JT808Constants.JT808_0x8103_0x0100, new JT808_0x8103_0x0100());
            Map.Add(JT808Constants.JT808_0x8103_0x0101, new JT808_0x8103_0x0101());
            Map.Add(JT808Constants.JT808_0x8103_0x0102, new JT808_0x8103_0x0102());
            Map.Add(JT808Constants.JT808_0x8103_0x0103, new JT808_0x8103_0x0103());
            Map.Add(JT808Constants.JT808_0x8103_0x0110, new JT808_0x8103_0x0110());
        }

        public IDictionary<uint, object> Map { get; }

        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.GetInterface(nameof(JT808_0x8103_BodyBase)) == typeof(JT808_0x8103_BodyBase)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                uint paramId = 0;
                try
                {
                    paramId = (uint)type.GetProperty(nameof(JT808_0x8103_BodyBase.ParamId)).GetValue(instance);
                }
                catch
                {
                    continue;
                }
                if (Map.ContainsKey(paramId))
                {
                    throw new ArgumentException($"{type.FullName} {paramId} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(paramId, instance);
                }
            }
        }

        public IJT808_0x8103_Factory SetMap<TJT808_0x8103_Body>() where TJT808_0x8103_Body : JT808_0x8103_BodyBase
        {
            Type type = typeof(TJT808_0x8103_Body);
            var instance = Activator.CreateInstance(type);
            var paramId = (uint)type.GetProperty(nameof(JT808_0x8103_BodyBase.ParamId)).GetValue(instance);
            if (Map.ContainsKey(paramId))
            {
                throw new ArgumentException($"{type.FullName} {paramId} An element with the same key already exists.");
            }
            else
            {
                Map.Add(paramId, instance);
            }
            return this;
        }
    }
}
