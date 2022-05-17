namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 插拔状态
    /// Plug state
    /// </summary>
    public enum JT808ICCardStatus : byte
    {
        /// <summary>
        /// 从业资格证IC卡插入_驾驶员上班
        /// License IC card inserted_Driver on duty
        /// </summary>
        ic_card_into = 0x01,
        /// <summary>
        /// 从业资格证IC卡拔出_驾驶员下班
        /// License IC card pulled out_driver off duty
        /// </summary>
        ic_card_pull_out = 0x02
    }
}
