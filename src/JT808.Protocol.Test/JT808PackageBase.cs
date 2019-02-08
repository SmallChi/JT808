namespace JT808.Protocol.Test
{
    public class JT808PackageBase
    {
        public JT808PackageBase()
        {
            JT808GlobalConfig.Instance.SetSkipCRCCode(true);
        }
    }
}
