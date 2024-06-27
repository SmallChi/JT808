using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using JT808.Protocol.Extensions.GPS51.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions.GPS51.MessageBody
{
    /// <summary>
    /// 4+7*N
    /// 基站编码的格式为 MCC MNC LAC CI Signal 2-2-2-4-1-2-4-1，其中MCC 2个字节国家编码，MNC 为 2个字节网络编码，
    /// LAC为 2个字节地区编码,CI 为 4个字节蜂窝 ID, 信号强度 1字节,单基站可以不用信号强度 1cc-0-696a-863a8d0-0
    /// </summary>
    public class JT808_0x0200_0xe1 : JT808MessagePackFormatter<JT808_0x0200_0xe1>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808_GPS51_Constants.JT808_0x0200_0xe1;
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// 国家编码
        /// </summary>
        public ushort MCC { get; set; }
        /// <summary>
        /// 网络编码
        /// </summary>
        public ushort MNC { get; set; }

        public List<BaseStation> BaseStations { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0xe1 value = new JT808_0x0200_0xe1();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.MCC = reader.ReadUInt16();
            writer.WriteNumber($"[{value.MCC.ReadNumber()}]国家编码", value.MCC);
            value.MNC=reader.ReadUInt16();
            writer.WriteNumber($"[{value.MNC.ReadNumber()}]网络编码", value.MNC);
            writer.WriteStartArray("地区编码列表");
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                var LAC = reader.ReadUInt16();
                writer.WriteNumber($"[{LAC.ReadNumber()}]地区编码", LAC);            
                var CI = reader.ReadUInt32();
                writer.WriteNumber($"[{CI.ReadNumber()}]蜂窝 ID", CI);
                if (reader.ReadCurrentRemainContentLength() > 0)
                {
                    var Signal = reader.ReadByte();
                    writer.WriteNumber($"[{Signal.ReadNumber()}]信号强度", Signal);
                }
            }
            writer.WriteEndArray();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0xe1 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0xe1 value = new JT808_0x0200_0xe1();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            if (value.AttachInfoLength > 0) {
                value.MCC = reader.ReadUInt16();
                value.MNC = reader.ReadUInt16();
                value.BaseStations = new List<BaseStation>();
                while (reader.ReadCurrentRemainContentLength()> 0)
                {
                    BaseStation baseStation = new BaseStation();
                    baseStation.LAC = reader.ReadUInt16();
                    baseStation.CI = reader.ReadUInt32();
                    if (reader.ReadCurrentRemainContentLength() > 0) {
                        baseStation.Signal=reader.ReadByte();
                    }
                    value.BaseStations.Add(baseStation);
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
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0xe1 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.Skip(1,out int position);
            writer.WriteUInt16(value.MCC);
            writer.WriteUInt16(value.MNC);
            foreach (var baseStation in value.BaseStations)
            {
                writer.WriteUInt16(baseStation.LAC);
                writer.WriteUInt32(baseStation.CI);
                if (value.BaseStations.Count >1) {
                    writer.WriteByte(baseStation.Signal);
                }
            }
            var length = writer.GetCurrentPosition() - position - 1;
            writer.WriteByteReturn((byte)length, position);
        }
    }

    public class BaseStation() {
        /// <summary>
        /// 地区编码
        /// </summary>
        public ushort LAC { get; set; }
        /// <summary>
        /// 蜂窝 ID
        /// </summary>
        public uint CI { get; set; }
        /// <summary>
        /// 信号强度
        /// </summary>
        public byte Signal { get; set; }
    }
}
