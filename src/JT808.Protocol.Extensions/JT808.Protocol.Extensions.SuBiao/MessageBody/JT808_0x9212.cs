using JT808.Protocol.Extensions.SuBiao.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.Extensions.SuBiao.MessageBody
{
    /// <summary>
    /// 文件上传完成消息应答
    /// </summary>
    public class JT808_0x9212: JT808MessagePackFormatter<JT808_0x9212>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 文件上传完成消息应答
        /// </summary>
        public string Description => "文件上传完成消息应答";
        /// <summary>
        /// 文件名称长度
        /// </summary>
        public byte FileNameLength { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public byte FileType { get; set; }
        /// <summary>
        /// 上传结果
        /// </summary>
        public byte UploadResult { get; set; }
        /// <summary>
        /// 补传数据包数量
        /// 需要补传的数据包数量，无补传时该值为0
        /// </summary>
        public byte DataPackageCount { get; set; }
        /// <summary>
        /// 补传数据包列表
        /// </summary>
        public List<DataPackageProperty> DataPackages { get; set; }
        /// <summary>
        /// 文件上传完成消息应答Id
        /// </summary>
        public ushort MsgId => 0x9212;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9212 value = new JT808_0x9212();
            value.FileNameLength = reader.ReadByte();
            writer.WriteNumber($"[{value.FileNameLength.ReadNumber()}]文件名称长度", value.FileNameLength);
            string fileNameHex = reader.ReadVirtualArray(value.FileNameLength).ToArray().ToHexString();
            value.FileName = reader.ReadString(value.FileNameLength);
            writer.WriteString($"[{fileNameHex}]文件名称", value.FileName);
            value.FileType = reader.ReadByte();
            writer.WriteNumber($"[{value.FileType.ReadNumber()}]文件类型", value.FileType);
            value.UploadResult = reader.ReadByte();
            writer.WriteNumber($"[{value.UploadResult.ReadNumber()}]上传结果", value.UploadResult);
            value.DataPackageCount = reader.ReadByte();
            writer.WriteNumber($"[{value.DataPackageCount.ReadNumber()}]补传数据包数量", value.DataPackageCount);
            if (value.DataPackageCount > 0)
            {
                writer.WriteStartArray("补传数据包列表");
                for (int i = 0; i < value.DataPackageCount; i++)
                {
                    writer.WriteStartObject();
                    DataPackageProperty dataPackageProperty = new DataPackageProperty();
                    dataPackageProperty.Offset = reader.ReadUInt32();
                    writer.WriteNumber($"[{dataPackageProperty.Offset.ReadNumber()}]数据偏移量", dataPackageProperty.Offset);
                    dataPackageProperty.Length = reader.ReadUInt32();
                    writer.WriteNumber($"[{dataPackageProperty.Length.ReadNumber()}]数据长度", dataPackageProperty.Length);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x9212 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9212 value = new JT808_0x9212();
            value.FileNameLength = reader.ReadByte();
            value.FileName = reader.ReadString(value.FileNameLength);
            value.FileType = reader.ReadByte();
            value.UploadResult = reader.ReadByte();
            value.DataPackageCount = reader.ReadByte();
            if (value.DataPackageCount > 0)
            {
                value.DataPackages = new List<DataPackageProperty>();
                for (int i = 0; i < value.DataPackageCount; i++)
                {
                    DataPackageProperty dataPackageProperty = new DataPackageProperty();
                    dataPackageProperty.Offset = reader.ReadUInt32();
                    dataPackageProperty.Length = reader.ReadUInt32();
                    value.DataPackages.Add(dataPackageProperty);
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
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9212 value, IJT808Config config)
        {
            writer.Skip(1, out int FileNameLengthPosition);
            writer.WriteString(value.FileName);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - FileNameLengthPosition - 1), FileNameLengthPosition);
            writer.WriteByte(value.FileType);
            writer.WriteByte(value.UploadResult);
            if (value.DataPackages != null && value.DataPackages.Count > 0)
            {
                writer.WriteByte((byte)value.DataPackages.Count);
                foreach (var item in value.DataPackages)
                {
                    writer.WriteUInt32(item.Offset);
                    writer.WriteUInt32(item.Length);
                }
            }
            else
            {
                writer.WriteByte(0);
            }
        }
    }
}
