using JT808.Protocol.JT808Internal;
using JT808.Protocol.MessageBody;
using System;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("JT808.Protocol.Benchmark")]
[assembly: InternalsVisibleTo("JT808.Protocol.Test")]
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
            SplitPackageStrategy = new DefaultSplitPackageStrategyImpl();
            SkipCRCCode = false;
            Encoding = Encoding.GetEncoding("GBK");
        }

        public IMsgSNDistributed MsgSNDistributed { get; private set; }

        public IJT808ICompress Compress { get; private set; }

        public ISplitPackageStrategy SplitPackageStrategy { get; private set; }

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
        /// <typeparam name="attachInfoId"></typeparam>
        public JT808GlobalConfig Register_0x0200_Attach(params byte[] attachInfoId)
        {
            if (attachInfoId != null && attachInfoId.Length > 0)
            {
                foreach (var id in attachInfoId)
                {
                    if (!JT808_0x0200_CustomBodyBase.CustomAttachIds.Contains(id))
                    {
                        JT808_0x0200_CustomBodyBase.CustomAttachIds.Add(id);
                    }
                }
            }
            return instance.Value;
        }

        /// <summary>
        /// 注册自定义设置终端参数Id
        /// <see cref="typeof(JT808.Protocol.MessageBody.JT808_0x8103_BodyBase)"/>
        /// <see cref="typeof(实现JT808_0x8103_BodyBase)"/>
        /// <returns></returns>
        public JT808GlobalConfig Register_0x8103_ParamId(uint paramId, Type type)
        {
            JT808_0x8103_BodyBase.AddJT808_0x8103Method(paramId, type);
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
        public JT808GlobalConfig SetCompress(IJT808ICompress compressImpl)
        {
            instance.Value.Compress = compressImpl;
            return instance.Value;
        }
        /// <summary>
        /// 设置分包算法
        /// 默认3*256
        /// </summary>
        /// <param name="splitPackageStrategy"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetSplitPackageStrategy(ISplitPackageStrategy splitPackageStrategy)
        {
            instance.Value.SplitPackageStrategy = splitPackageStrategy;
            return instance.Value;
        }
        /// <summary>
        /// 设置跳过校验码
        /// 场景：测试的时候，可能需要手动改数据，所以测试的时候有用
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
