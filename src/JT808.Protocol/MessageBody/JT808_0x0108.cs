using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端升级结果通知
    /// </summary>
    public class JT808_0x0108 : JT808MessagePackFormatter<JT808_0x0108>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x0108
        /// </summary>
        public ushort MsgId  => 0x0108;
        /// <summary>
        /// 终端升级结果通知
        /// </summary>
        public string Description => "终端升级结果通知";
        /// <summary>
        /// 升级类型
        /// 0：终端，12：道路运输证 IC 卡读卡器，52：北斗卫星定位模块
        /// </summary>
        public JT808UpgradeType UpgradeType { get; set; }

        /// <summary>
        /// 升级结果
        /// 0：成功，1：失败，2：取消
        /// </summary>
        public JT808UpgradeResult UpgradeResult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0108 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0108 jT808_0X0108 = new JT808_0x0108();
            jT808_0X0108.UpgradeType = (JT808UpgradeType)reader.ReadByte();
            jT808_0X0108.UpgradeResult = (JT808UpgradeResult)reader.ReadByte();
            return jT808_0X0108;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0108 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.UpgradeType);
            writer.WriteByte((byte)value.UpgradeResult);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0108 jT808_0X0108 = new JT808_0x0108();
            jT808_0X0108.UpgradeType = (JT808UpgradeType)reader.ReadByte();
            jT808_0X0108.UpgradeResult = (JT808UpgradeResult)reader.ReadByte();
            writer.WriteString($"[{((byte)jT808_0X0108.UpgradeType).ReadNumber()}]升级类型", jT808_0X0108.UpgradeType.ToString());
            writer.WriteString($"[{((byte)jT808_0X0108.UpgradeResult).ReadNumber()}]升级结果", jT808_0X0108.UpgradeResult.ToString());
        }
    }
}
