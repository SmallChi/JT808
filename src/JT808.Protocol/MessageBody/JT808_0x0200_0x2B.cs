
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 模拟量
    /// </summary>
    public class JT808_0x0200_0x2B : JT808MessagePackFormatter<JT808_0x0200_0x2B>, JT808_0x0200_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        // public int Analog { get; set; }

        /// <summary>
        /// JT808_0x0200_0x2B
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x2B;
        /// <summary>
        /// 标准808固定 4 byte
        /// </summary>
        public byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 标准 808 文档 模拟量 bit0-15，AD0；bit16-31，AD1
        /// 扩展兼容模拟量
        /// 1路：2b02aaaa
        /// 2路：2b04aaaabbbb
        /// 3路：2b06aaaabbbbcccc
        /// 4路：2b08aaaabbbbccccdddd
        /// </summary>
        public List<ushort> Analogs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x2B value = new JT808_0x0200_0x2B();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            if (value.AttachInfoLength > 0)
            {
                var buffer = reader.ReadArray(value.AttachInfoLength);
                for(int i = 0; i < value.AttachInfoLength / 2; i++)
                {
                    ushort analog = (ushort)((buffer[i * 2] << 8) + buffer[i * 2 + 1]);
                    writer.WriteNumber($"[{analog.ReadNumber()}]模拟量通道{i + 1}", analog);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x2B Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x2B value = new JT808_0x0200_0x2B();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            if (value.AttachInfoLength > 0)
            {
                Analogs = new List<ushort>();
                var buffer = reader.ReadArray(value.AttachInfoLength);
                for (int i = 0; i < value.AttachInfoLength / 2; i++)
                {
                    ushort analog = (ushort)((buffer[i * 2] << 8) + buffer[i * 2 + 1]);
                    Analogs.Add(analog);
                }
            }
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x2B value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            if(Analogs==null || Analogs.Count==0)
            {
                value.AttachInfoLength = 0;
                writer.WriteByte(value.AttachInfoLength);
                return;
            }
            // ushort 占2个字节
            writer.WriteByte((byte)(Analogs.Count() * 2));
            foreach (var analog in Analogs)
            {
                writer.WriteUInt16(analog);
            }
        }
    }
}
