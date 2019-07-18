using JT808.Protocol.Formatters;
using JT808.Protocol.Internal;
using System;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    public abstract class GlobalConfigBase : IJT808Config
    {
        protected GlobalConfigBase()
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
            JT808_0X0200_Factory = new JT808_0x0200_Factory();
            JT808_0X8103_Custom_Factory = new JT808_0x8103_Custom_Factory();
            JT808_0X8103_Factory = new JT808_0x8103_Factory();
            TerminalPhoneNoLength = 12;
        }
        public abstract string ConfigId { get; }
        public virtual IJT808MsgSNDistributed MsgSNDistributed { get; set; }
        public virtual IJT808Compress Compress { get; set; }
        public virtual IJT808SplitPackageStrategy SplitPackageStrategy { get; set; }
        public virtual IJT808MsgIdFactory MsgIdFactory { get; set; }
        public virtual Encoding Encoding { get; set; }
        public virtual bool SkipCRCCode { get; set; }
        public virtual IJT808FormatterFactory FormatterFactory { get; set; }
        public virtual IJT808_0x0200_Custom_Factory JT808_0X0200_Custom_Factory { get; set; }
        public virtual IJT808_0x0200_Factory JT808_0X0200_Factory { get; set; }
        public virtual IJT808_0x8103_Custom_Factory JT808_0X8103_Custom_Factory { get; set; }
        public virtual IJT808_0x8103_Factory JT808_0X8103_Factory { get; set; }
        public virtual int TerminalPhoneNoLength { get; set; }
        public virtual IJT808Config Register(params Assembly[] externalAssemblies)
        {
            if (externalAssemblies != null)
            {
                foreach (var easb in externalAssemblies)
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
