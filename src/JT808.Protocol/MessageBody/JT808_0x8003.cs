using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 补传分包请求
    /// 0x8003
    /// </summary>
    public class JT808_0x8003 : JT808MessagePackFormatter<JT808_0x8003>, JT808Bodies,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8003
        /// </summary>
        public ushort MsgId  => 0x8003;
        /// <summary>
        /// 补传分包请求
        /// </summary>
        public string Description => "补传分包请求";
        /// <summary>
        /// 原始消息流水号
        /// 对应要求补传的原始消息第一包的消息流水号
        /// </summary>
        public ushort OriginalMsgNum { get; set; }
        /// <summary>
        /// 重传包总数
        /// 2013 byte
        /// 2019 ushort
        /// </summary>
        public ushort AgainPackageCount { get; set; }
        /// <summary>
        /// 重传包 ID 列表
        /// BYTE[2*n]
        /// 重传包序号顺序排列，如“包 ID1 包 ID2......包 IDn”。
        /// </summary>
        public byte[] AgainPackageData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8003 value = new JT808_0x8003();
            value.OriginalMsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.OriginalMsgNum.ReadNumber()}]原始消息流水号", value.OriginalMsgNum);
            if (reader.Version == JT808Version.JTT2019)
            {
                value.AgainPackageCount = reader.ReadUInt16();
            }
            else {
                value.AgainPackageCount = reader.ReadByte();
            }

            writer.WriteNumber($"[{value.AgainPackageCount.ReadNumber()}]重传包总数", value.AgainPackageCount);
            writer.WriteStartArray("重传包ID列表");
            for(int i=0;i< value.AgainPackageCount; i++)
            {
                writer.WriteStartObject();
                var idBuffer=reader.ReadArray(2).ToArray();
                writer.WriteString($"Id{i+1}", idBuffer.ToHexString());
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8003 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8003 value = new JT808_0x8003();
            value.OriginalMsgNum = reader.ReadUInt16();
            if (reader.Version == JT808Version.JTT2019)
            {
                value.AgainPackageCount = reader.ReadUInt16();
            }
            else
            {
                value.AgainPackageCount = reader.ReadByte();
            }
            value.AgainPackageData = reader.ReadArray(value.AgainPackageCount * 2).ToArray();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8003 value, IJT808Config config)
        {
            writer.WriteUInt16(value.OriginalMsgNum);
            if (writer.Version == JT808Version.JTT2019)
            {
                writer.WriteUInt16((byte)(value.AgainPackageData.Length / 2));
            }
            else {
                writer.WriteByte((byte)(value.AgainPackageData.Length / 2));
            }

            writer.WriteArray(value.AgainPackageData);
        }
    }
}
