using JT808.Protocol.Exceptions;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端 RSA 公钥
    /// 0x0A00
    /// </summary>
    public class JT808_0x0A00 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0A00>
    {
        public override ushort MsgId { get; } = 0x0A00;
        /// <summary>
        /// e
        /// 终端 RSA 公钥{e,n}中的 e
        /// </summary>
        public uint E { get; set; }
        /// <summary>
        /// n
        /// RSA 公钥{e,n}中的 n
        /// </summary>
        public byte[] N { get; set; }
        public JT808_0x0A00 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0A00 jT808_0X0A00 = new JT808_0x0A00
            {
                E = reader.ReadUInt32(),
                N = reader.ReadArray(128).ToArray()
            };
            if (jT808_0X0A00.N.Length != 128)
            {
                throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(jT808_0X0A00.N)}->128");
            }
            return jT808_0X0A00;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0A00 value, IJT808Config config)
        {
            writer.WriteUInt32(value.E);
            if (value.N.Length != 128)
            {
                throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(value.N)}->128");
            }
            writer.WriteArray(value.N);
        }
    }
}
