using JT808.Protocol.Enums;
using JT808.Protocol.MessageBody;

namespace JT808.Protocol.JT808PackageImpl.Send
{
    public class JT808_0x8300Package : JT808PackageBase<JT808_0x8300>
    {
        public JT808_0x8300Package(JT808Header jT808Header, JT808_0x8300 bodies) : base(jT808Header, bodies)
        {
        }

        protected override JT808Package Create(JT808Header jT808Header, JT808_0x8300 bodies)
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header();
            jT808Package.Header.MsgId = JT808MsgId.文本信息下发;
            jT808Package.Header.TerminalPhoneNo = jT808Header.TerminalPhoneNo;
            jT808Package.Header.MsgNum = MsgNum;
            jT808Package.Bodies = bodies;
            return jT808Package;
        }
    }
}
