using JT808.Protocol.Enums;

namespace JT808.Protocol.Extensions
{
    /// <summary>
    /// JT808创建包扩展
    /// </summary>
    public static partial class JT808PackageExtensions
    {
        /// <summary>
        /// 根据消息Id创建包
        /// </summary>
        /// <typeparam name="TJT808Bodies"></typeparam>
        /// <param name="msgId"></param>
        /// <param name="terminalPhoneNo"></param>
        /// <param name="bodies"></param>
        /// <returns></returns>
        public static JT808Package Create<TJT808Bodies>(this JT808MsgId msgId, string terminalPhoneNo, TJT808Bodies bodies)
            where TJT808Bodies : JT808Bodies
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = (ushort)msgId,
                    TerminalPhoneNo = terminalPhoneNo,
                },
                Bodies = bodies
            };
            return jT808Package;
        }
        /// <summary>
        /// 根据消息Id创建包
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="terminalPhoneNo"></param>
        /// <returns></returns>
        public static JT808Package Create(this JT808MsgId msgId, string terminalPhoneNo)
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = (ushort)msgId,
                    TerminalPhoneNo = terminalPhoneNo,
                }
            };
            return jT808Package;
        }
        /// <summary>
        /// 根据自定义消息Id创建包
        /// </summary>
        /// <typeparam name="TJT808Bodies"></typeparam>
        /// <param name="msgId"></param>
        /// <param name="terminalPhoneNo"></param>
        /// <param name="bodies"></param>
        /// <returns></returns>
        public static JT808Package CreateCustomMsgId<TJT808Bodies>(this ushort msgId, string terminalPhoneNo, TJT808Bodies bodies)
            where TJT808Bodies : JT808Bodies
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = msgId,
                    TerminalPhoneNo = terminalPhoneNo
                },
                Bodies = bodies
            };
            return jT808Package;
        }
        /// <summary>
        /// 根据自定义消息Id创建包
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="terminalPhoneNo"></param>
        /// <returns></returns>
        public static JT808Package CreateCustomMsgId(this ushort msgId, string terminalPhoneNo)
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = msgId,
                    TerminalPhoneNo = terminalPhoneNo
                }
            };
            return jT808Package;
        }
        /// <summary>
        /// 根据消息Id创建2019版本包
        /// </summary>
        /// <typeparam name="TJT808Bodies"></typeparam>
        /// <param name="msgId"></param>
        /// <param name="terminalPhoneNo"></param>
        /// <param name="bodies"></param>
        /// <returns></returns>
        public static JT808Package Create2019<TJT808Bodies>(this JT808MsgId msgId, string terminalPhoneNo, TJT808Bodies bodies)
            where TJT808Bodies : JT808Bodies
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = (ushort)msgId,
                    TerminalPhoneNo = terminalPhoneNo,
                },
                Bodies = bodies
            };
            jT808Package.Header.MessageBodyProperty.VersionFlag = true;
            return jT808Package;
        }
        /// <summary>
        /// 根据消息Id创建2019版本包
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="terminalPhoneNo"></param>
        /// <returns></returns>
        public static JT808Package Create2019(this JT808MsgId msgId, string terminalPhoneNo)
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = (ushort)msgId,
                    TerminalPhoneNo = terminalPhoneNo,
                }
            };
            jT808Package.Header.MessageBodyProperty.VersionFlag = true;
            return jT808Package;
        }
        /// <summary>
        /// 根据自定义消息Id创建2019版本包
        /// </summary>
        /// <typeparam name="TJT808Bodies"></typeparam>
        /// <param name="msgId"></param>
        /// <param name="terminalPhoneNo"></param>
        /// <param name="bodies"></param>
        /// <returns></returns>
        public static JT808Package CreateCustomMsgId2019<TJT808Bodies>(this ushort msgId, string terminalPhoneNo, TJT808Bodies bodies)
            where TJT808Bodies : JT808Bodies
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = msgId,
                    TerminalPhoneNo = terminalPhoneNo
                },
                Bodies = bodies
            };
            jT808Package.Header.MessageBodyProperty.VersionFlag = true;
            return jT808Package;
        }
        /// <summary>
        /// 根据自定义消息Id创建2019版本包
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="terminalPhoneNo"></param>
        /// <returns></returns>
        public static JT808Package CreateCustomMsgId2019(this ushort msgId, string terminalPhoneNo)
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = msgId,
                    TerminalPhoneNo = terminalPhoneNo
                }
            };
            jT808Package.Header.MessageBodyProperty.VersionFlag = true;
            return jT808Package;
        }
    }
}
