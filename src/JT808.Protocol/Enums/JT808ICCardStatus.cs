namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 插拔状态
    /// </summary>
    public enum JT808ICCardStatus : byte
    {
        从业资格证IC卡插入_驾驶员上班 = 0x01,
        从业资格证IC卡拔出_驾驶员下班 = 0x02
    }
}
