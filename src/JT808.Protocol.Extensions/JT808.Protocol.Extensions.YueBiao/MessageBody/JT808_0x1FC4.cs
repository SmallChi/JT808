using JT808.Protocol.Enums;
using JT808.Protocol.Extensions.YueBiao.Enums;
using JT808.Protocol.Extensions.YueBiao.Metadata;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.Extensions.YueBiao.MessageBody
{
    /// <summary>
    /// 终端升级进度上报
    /// </summary>
    public class JT808_0x1FC4 : JT808MessagePackFormatter<JT808_0x1FC4>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 终端升级进度上报
        /// </summary>
        public string Description => "终端升级进度上报";
        /// <summary>
        /// 终端升级进度上报
        /// </summary>
        public ushort MsgId => JT808_YueBiao_MsgId.terminal_upgrade_progress_reported.ToUInt16Value();
        /// <summary>
        /// 流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 升级类型
        /// </summary>
        public JT808UpgradeType UpgradeType { get; set; }
        /// <summary>
        /// 升级状态
        /// </summary>
        public JT808UpgradeStatus UpgradeStatus { get; set; }
        /// <summary>
        /// 升级进度
        /// 0-100
        /// </summary>
        public byte UploadProgress { get; set; }
        /// <summary>
        /// 错误码
        /// 由厂家自定义
        /// </summary>
        public byte ErrorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x1FC4 value = new JT808_0x1FC4();
            value.MsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.MsgNum.ReadNumber()}]流水号", value.MsgNum);
            value.UpgradeType = (JT808UpgradeType)reader.ReadByte();
            writer.WriteString($"[{value.UpgradeType.ToByteValue().ReadNumber()}]升级类型", value.UpgradeType.ToString());
            value.UpgradeStatus = (JT808UpgradeStatus)reader.ReadByte();
            writer.WriteString($"[{value.UpgradeStatus.ToByteValue().ReadNumber()}]升级状态", value.UpgradeStatus.ToString());
            value.UploadProgress = reader.ReadByte();
            writer.WriteNumber($"[{value.UploadProgress.ReadNumber()}]升级进度", UploadProgress);
            value.ErrorCode = reader.ReadByte();
            writer.WriteNumber($"[{value.ErrorCode.ReadNumber()}]错误码", ErrorCode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x1FC4 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x1FC4 value = new JT808_0x1FC4();
            value.MsgNum = reader.ReadUInt16();
            value.UpgradeType = (JT808UpgradeType)reader.ReadByte();
            value.UpgradeStatus = (JT808UpgradeStatus)reader.ReadByte();
            value.UploadProgress = reader.ReadByte();
            value.ErrorCode = reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x1FC4 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteByte(value.UpgradeType.ToByteValue());
            writer.WriteByte(value.UpgradeStatus.ToByteValue());
            writer.WriteByte(value.UploadProgress);
            writer.WriteByte(value.ErrorCode);
        }
    }
}
