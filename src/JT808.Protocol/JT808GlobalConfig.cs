using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Internal;
using JT808.Protocol.MessageBody;
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
            Compress = new JT808GZipCompressImpl();
            SkipCRCCode = false;
            Encoding = Encoding.GetEncoding("GBK");
        }

        public IMsgSNDistributed MsgSNDistributed { get; private set; }

        public JT808ICompress Compress { get; private set; }

        public static JT808GlobalConfig Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public Encoding Encoding;

        /// <summary>
        /// 跳过校验码
        /// 测试的时候需要手动修改值，避免验证
        /// 默认：false
        /// </summary>
        public bool SkipCRCCode { get; private set; }

        /// <summary>
        /// 注册自定义定位信息附加数据
        /// </summary>
        /// <typeparam name="TJT808LocationAttach"></typeparam>
        /// <param name="attachInfoId"></param>
        public JT808GlobalConfig Register_0x0200_Attach<TJT808LocationAttach>(byte attachInfoId)
               where TJT808LocationAttach : JT808_0x0200_BodyBase
        {
            if (!JT808_0x0200_BodyBase.JT808LocationAttachMethod.ContainsKey(attachInfoId))
            {
                JT808_0x0200_BodyBase.AddJT808LocationAttachMethod<TJT808LocationAttach>(attachInfoId);
            }
            return instance.Value;
        }

        /// <summary>
        /// 注册自定义定位信息附加数据
        /// </summary>
        /// <typeparam name="attachInfoId"></typeparam>
        /// <param name="type"></param>
        public JT808GlobalConfig Register_0x0200_Attach(byte attachInfoId,Type type)
        {
            if (!JT808_0x0200_BodyBase.JT808LocationAttachMethod.ContainsKey(attachInfoId))
            {
                JT808_0x0200_BodyBase.AddJT808LocationAttachMethod(attachInfoId, type);
            }
            return instance.Value;
        }


        /// <summary>
        /// 注册自定义数据上行透传信息
        /// </summary>
        /// <typeparam name="TJT808_0x0900_Ext"></typeparam>
        /// <param name="passthroughType"></param>
        public JT808GlobalConfig Register_0x0900_Ext<TJT808_0x0900_Ext>(byte passthroughType)
               where TJT808_0x0900_Ext : JT808_0x0900_BodyBase
        {
            if (!JT808_0x0900_BodyBase.JT808_0x0900Method.ContainsKey(passthroughType))
            {
                JT808_0x0900_BodyBase.AddJT808_0x0900Method<TJT808_0x0900_Ext>(passthroughType);
            }
            return instance.Value;
        }
        /// <summary>
        /// 注册自定义数据上行透传信息
        /// </summary>
        /// <typeparam name="passthroughType"></typeparam>
        /// <param name="type"></param>
        public JT808GlobalConfig Register_0x0900_Ext(byte passthroughType,Type type)
        {
            if (!JT808_0x0900_BodyBase.JT808_0x0900Method.ContainsKey(passthroughType))
            {
                JT808_0x0900_BodyBase.AddJT808_0x0900Method(passthroughType, type);
            }
            return instance.Value;
        }
        /// <summary>
        /// 注册自定义数据下行透传信息
        /// </summary>
        /// <typeparam name="TJT808_0x8900_Ext"></typeparam>
        /// <param name="passthroughType"></param>
        public JT808GlobalConfig Register_0x8900_Ext<TJT808_0x8900_Ext>(byte passthroughType)
               where TJT808_0x8900_Ext : JT808_0x8900_BodyBase
        {
            if (!JT808_0x8900_BodyBase.JT808_0x8900Method.ContainsKey(passthroughType))
            {
                JT808_0x8900_BodyBase.AddJT808_0x8900Method<TJT808_0x8900_Ext>(passthroughType);
            }
            return instance.Value;
        }
        /// <summary>
        /// 注册自定义数据下行透传信息
        /// </summary>
        /// <typeparam name="passthroughType"></typeparam>
        /// <param name="type"></param>
        public JT808GlobalConfig Register_0x8900_Ext(byte passthroughType,Type type)
        {
            if (!JT808_0x8900_BodyBase.JT808_0x8900Method.ContainsKey(passthroughType))
            {
                JT808_0x8900_BodyBase.AddJT808_0x8900Method(passthroughType, type);
            }
            return instance.Value;
        }
        /// <summary>
        /// 注册自定义消息
        /// </summary>
        /// <typeparam name="TJT808Bodies"></typeparam>
        /// <param name="msgId"></param>
        /// <returns></returns>
        public JT808GlobalConfig Register_CustomMsgId<TJT808Bodies>(ushort customMsgId)
               where TJT808Bodies : JT808Bodies
        {
            JT808MsgIdFactory.SetMap<TJT808Bodies>(customMsgId);
            return instance.Value;
        }

        /// <summary>
        /// 注册电子运单内容实现类
        /// </summary>
        /// <typeparam name="TJT808_0x0701Body"></typeparam>
        /// <returns></returns>
        public JT808GlobalConfig Register_JT808_0x0701Body<TJT808_0x0701Body>()
               where TJT808_0x0701Body : JT808_0x0701.JT808_0x0701Body
        {
            JT808_0x0701.JT808_0x0701Body.BodyImpl = typeof(TJT808_0x0701Body);
            return instance.Value;
        }

        /// <summary>
        /// 重写消息
        /// </summary>
        /// <typeparam name="TJT808Bodies"></typeparam>
        /// <param name="overwriteMsgId"></param>
        /// <returns></returns>
        public JT808GlobalConfig Overwrite_MsgId<TJT808Bodies>(ushort overwriteMsgId)
               where TJT808Bodies : JT808Bodies
        {
            JT808MsgIdFactory.ReplaceMap<TJT808Bodies>(overwriteMsgId);
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
        /// 设置压缩算法
        /// 默认GZip
        /// </summary>
        /// <param name="compressImpl"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetCompress(JT808ICompress compressImpl)
        {
            instance.Value.Compress = compressImpl;
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
