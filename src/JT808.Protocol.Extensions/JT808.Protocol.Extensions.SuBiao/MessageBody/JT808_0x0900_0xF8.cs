using JT808.Protocol.Extensions.SuBiao.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.SuBiao.MessageBody
{
    /// <summary>
    /// 透传数据
    /// </summary>
    public class JT808_0x0900_0xF8 : JT808MessagePackFormatter<JT808_0x0900_0xF8>, JT808_0x0900_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 透传类型
        /// </summary>
        public byte PassthroughType { get; set; } = JT808_SuBiao_Constants.JT808_0X0900_0xF8;
        /// <summary>
        /// 消息列表总数
        /// </summary>
        public byte USBMessageCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<JT808_0x0900_0xF8_USB> USBMessages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0900_0xF8 value = new JT808_0x0900_0xF8();
            value.USBMessageCount = reader.ReadByte();
            writer.WriteNumber($"[{value.USBMessageCount.ReadNumber()}]消息列表总数", value.USBMessageCount);
            if (value.USBMessageCount > 0)
            {
                writer.WriteStartArray("消息列表");
                for (int i = 0; i < value.USBMessageCount; i++)
                {
                    writer.WriteStartObject();
                    JT808_0x0900_0xF8_USB item = new JT808_0x0900_0xF8_USB();
                    item.USBID = reader.ReadByte();
                    writer.WriteNumber($"[{item.USBID.ReadNumber()}]外设ID", item.USBID);
                    item.MessageLength = reader.ReadByte();
                    writer.WriteNumber($"[{item.MessageLength.ReadNumber()}]消息长度", item.MessageLength);
                    item.CompantNameLength = reader.ReadByte();
                    writer.WriteNumber($"[{item.CompantNameLength.ReadNumber()}]公司名称长度", item.CompantNameLength);

                    string compantNameHex = reader.ReadVirtualArray(item.CompantNameLength).ToArray().ToHexString();
                    item.CompantName = reader.ReadString(item.CompantNameLength);
                    writer.WriteString($"[{compantNameHex}]公司名称", item.CompantName);

                    item.ProductModelLength = reader.ReadByte();
                    writer.WriteNumber($"[{item.ProductModelLength.ReadNumber()}]产品型号长度", item.ProductModelLength);

                    string productModelHex = reader.ReadVirtualArray(item.ProductModelLength).ToArray().ToHexString();
                    item.ProductModel = reader.ReadString(item.ProductModelLength);
                    writer.WriteString($"[{productModelHex}]产品型号", item.ProductModel);

                    item.HardwareVersionNumberLength = reader.ReadByte();
                    writer.WriteNumber($"[{item.HardwareVersionNumberLength.ReadNumber()}]硬件版本号长度", item.HardwareVersionNumberLength);
                    string hardwareVersionNumberHex = reader.ReadVirtualArray(item.HardwareVersionNumberLength).ToArray().ToHexString();
                    item.HardwareVersionNumber = reader.ReadString(item.HardwareVersionNumberLength);
                    writer.WriteString($"[{hardwareVersionNumberHex}]硬件版本号", item.HardwareVersionNumber);

                    item.SoftwareVersionNumberLength = reader.ReadByte();
                    writer.WriteNumber($"[{item.SoftwareVersionNumberLength.ReadNumber()}]软件版本号长度", item.SoftwareVersionNumberLength);
                    string softwareVersionNumberHex = reader.ReadVirtualArray(item.SoftwareVersionNumberLength).ToArray().ToHexString();
                    item.SoftwareVersionNumber = reader.ReadString(item.SoftwareVersionNumberLength);
                    writer.WriteString($"[{softwareVersionNumberHex}]软件版本号", item.SoftwareVersionNumber);

                    item.DevicesIDLength = reader.ReadByte();
                    writer.WriteNumber($"[{item.DevicesIDLength.ReadNumber()}]设备ID长度", item.DevicesIDLength);
                    string devicesIDHex = reader.ReadVirtualArray(item.DevicesIDLength).ToArray().ToHexString();
                    item.DevicesID = reader.ReadString(item.DevicesIDLength);
                    writer.WriteString($"[{devicesIDHex}]设备ID", item.DevicesID);

                    item.CustomerCodeLength = reader.ReadByte();
                    writer.WriteNumber($"[{item.CustomerCodeLength.ReadNumber()}]客户代码长度", item.CustomerCodeLength);
                    string customerCodeHex = reader.ReadVirtualArray(item.CustomerCodeLength).ToArray().ToHexString();
                    item.CustomerCode = reader.ReadString(item.CustomerCodeLength);
                    writer.WriteString($"[{customerCodeHex}]客户代码", item.CustomerCode);

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
        public override JT808_0x0900_0xF8 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0900_0xF8 value = new JT808_0x0900_0xF8();
            value.USBMessageCount = reader.ReadByte();
            if (value.USBMessageCount > 0)
            {
                value.USBMessages = new List<JT808_0x0900_0xF8_USB>();
                for (int i = 0; i < value.USBMessageCount; i++)
                {
                    JT808_0x0900_0xF8_USB item = new JT808_0x0900_0xF8_USB();
                    item.USBID = reader.ReadByte();
                    item.MessageLength = reader.ReadByte();
                    item.CompantNameLength = reader.ReadByte();
                    item.CompantName = reader.ReadString(item.CompantNameLength);
                    item.ProductModelLength = reader.ReadByte();
                    item.ProductModel = reader.ReadString(item.ProductModelLength);
                    item.HardwareVersionNumberLength = reader.ReadByte();
                    item.HardwareVersionNumber = reader.ReadString(item.HardwareVersionNumberLength);
                    item.SoftwareVersionNumberLength = reader.ReadByte();
                    item.SoftwareVersionNumber = reader.ReadString(item.SoftwareVersionNumberLength);
                    item.DevicesIDLength = reader.ReadByte();
                    item.DevicesID = reader.ReadString(item.DevicesIDLength);
                    item.CustomerCodeLength = reader.ReadByte();
                    item.CustomerCode = reader.ReadString(item.CustomerCodeLength);
                    value.USBMessages.Add(item);
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
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0900_0xF8 value, IJT808Config config)
        {
            if (value.USBMessages != null && value.USBMessages.Count > 0)
            {
                writer.WriteByte((byte)value.USBMessages.Count);
                foreach (var item in value.USBMessages)
                {
                    writer.WriteByte(item.USBID);
                    writer.Skip(1,out int messageLengthPosition);

                    writer.Skip(1, out int CompantNameLengthPosition);
                    writer.WriteString(item.CompantName);
                    writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - CompantNameLengthPosition - 1), CompantNameLengthPosition);

                    writer.Skip(1, out int ProductModelLengthPosition);
                    writer.WriteString(item.ProductModel);
                    writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - ProductModelLengthPosition - 1), ProductModelLengthPosition);

                    writer.Skip(1, out int HardwareVersionNumberLengthPosition);
                    writer.WriteString(item.HardwareVersionNumber);
                    writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - HardwareVersionNumberLengthPosition - 1), HardwareVersionNumberLengthPosition);

                    writer.Skip(1, out int SoftwareVersionNumberLengthPosition);
                    writer.WriteString(item.SoftwareVersionNumber);
                    writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - SoftwareVersionNumberLengthPosition - 1), SoftwareVersionNumberLengthPosition);

                    writer.Skip(1, out int DevicesIDLengthPosition);
                    writer.WriteString(item.DevicesID);
                    writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - DevicesIDLengthPosition - 1), DevicesIDLengthPosition);

                    writer.Skip(1, out int CustomerCodeLengthPosition);
                    writer.WriteString(item.CustomerCode);
                    writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - CustomerCodeLengthPosition - 1), CustomerCodeLengthPosition);

                    writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - messageLengthPosition - 1), messageLengthPosition);
                }
            }
        }
    }
}
