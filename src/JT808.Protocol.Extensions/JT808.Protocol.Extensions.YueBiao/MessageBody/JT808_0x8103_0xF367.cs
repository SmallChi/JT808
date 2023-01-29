using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.Extensions.YueBiao.MessageBody
{
    /// <summary>
    /// 盲区监测系统参数
    /// </summary>
    public class JT808_0x8103_0xF367 : JT808MessagePackFormatter<JT808_0x8103_0xF367>, JT808_0x8103_BodyBase,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 盲区监测系统参数Id
        /// </summary>
        public uint ParamId { get; set; } = JT808_YueBiao_Constants.JT808_0X8103_0xF367;
        /// <summary>
        /// 盲区监测系统参数长度
        /// </summary>
        public byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 后方接近报警时间阈值
        /// </summary>
        public byte RearApproachAlarmTimeThreshold { get; set; }
        /// <summary>
        /// 侧后方接近报警时间阈值
        /// </summary>
        public byte LateralRearApproachAlarmTimeThreshold { get; set; }
        /// <summary>
        /// 盲区监测系统参数
        /// </summary>
        public string Description => "盲区监测系统参数";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0xF367 value = new JT808_0x8103_0xF367();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            writer.WriteNumber($"[{ value.ParamId.ReadNumber()}]参数ID", value.ParamId);
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]参数长度", value.ParamLength);
            value.RearApproachAlarmTimeThreshold = reader.ReadByte();
            writer.WriteNumber($"[{value.RearApproachAlarmTimeThreshold.ReadNumber()}]后方接近报警时间阈值", value.RearApproachAlarmTimeThreshold);
            value.LateralRearApproachAlarmTimeThreshold = reader.ReadByte();
            writer.WriteNumber($"[{value.LateralRearApproachAlarmTimeThreshold.ReadNumber()}]侧后方接近报警时间阈值", value.LateralRearApproachAlarmTimeThreshold);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0xF367 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0xF367 value = new JT808_0x8103_0xF367();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            value.RearApproachAlarmTimeThreshold = reader.ReadByte();
            value.LateralRearApproachAlarmTimeThreshold = reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0xF367 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(2);
            writer.WriteByte(value.RearApproachAlarmTimeThreshold);
            writer.WriteByte(value.LateralRearApproachAlarmTimeThreshold);
        }
    }
}
