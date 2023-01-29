using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Extensions;
using System.Text.Json;
using JT808.Protocol.Enums;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端补传分包请求
    /// 2019版本
    /// </summary>
    public class JT808_0x0005 : JT808MessagePackFormatter<JT808_0x0005>, JT808Bodies, IJT808_2019_Version, IJT808Analyze
    {
        /// <summary>
        /// 0x0005
        /// </summary>
        public ushort MsgId  => 0x0005;
        /// <summary>
        /// 终端补传分包请求
        /// </summary>
        public string Description => "终端补传分包请求";
        /// <summary>
        /// 原始消息流水号
        /// 对应要求补传的原始消息第一包的消息流水号
        /// </summary>
        public ushort OriginalMsgNum { get; set; }
        /// <summary>
        /// 重传包总数
        /// n
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
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0005 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0005 value = new JT808_0x0005();
            value.OriginalMsgNum = reader.ReadUInt16();
            if(reader.Version== JT808Version.JTT2013)
            {
                value.AgainPackageCount = reader.ReadByte();
            }
            else
            {
                value.AgainPackageCount = reader.ReadUInt16();
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
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0005 value, IJT808Config config)
        {
            writer.WriteUInt16(value.OriginalMsgNum);
            if(writer.Version== JT808Version.JTT2013)
            {
                writer.WriteByte((byte)(value.AgainPackageData.Length / 2));
            }
            else
            {
                writer.WriteUInt16((ushort)(value.AgainPackageData.Length / 2));
            }
            writer.WriteArray(value.AgainPackageData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            var originalMsgNum = reader.ReadUInt16();
            ushort againPackageCount;
            if (reader.Version == JT808Version.JTT2013)
            {
                againPackageCount = reader.ReadByte();
            }
            else
            {
                againPackageCount = reader.ReadUInt16();
            }
            var againPackageData = reader.ReadArray(againPackageCount * 2);
            writer.WriteNumber($"[{originalMsgNum.ReadNumber()}]原始消息流水号", originalMsgNum);
            writer.WriteNumber($"[{againPackageCount.ReadNumber()}]重传包总数", againPackageCount);
            writer.WriteString("重传包ID", string.Join(",", againPackageData.ToArray()));
            writer.WriteStartArray("重传包ID列表");
            for (var i=0;i< againPackageCount; i++)
            {
                writer.WriteStringValue(string.Join(",",againPackageData.Slice(i*2,2).ToArray()));
            }
            writer.WriteEndArray();
        }
    }
}
