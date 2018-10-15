using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Internal;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body;
using JT808.Protocol.MessageBody.JT808LocationAttach;
using System;
using System.Text;

namespace JT808.Protocol
{
    public class JT808GlobalConfig
    {
        private static readonly Lazy<JT808GlobalConfig> instance = new Lazy<JT808GlobalConfig>(() => new JT808GlobalConfig());

        private JT808GlobalConfig()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MsgSNDistributed = new DefaultMsgSNDistributedImpl();
            SkipCRCCode = false;
        }

        public IMsgSNDistributed MsgSNDistributed { get; private set; }

        public static JT808GlobalConfig Instance
        {
            get
            {
                return instance.Value;
            }
        }

        /// <summary>
        /// 跳过校验码
        /// 默认：false
        /// </summary>
        public bool SkipCRCCode { get; private set; }

        /// <summary>
        /// 注册自定义定位信息附加数据
        /// </summary>
        /// <typeparam name="TJT808LocationAttach"></typeparam>
        /// <param name="attachInfoId"></param>
        public JT808GlobalConfig Register_0x0200_Attach<TJT808LocationAttach>(byte attachInfoId)
               where TJT808LocationAttach : JT808LocationAttachBase
        {
            JT808LocationAttachBase.AddJT808LocationAttachMethod<TJT808LocationAttach>(attachInfoId);
            return instance.Value;
        }

        /// <summary>
        /// 注册自定义数据上行透传信息
        /// </summary>
        /// <typeparam name="TJT808_0x0900_Ext"></typeparam>
        /// <param name="attachInfoId"></param>
        public JT808GlobalConfig Register_0x0900_Ext<TJT808_0x0900_Ext>(byte passthroughType)
               where TJT808_0x0900_Ext : JT808_0x0900_BodyBase
        {
            JT808_0x0900_BodyBase.AddJT808_0x0900Method<TJT808_0x0900_Ext>(passthroughType);
            return instance.Value;
        }

        /// <summary>
        /// 注册自定义数据下行透传信息
        /// </summary>
        /// <typeparam name="TJT808_0x8900_Ext"></typeparam>
        /// <param name="attachInfoId"></param>
        public JT808GlobalConfig Register_0x8900_Ext<TJT808_0x8900_Ext>(byte passthroughType)
               where TJT808_0x8900_Ext : JT808_0x8900_BodyBase
        {
            JT808_0x8900_BodyBase.AddJT808_0x8900Method<TJT808_0x8900_Ext>(passthroughType);
            return instance.Value;
        }

        /// <summary>
        /// 设置消息序列号
        /// </summary>
        /// <param name="msgSNDistributed"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetMsgSNDistributed(IMsgSNDistributed msgSNDistributed)
        {
            instance.Value.MsgSNDistributed = msgSNDistributed;
            return instance.Value;
        }

        /// <summary>
        /// 设置跳过校验码
        /// 场景：测试的时候，可能需要收到改数据，所以测试的时候有用
        /// </summary>
        /// <param name="skipCRCCode"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetSkipCRCCode(bool skipCRCCode)
        {
            instance.Value.SkipCRCCode = skipCRCCode;
            return instance.Value;
        }
    }
}
