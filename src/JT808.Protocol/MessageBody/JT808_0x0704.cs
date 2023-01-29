using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 定位数据批量上传
    /// </summary>
    public class JT808_0x0704 : JT808MessagePackFormatter<JT808_0x0704>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x0704
        /// </summary>
        public ushort MsgId  => 0x0704;
        /// <summary>
        /// 定位数据批量上传
        /// </summary>
        public string Description => "定位数据批量上传";
        /// <summary>
        /// 数据项个数
        /// </summary>
        public ushort Count { get; set; }

        /// <summary>
        /// 位置数据类型
        /// </summary>
        public BatchLocationType LocationType { get; set; }

        /// <summary>
        /// 位置汇报数据集合
        /// </summary>
        public List<JT808_0x0200> Positions { get; set; }
        /// <summary>
        /// 异常错误剩余数据存储
        /// key:count index
        /// value:0200 data
        /// </summary>
        public Dictionary<int,byte[]> ErrorRemainPositions { get; set; }

        /// <summary>
        /// 位置数据类型
        /// </summary>
        public enum BatchLocationType : byte
        {
            /// <summary>
            /// 正常位置批量汇报
            /// </summary>
            正常位置批量汇报 = 0x00,
            /// <summary>
            /// 盲区补报
            /// </summary>
            盲区补报 = 0x01
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0704 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0704 jT808_0X0704 = new JT808_0x0704();
            jT808_0X0704.Count = reader.ReadUInt16();
            jT808_0X0704.LocationType = (JT808_0x0704.BatchLocationType)reader.ReadByte();
            jT808_0X0704.ErrorRemainPositions = new Dictionary<int, byte[]>();
            jT808_0X0704.Positions = new List<JT808_0x0200>();
            for (int i = 0; i < jT808_0X0704.Count; i++)
            {
                int remainContent = reader.ReadCurrentRemainContentLength();
                if (remainContent <= 0) continue;
                int buflen = reader.ReadUInt16();
                if ((remainContent-buflen) >= 0)
                {
                    var buffer = reader.ReadArray(buflen);
                    try
                    {
                        JT808MessagePackReader tmpReader = new JT808MessagePackReader(buffer, reader.Version);
                        JT808_0x0200 jT808_0X0200 = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref tmpReader, config);
                        jT808_0X0704.Positions.Add(jT808_0X0200);
                    }
                    catch
                    {
                        jT808_0X0704.ErrorRemainPositions.Add(i, buffer.ToArray());
                    }
                }
                else
                {
                    int remainContent1 = reader.ReadCurrentRemainContentLength();
                    var buffer = reader.ReadArray(remainContent1);
                    jT808_0X0704.ErrorRemainPositions.Add(i, buffer.ToArray());
                }
            }
            return jT808_0X0704;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0704 value, IJT808Config config)
        {
            if(value.Positions!=null && value.Positions.Count > 0)
            {
                writer.WriteUInt16((ushort)value.Positions.Count);
                writer.WriteByte((byte)value.LocationType);
                foreach (var item in value.Positions)
                {
                    try
                    {
                        writer.Skip(2, out int position);
                        config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, item, config);
                        ushort length = (ushort)(writer.GetCurrentPosition() - position - 2);
                        writer.WriteUInt16Return(length, position);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            else
            {
                writer.WriteUInt16(0);
                writer.WriteByte((byte)value.LocationType);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0704 jT808_0X0704 = new JT808_0x0704();
            jT808_0X0704.Count = reader.ReadUInt16();
            writer.WriteNumber($"[{jT808_0X0704.Count.ReadNumber()}]数据项个数", jT808_0X0704.Count);
            jT808_0X0704.LocationType = (JT808_0x0704.BatchLocationType)reader.ReadByte();
            writer.WriteNumber($"[{((byte)jT808_0X0704.LocationType).ReadNumber()}]位置数据类型-{jT808_0X0704.LocationType}", (byte)jT808_0X0704.LocationType);
            writer.WriteStartArray("位置汇报数据集合");
            for (int i = 0; i < jT808_0X0704.Count; i++)
            {
                int remainContent = reader.ReadCurrentRemainContentLength();
                if (remainContent <= 0) continue;
                writer.WriteStartObject();
                int buflen = reader.ReadUInt16();
                writer.WriteNumber($"[{buflen.ReadNumber()}]位置汇报数据长度", buflen);
                if ((remainContent - buflen) >= 0)
                {
                    writer.WriteString($"位置汇报数据{{{i}}}", reader.ReadVirtualArray(buflen).ToArray().ToHexString());        
                    JT808MessagePackReader tmpReader = new JT808MessagePackReader(reader.ReadArray(buflen), reader.Version);
                    writer.WriteStartObject("位置信息汇报");
                    try
                    {
                        config.GetAnalyze<JT808_0x0200>().Analyze(ref tmpReader, writer, config);
                    }
                    catch (Exception ex)
                    {
                        writer.WriteString($"分析异常", ex.StackTrace);
                    }
                    writer.WriteEndObject();
                }
                else
                {
                    int remainContent1 = reader.ReadCurrentRemainContentLength();
                    var buffer = reader.ReadArray(remainContent1);
                    writer.WriteString($"位置汇报异常数据{{{i}}}", buffer.ToArray().ToHexString());
                }
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
