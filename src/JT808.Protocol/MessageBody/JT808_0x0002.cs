namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端心跳
    /// </summary>
    public class JT808_0x0002 : JT808Bodies
    {
        public override bool SkipSerialization { get; set; } = true;
    }
}
