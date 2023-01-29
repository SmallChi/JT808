using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 平台RSA公钥
    /// 0x8A00
    /// </summary>
    public class JT808_0x8A00 : JT808MessagePackFormatter<JT808_0x8A00>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x8A00
        /// </summary>
        public ushort MsgId => 0x8A00;
        /// <summary>
        /// 平台RSA公钥
        /// </summary>
        public string Description => "平台RSA公钥";
        /// <summary>
        /// e
        /// 平台 RSA 公钥{e,n}中的 e
        /// </summary>
        public uint E { get; set; }
        /// <summary>
        /// n
        /// RSA 公钥{e,n}中的 n
        /// </summary>
        public byte[] N { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8A00 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8A00 jT808_0X8A00 = new JT808_0x8A00();
            jT808_0X8A00.E = reader.ReadUInt32();
            jT808_0X8A00.N = reader.ReadArray(128).ToArray();
            return jT808_0X8A00;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8A00 value, IJT808Config config)
        {
            writer.WriteUInt32(value.E);
            if (value.N.Length != 128)
            {
                throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(value.N)}->128");
            }
            writer.WriteArray(value.N);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8A00 jT808_0X8A00 = new JT808_0x8A00();
            jT808_0X8A00.E = reader.ReadUInt32();
            jT808_0X8A00.N = reader.ReadArray(128).ToArray();
            writer.WriteNumber($"[{jT808_0X8A00.E.ReadNumber()}]RSA公钥e", jT808_0X8A00.E);
            writer.WriteString($"RSA公钥N", jT808_0X8A00.N.ToHexString());
        }
    }
}
