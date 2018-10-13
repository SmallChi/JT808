using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body;
using JT808.Protocol.MessageBody.JT808LocationAttach;
using System;
using System.Text;

namespace JT808.Protocol
{
    public static class JT808GlobalConfigs
    {
        static JT808GlobalConfigs()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// 根据安装后终端自身的手机号转换。手机号不足 12 位，则在前补充数字，大陆手机号补充数字 0，港澳台则根据其区号进行位数补充
        /// </summary>
        public static JT808PhoneNumberType jT808PhoneNumber = JT808PhoneNumberType.大陆;

        public static void Register_JT808_0x0200_Attach<TJT808LocationAttach>(byte attachInfoId)
               where TJT808LocationAttach : JT808LocationAttachBase
        {
            JT808LocationAttachBase.AddJT808LocationAttachMethod<TJT808LocationAttach>(attachInfoId);
        }
    }
}
