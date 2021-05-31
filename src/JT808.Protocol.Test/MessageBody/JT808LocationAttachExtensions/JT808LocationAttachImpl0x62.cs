using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Test.JT808LocationAttach
{
    /// <summary>
    /// 自定义附加信息
    /// data-byte[]-256
    /// </summary>
    public class JT808LocationAttachImpl0x62 : JT808_0x0200_CustomBodyBase, IJT808MessagePackFormatter<JT808LocationAttachImpl0x62>
    {
        public override byte AttachInfoId { get;  set; } = 0x62;
        public override uint AttachInfoLengthExtend { get; set; } = 256;
        public override byte AttachInfoLength { get; set; }
        public byte[] Data { get; set; }

        public JT808LocationAttachImpl0x62 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808LocationAttachImpl0x62 jT808LocationAttachImpl0x62 = new JT808LocationAttachImpl0x62();
            jT808LocationAttachImpl0x62.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x62.AttachInfoLengthExtend = reader.ReadUInt32();
            jT808LocationAttachImpl0x62.Data = reader.ReadArray((int)jT808LocationAttachImpl0x62.AttachInfoLengthExtend).ToArray();
            return jT808LocationAttachImpl0x62;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808LocationAttachImpl0x62 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteUInt32((uint)value.Data.Length);
            writer.WriteArray(value.Data);
        }
    }
}
