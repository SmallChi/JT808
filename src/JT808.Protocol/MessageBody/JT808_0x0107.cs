using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections;
using System.Linq;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询终端属性应答
    /// </summary>
    public class JT808_0x0107 : JT808MessagePackFormatter<JT808_0x0107>, JT808Bodies, IJT808_2019_Version,IJT808Analyze
    {
        /// <summary>
        /// 0x0107
        /// </summary>
        public ushort MsgId => 0x0107;
        /// <summary>
        /// 查询终端属性应答
        /// </summary>
        public string Description => "查询终端属性应答";
        /// <summary>
        /// 终端类型
        /// bit0，0：不适用客运车辆，1：适用客运车辆；
        /// bit1，0：不适用危险品车辆，1：适用危险品车辆；
        /// bit2，0：不适用普通货运车辆，1：适用普通货运车辆；
        /// bit3，0：不适用出租车辆，1：适用出租车辆；
        /// bit6，0：不支持硬盘录像，1：支持硬盘录像；
        /// bit7，0：一体机，1：分体机
        /// </summary>
        public ushort TerminalType { get; set; }
        /// <summary>
        /// 制造商 ID
        /// 2013版本 5 个字节，终端制造商编码
        /// 2019版本 11 个字节，终端制造商编码
        /// </summary>
        public string MakerId { get; set; }
        /// <summary>
        /// 终端型号
        /// BYTE[20] 20 个字节，此终端型号由制造商自行定义，位数不足时，后补“0X00”。
        /// 2019版本
        /// BYTE[30] 30 个字节，此终端型号由制造商自行定义，位数不足时，后补“0X00”。
        /// </summary>
        public string TerminalModel { get; set; }
        /// <summary>
        /// 终端ID 
        /// BYTE[7]  7 个字节，由大写字母和数字组成，此终端 ID 由制造商自行定义，位数不足时，后补“0X00”
        /// 2019版本
        /// BYTE[30]  30 个字节，由大写字母和数字组成，此终端 ID 由制造商自行定义，位数不足时，后补“0X00”
        /// </summary>
        public string TerminalId { get; set; }
        /// <summary>
        /// 终端 SIM 卡 ICCID
        /// BCD[10]
        /// </summary>
        public string Terminal_SIM_ICCID { get; set; }
        /// <summary>
        /// 终端硬件版本号长度
        /// </summary>
        public byte Terminal_Hardware_Version_Length { get; set; }
        /// <summary>
        /// 终端硬件版本号
        /// </summary>
        public string Terminal_Hardware_Version_Num { get; set; }
        /// <summary>
        /// 终端固件版本号长度
        /// </summary>
        public byte Terminal_Firmware_Version_Length { get; set; }
        /// <summary>
        /// 终端固件版本号
        /// </summary>
        public string Terminal_Firmware_Version_Num { get; set; }
        /// <summary>
        /// GNSS 模块属性
        /// bit0，0：不支持 GPS 定位， 1：支持 GPS 定位；
        /// bit1，0：不支持北斗定位， 1：支持北斗定位；
        /// bit2，0：不支持 GLONASS 定位， 1：支持 GLONASS 定位；
        /// bit3，0：不支持 Galileo 定位， 1：支持 Galileo 定位
        /// </summary>
        public byte GNSSModule { get; set; }
        /// <summary>
        /// 通信模块属性
        /// bit0，0：不支持GPRS通信， 1：支持GPRS通信；
        /// bit1，0：不支持CDMA通信， 1：支持CDMA通信；
        /// bit2，0：不支持TD-SCDMA通信， 1：支持TD-SCDMA通信；
        /// bit3，0：不支持WCDMA通信， 1：支持WCDMA通信；
        /// bit4，0：不支持CDMA2000通信， 1：支持CDMA2000通信。
        /// bit5，0：不支持TD-LTE通信， 1：支持TD-LTE通信；
        /// bit7，0：不支持其他通信方式， 1：支持其他通信方式
        /// </summary>
        public byte CommunicationModule { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0107 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0107 jT808_0X0107 = new JT808_0x0107();
            jT808_0X0107.TerminalType = reader.ReadUInt16();
            if(reader.Version== JT808Version.JTT2019)
            {
                jT808_0X0107.MakerId = reader.ReadString(11);
                jT808_0X0107.TerminalModel = reader.ReadString(30);
                jT808_0X0107.TerminalId = reader.ReadString(30);
            }
            else
            {
                jT808_0X0107.MakerId = reader.ReadString(5);
                jT808_0X0107.TerminalModel = reader.ReadString(20);
                jT808_0X0107.TerminalId = reader.ReadString(7);
            }
            jT808_0X0107.Terminal_SIM_ICCID = reader.ReadBCD(20, config.Trim);
            jT808_0X0107.Terminal_Hardware_Version_Length = reader.ReadByte();
            jT808_0X0107.Terminal_Hardware_Version_Num = reader.ReadString(jT808_0X0107.Terminal_Hardware_Version_Length);
            jT808_0X0107.Terminal_Firmware_Version_Length = reader.ReadByte();
            jT808_0X0107.Terminal_Firmware_Version_Num = reader.ReadString(jT808_0X0107.Terminal_Firmware_Version_Length);
            jT808_0X0107.GNSSModule = reader.ReadByte();
            jT808_0X0107.CommunicationModule = reader.ReadByte();
            return jT808_0X0107;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0107 value, IJT808Config config)
        {
            writer.WriteUInt16(value.TerminalType);
            if (writer.Version == JT808Version.JTT2019)
            {
                writer.WriteString(value.MakerId.PadRight(11, '\0').ValiString(nameof(value.MakerId),11));
                writer.WriteString(value.TerminalModel.PadRight(30, '\0').ValiString(nameof(value.TerminalModel), 30));
                writer.WriteString(value.TerminalId.PadRight(30, '\0').ValiString(nameof(value.TerminalId), 30));
            }
            else
            {
                writer.WriteString(value.MakerId.PadRight(5, '\0').ValiString(nameof(value.MakerId), 5));
                writer.WriteString(value.TerminalModel.PadRight(20, '\0').ValiString(nameof(value.TerminalModel), 20));
                writer.WriteString(value.TerminalId.PadRight(7, '\0').ValiString(nameof(value.TerminalId), 7));
            }
            writer.WriteBCD(value.Terminal_SIM_ICCID.ValiString(nameof(value.Terminal_SIM_ICCID), 20), 20);
            writer.WriteByte((byte)value.Terminal_Hardware_Version_Num.Length);
            writer.WriteString(value.Terminal_Hardware_Version_Num);
            writer.WriteByte((byte)value.Terminal_Firmware_Version_Num.Length);
            writer.WriteString(value.Terminal_Firmware_Version_Num);
            writer.WriteByte(value.GNSSModule);
            writer.WriteByte(value.CommunicationModule);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public  void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0107 jT808_0X0107 = new JT808_0x0107();
            jT808_0X0107.TerminalType = reader.ReadUInt16();
            writer.WriteNumber($"[{jT808_0X0107.TerminalType.ReadNumber()}]终端类型", jT808_0X0107.TerminalType);
            ReadOnlySpan<char> terminalTypeBits =string.Join("", Convert.ToString(jT808_0X0107.TerminalType, 2).PadLeft(16, '0').Reverse()).AsSpan();
            writer.WriteStartObject("终端类型");
            writer.WriteString("bit0", terminalTypeBits[0] == '0' ? "不适用客运车辆" : "适用客运车辆");
            writer.WriteString("bit1", terminalTypeBits[1] == '0' ? "不适用危险品车辆" : "适用危险品车辆");
            writer.WriteString("bit2", terminalTypeBits[2] == '0' ? "不适用普通货运车辆" : "适用普通货运车辆");
            writer.WriteString("bit3", terminalTypeBits[3] == '0' ? "不适用出租车辆" : "适用出租车辆");
            writer.WriteString("bit6", terminalTypeBits[6] == '0' ? "不支持硬盘录像" : "支持硬盘录像");
            writer.WriteString("bit7", terminalTypeBits[7] == '0' ? "一体机" : "分体机");
            if (reader.Version == JT808Version.JTT2019)
            {
                writer.WriteString("bit8", terminalTypeBits[8] == '0' ? "不适用挂车" : "适用挂车");
            }
            writer.WriteEndObject();
            if (reader.Version == JT808Version.JTT2019)
            {
                ReadOnlySpan<byte> makerIdSpan = reader.ReadVirtualArray(11);
                jT808_0X0107.MakerId = reader.ReadString(11);
                writer.WriteString($"[{makerIdSpan.ToArray().ToHexString()}]制造商ID", jT808_0X0107.MakerId);
                ReadOnlySpan<byte> terminalModelSpan = reader.ReadVirtualArray(30);
                jT808_0X0107.TerminalModel = reader.ReadString(30);
                writer.WriteString($"[{terminalModelSpan.ToArray().ToHexString()}]终端型号", jT808_0X0107.TerminalModel);
                ReadOnlySpan<byte> terminalIdSpan = reader.ReadVirtualArray(30);
                jT808_0X0107.TerminalId = reader.ReadString(30);
                writer.WriteString($"[{terminalIdSpan.ToArray().ToHexString()}]终端ID", jT808_0X0107.TerminalId);
            }
            else
            {
                ReadOnlySpan<byte> makerIdSpan = reader.ReadVirtualArray(5);
                jT808_0X0107.MakerId = reader.ReadString(5);
                writer.WriteString($"[{makerIdSpan.ToArray().ToHexString()}]制造商ID", jT808_0X0107.MakerId);
                ReadOnlySpan<byte> terminalModelSpan = reader.ReadVirtualArray(20);
                jT808_0X0107.TerminalModel = reader.ReadString(20);
                writer.WriteString($"[{terminalModelSpan.ToArray().ToHexString()}]终端型号", jT808_0X0107.TerminalModel);
                ReadOnlySpan<byte> terminalIdSpan = reader.ReadVirtualArray(7);
                jT808_0X0107.TerminalId = reader.ReadString(7);
                writer.WriteString($"[{terminalIdSpan.ToArray().ToHexString()}]终端ID", jT808_0X0107.TerminalId);
            }
            ReadOnlySpan<byte> iccidSpan = reader.ReadVirtualArray(10);
            jT808_0X0107.Terminal_SIM_ICCID = reader.ReadBCD(20, config.Trim);
            writer.WriteString($"[{iccidSpan.ToArray().ToHexString()}]终端SIM卡ICCID", jT808_0X0107.Terminal_SIM_ICCID);
            jT808_0X0107.Terminal_Hardware_Version_Length = reader.ReadByte();
            writer.WriteNumber($"[{jT808_0X0107.Terminal_Hardware_Version_Length.ReadNumber()}]终端硬件版本号长度", jT808_0X0107.Terminal_Hardware_Version_Length);
            ReadOnlySpan<byte> hardwareVersionNumSpan = reader.ReadVirtualArray(jT808_0X0107.Terminal_Hardware_Version_Length);
            jT808_0X0107.Terminal_Hardware_Version_Num = reader.ReadString(jT808_0X0107.Terminal_Hardware_Version_Length);
            writer.WriteString($"[{hardwareVersionNumSpan.ToArray().ToHexString()}]终端硬件版本号", jT808_0X0107.Terminal_Hardware_Version_Num);
            jT808_0X0107.Terminal_Firmware_Version_Length = reader.ReadByte();
            writer.WriteNumber($"[{jT808_0X0107.Terminal_Firmware_Version_Length.ReadNumber()}]终端硬件版本号长度", jT808_0X0107.Terminal_Firmware_Version_Length);
            ReadOnlySpan<byte> firmwareVersionNumSpan = reader.ReadVirtualArray(jT808_0X0107.Terminal_Firmware_Version_Length);
            jT808_0X0107.Terminal_Firmware_Version_Num = reader.ReadString(jT808_0X0107.Terminal_Firmware_Version_Length);
            writer.WriteString($"[{firmwareVersionNumSpan.ToArray().ToHexString()}]终端固件版本号", jT808_0X0107.Terminal_Firmware_Version_Num);
            jT808_0X0107.GNSSModule = reader.ReadByte();
            ReadOnlySpan<char> gNSSModuleBits =string.Join("", Convert.ToString(jT808_0X0107.GNSSModule, 2).PadLeft(8,'0').Reverse()).AsSpan();
            writer.WriteNumber($"[{jT808_0X0107.GNSSModule.ReadNumber()}]GNSS模块属性", jT808_0X0107.GNSSModule);
            writer.WriteStartObject("GNSS模块属性");
            writer.WriteString("bit0", gNSSModuleBits[0] == '0' ? "不支持GPS定位" : "支持GPS定位");
            writer.WriteString("bit1", gNSSModuleBits[1] == '0' ? "不支持北斗定位" : "支持北斗定位");
            writer.WriteString("bit2", gNSSModuleBits[2] == '0' ? "不支持GLONASS定位" : "支持GLONASS定位");
            writer.WriteString("bit3", gNSSModuleBits[3] == '0' ? "不支持Galileo定位" : "支持Galileo定位");
            writer.WriteEndObject();
            jT808_0X0107.CommunicationModule = reader.ReadByte();
            ReadOnlySpan<char> communicationModuleBits=string.Join("",Convert.ToString(jT808_0X0107.CommunicationModule, 2).PadLeft(8, '0').Reverse()).AsSpan();
            writer.WriteNumber($"[{jT808_0X0107.CommunicationModule.ReadNumber()}]通信模块属性", jT808_0X0107.CommunicationModule);
            writer.WriteStartObject("通信模块属性");
            writer.WriteString("bit0", communicationModuleBits[0] == '0' ? "不支持GPRS通信" : "支持GPRS通信");
            writer.WriteString("bit1", communicationModuleBits[1] == '0' ? "不支持CDMA通信" : "支持CDMA通信");
            writer.WriteString("bit2", communicationModuleBits[2] == '0' ? "不支持TD-SCDMA通信" : "支持TD-SCDMA通信");
            writer.WriteString("bit3", communicationModuleBits[3] == '0' ? "不支持WCDMA通信" : "支持WCDMA通信");
            writer.WriteString("bit4", communicationModuleBits[4] == '0' ? "不支持CDMA2000通信" : "支持CDMA2000通信");
            writer.WriteString("bit5", communicationModuleBits[5] == '0' ? "不支持TD-LTE通信" : "支持TD-LTE通信");
            writer.WriteString("bit6", "保留");
            writer.WriteString("bit7", communicationModuleBits[7] == '0' ? "不支持其他通信方式" : "不支持其他通信方式");
            writer.WriteEndObject();
        }
    }
}
