using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters;

namespace JT808.Protocol
{
    /// <summary>
    /// 头部
    /// </summary>
    [JT808Formatter(typeof(JT808HeaderFormatter))]
    public class JT808Header
    {
        /// <summary>
        /// 消息ID 
        /// <see cref="JT808.Protocol.Enums.JT808MsgId"/>
        /// </summary>
        public ushort MsgId { get; set; }
        public JT808HeaderMessageBodyProperty MessageBodyProperty { get; set; } = new JT808HeaderMessageBodyProperty();
        /// <summary>
        /// 终端手机号
        /// 根据安装后终端自身的手机号转换。手机号不足 12 位，则在前补充数字，大陆手机号补充数字 0，港澳台则根据其区号进行位数补充
        /// </summary>
        public string TerminalPhoneNo { get;  set; }
        /// <summary>
        /// 消息流水号
        /// 发送计数器
        /// 占用四个字节，为发送信息的序列号，用于接收方检测是否有信息的丢失，上级平台和下级平台接自己发送数据包的个数计数，互不影响。
        /// 程序开始运行时等于零，发送第一帧数据时开始计数，到最大数后自动归零
        /// </summary>
        public ushort MsgNum { get;  set; }
    }
}
