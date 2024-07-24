using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 存储器故障报警状态
    /// 0x0200_0x17
    /// </summary>
    public class JT808_0x0200_0x17 : JT808MessagePackFormatter<JT808_0x0200_0x17>, JT808_0x0200_CustomBodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoId { get; set; } = 0x17;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte AttachInfoLength { get; set; } = 2;
        /// <summary>
        /// 存储器故障报警状态
        /// </summary>
        public ushort StorageFaultAlarmStatus { get; set; }

        /// <summary>
        /// 存储器故障集合
        /// <para>表示第 1 ~ 12 个主存储器</para>
        /// </summary>
        public List<FaultItem> StorageFault { get; set; } = new();

        /// <summary>
        /// 灾备存储故障集合
        /// <para>表示第 1 ~ 4 个灾备存储装置</para>
        /// </summary>
        public List<FaultItem> DisasterFault { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x17 value = new JT808_0x0200_0x17();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.StorageFaultAlarmStatus = reader.ReadUInt16();
            writer.WriteNumber($"[{value.StorageFaultAlarmStatus.ReadNumber()}]存储器故障报警状态", value.StorageFaultAlarmStatus);


            var storageFaultAlarmStatusSpan = Convert.ToString(value.StorageFaultAlarmStatus, 2).PadLeft(16, '0').AsSpan();
            writer.WriteStartArray("存储器故障报警状态集合");
            FaultItem.Parse(value.StorageFaultAlarmStatus, out var storageFault, out var disasterFault);
            storageFault.ForEach(x => writer.WriteStringValue($"{x.Index}主存储器{(x.Fault ? "故障" : "正常")}"));
            disasterFault.ForEach(x => writer.WriteStringValue($"{x.Index}灾备存储装置{(x.Fault ? "故障" : "正常")}"));
            writer.WriteEndArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x17 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x17 value = new JT808_0x0200_0x17();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.StorageFaultAlarmStatus = reader.ReadUInt16();
            FaultItem.Parse(value.StorageFaultAlarmStatus, out var storageFault, out var disasterFault);
            value.StorageFault = storageFault;
            value.DisasterFault = disasterFault;
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x17 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.StorageFaultAlarmStatus);
        }

        /// <summary>
        /// 故障信息
        /// </summary>
        /// <param name="index">存储器索引，表示第几个存储器，从1开始</param>
        /// <param name="fault">是否存在故障</param>
        public class FaultItem(int index, bool fault)
        {

            /// <summary>
            /// 存储器索引，表示第几个存储器，从1开始
            /// </summary>
            public int Index { get; set; } = index;
            /// <summary>
            /// 是否存在故障
            /// </summary>
            public bool Fault { get; set; } = fault;
            /// <summary>
            /// 解析故障信息
            /// </summary>
            /// <param name="value"></param>
            /// <param name="storageFault"></param>
            /// <param name="disasterFault"></param>
            public static void Parse(ushort value, out List<FaultItem> storageFault, out List<FaultItem> disasterFault)
            {
                disasterFault = storageFault = [];
                for (int i = 0; i < 12; i++)
                {
                    storageFault.Add(new(i + 1, ((value >> i) & 1) > 0));
                }
                for (int i = 12; i < 16; i++)
                {
                    disasterFault.Add(new(i + 1, ((value >> i) & 1) > 0));
                }
            }
        }
    }
}
