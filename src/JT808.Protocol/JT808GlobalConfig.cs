using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("JT808.Protocol.Benchmark")]
[assembly: InternalsVisibleTo("JT808.Protocol.Test")]
namespace JT808.Protocol
{
    public class JT808GlobalConfig
    {
        public static readonly JT808GlobalConfig Instance = new JT808GlobalConfig();

        public JT808GlobalConfig()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MsgSNDistributed = new DefaultMsgSNDistributedImpl();
            Compress = new JT808GZipCompressImpl();
            SplitPackageStrategy = new DefaultSplitPackageStrategyImpl();
            SkipCRCCode = false;
            MsgIdFactory = new JT808MsgIdFactory();
            Encoding = Encoding.GetEncoding("GBK");
            FormatterFactory = new JT808FormatterFactory();
            JT808_0X0200_Custom_Factory = new JT808_0x0200_Custom_Factory();
            JT808_0X8103_Custom_Factory = new JT808_0x8103_Custom_Factory();
        }

        public IJT808MsgSNDistributed MsgSNDistributed { get; private set; }

        public IJT808Compress Compress { get; private set; }

        public IJT808SplitPackageStrategy SplitPackageStrategy { get; private set; }

        public IJT808MsgIdFactory MsgIdFactory { get; private set; }

        public Encoding Encoding;

        /// <summary>
        /// 序列化器工厂
        /// </summary>
        internal IJT808FormatterFactory FormatterFactory { get; }

        /// <summary>
        /// 自定义附加信息工厂
        /// </summary>
        internal IJT808_0x0200_Custom_Factory  JT808_0X0200_Custom_Factory { get; }

        /// <summary>
        ///自定义设置终端参数工厂
        /// </summary>
        internal IJT808_0x8103_Custom_Factory JT808_0X8103_Custom_Factory { get; }

        /// <summary>
        /// 跳过校验码
        /// 测试的时候需要手动修改值，避免验证
        /// 默认：false
        /// </summary>
        public bool SkipCRCCode { get; private set; }


        /// <summary>
        /// 设置消息序列号
        /// </summary>
        /// <param name="msgSNDistributed"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetMsgSNDistributed(IJT808MsgSNDistributed msgSNDistributed)
        {
            Instance.MsgSNDistributed = msgSNDistributed;
            return this;
        }

        /// <summary>
        /// 设置压缩算法
        /// 默认GZip
        /// </summary>
        /// <param name="compressImpl"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetCompress(IJT808Compress compressImpl)
        {
            Instance.Compress = compressImpl;
            return this;
        }
        /// <summary>
        /// 设置分包算法
        /// 默认3*256
        /// </summary>
        /// <param name="splitPackageStrategy"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetSplitPackageStrategy(IJT808SplitPackageStrategy splitPackageStrategy)
        {
            Instance.SplitPackageStrategy = splitPackageStrategy;
            return this;
        }
        /// <summary>
        /// 设置跳过校验码
        /// 场景：测试的时候，可能需要手动改数据，所以测试的时候有用
        /// </summary>
        /// <param name="skipCRCCode"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetSkipCRCCode(bool skipCRCCode)
        {
            Instance.SkipCRCCode = skipCRCCode;
            return this;
        }
        /// <summary>
        /// 设置消息工厂的实现
        /// </summary>
        /// <param name="msgIdFactory"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetMsgIdFactory(IJT808MsgIdFactory  msgIdFactory)
        {
            if (msgIdFactory != null)
            {
                Instance.MsgIdFactory = msgIdFactory;
            }
            return this;
        }
        /// <summary>
        /// 全局注册外部程序集
        /// </summary>
        /// <param name="externalAssemblies"></param>
        /// <returns></returns>
        public JT808GlobalConfig Register(params Assembly[] externalAssemblies)
        {
            if (externalAssemblies != null)
            {
                foreach(var easb in externalAssemblies)
                {
                    FormatterFactory.Register(easb);
                    JT808_0X0200_Custom_Factory.Register(easb);
                    JT808_0X8103_Custom_Factory.Register(easb);
                }
            }
            return this;
        }
    }
}
