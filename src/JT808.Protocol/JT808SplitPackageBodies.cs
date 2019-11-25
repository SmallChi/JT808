using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol
{
    /// <summary>
    /// 统一分包数据体
    /// </summary>
    public class JT808SplitPackageBodies : JT808Bodies,  IJT808MessagePackFormatter<JT808SplitPackageBodies>
    {

        public byte[] Data { get; set; }

        public override ushort MsgId => 0xFFFF;

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
