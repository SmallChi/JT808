using JT808.Protocol.Extensions;
using System;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters
{
    public class JT808SplitPackageBodiesFormatter : IJT808MessagePackFormatter<JT808SplitPackageBodies>
    {
        public static readonly JT808SplitPackageBodiesFormatter Instance = new JT808SplitPackageBodiesFormatter();
        public JT808SplitPackageBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808SplitPackageBodies jT808SplitPackageBodies = new JT808SplitPackageBodies
            {
                Data = reader.ReadContent().ToArray()
            };
            return jT808SplitPackageBodies;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808SplitPackageBodies value, IJT808Config config)
        {
            writer.WriteArray(value.Data);
        }
    }
}
