using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using System;
using System.Reflection;
using System.Text;
using static JT808.Protocol.MessageBody.JT808_0x8105;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 全局配置基类
    /// </summary>
    public abstract class GlobalConfigBase : IJT808Config
    {
        /// <summary>
        /// 
        /// </summary>
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
            JT808_0x0900_Custom_Factory = new JT808_0x0900_Custom_Factory();
            JT808_0x8900_Custom_Factory = new JT808_0x8900_Custom_Factory();
            JT808_0x8500_2019_Factory = new JT808_0x8500_2019_Factory();
            JT808_CarDVR_Up_Factory = new JT808_CarDVR_Up_Factory();
            JT808_CarDVR_Down_Factory = new JT808_CarDVR_Down_Factory();
            JT808_0x8105_Cusotm_Factory = new JT808_0x8105_Cusotm_Factory();
            TerminalPhoneNoLength = 12;
            Trim = true;
        }
        /// <summary>
        /// 配置Id
        /// </summary>
        public abstract string ConfigId { get; protected set; }
        /// <summary>
        /// 分布式消息自增流水号
        /// </summary>
        public virtual IJT808MsgSNDistributed MsgSNDistributed { get; set; }
        /// <summary>
        /// 压缩
        /// </summary>
        public virtual IJT808Compress Compress { get; set; }
        /// <summary>
        /// 808分包策略
        /// </summary>
        public virtual IJT808SplitPackageStrategy SplitPackageStrategy { get; set; }
        /// <summary>
        /// 808消息Id工厂
        /// </summary>
        public virtual IJT808MsgIdFactory MsgIdFactory { get; set; }
        /// <summary>
        /// GBK编码
        /// </summary>
        public virtual Encoding Encoding { get; set; }
        /// <summary>
        /// 跳过校验码验证
        /// 默认false
        /// </summary>
        public virtual bool SkipCRCCode { get; set; }
        /// <summary>
        /// 序列化器工厂
        /// </summary>
        public virtual IJT808FormatterFactory FormatterFactory { get; set; }
        /// <summary>
        /// 0x0200自定义附加信息工厂
        /// </summary>
        public virtual IJT808_0x0200_Custom_Factory JT808_0X0200_Custom_Factory { get; set; }
        /// <summary>
        /// 0x0200附加信息工厂
        /// </summary>
        public virtual IJT808_0x0200_Factory JT808_0X0200_Factory { get; set; }
        /// <summary>
        /// 0x8103自定义终端参数设置自定义消息工厂
        /// </summary>
        public virtual IJT808_0x8103_Custom_Factory JT808_0X8103_Custom_Factory { get; set; }
        /// <summary>
        /// 0x8103终端参数设置消息工厂
        /// </summary>
        public virtual IJT808_0x8103_Factory JT808_0X8103_Factory { get; set; }
        /// <summary>
        /// 终端SIM卡长度
        /// </summary>
        public virtual int TerminalPhoneNoLength { get; set; }
        /// <summary>
        /// 是否去掉头尾空格
        /// </summary>
        public virtual bool Trim { get; set; }
        /// <summary>
        /// 自定义数据上行透传消息工厂
        /// </summary>
        public virtual IJT808_0x0900_Custom_Factory JT808_0x0900_Custom_Factory { get; set; }
        /// <summary>
        /// 自定义数据下行透传消息工厂
        /// </summary>
        public virtual IJT808_0x8900_Custom_Factory JT808_0x8900_Custom_Factory { get; set; }
        /// <summary>
        /// 车辆控制消息工厂
        /// </summary>
        public virtual IJT808_0x8500_2019_Factory JT808_0x8500_2019_Factory { get; set; }
        /// <summary>
        /// JT19056上行消息工厂
        /// </summary>
        public IJT808_CarDVR_Up_Factory JT808_CarDVR_Up_Factory { get; set; }
        /// <summary>
        /// JT19056下行消息工厂
        /// </summary>
        public IJT808_CarDVR_Down_Factory JT808_CarDVR_Down_Factory { get; set; }
        /// <summary>
        /// 跳过校验码验证
        /// 默认false
        /// </summary>
        public bool SkipCarDVRCRCCode { get; set; }
        /// <summary>
        /// 终端控制自定义参数命令工厂
        /// </summary>
        public virtual IJT808_0x8105_Cusotm_Factory JT808_0x8105_Cusotm_Factory { get; set; }
        /// <summary>
        /// 外部扩展程序集注册
        /// </summary>
        /// <param name="externalAssemblies"></param>
        /// <returns></returns>
        public virtual IJT808Config Register(params Assembly[] externalAssemblies)
        {
            if (externalAssemblies != null)
            {
                foreach (var easb in externalAssemblies)
                {
                    MsgIdFactory.Register(easb);
                    FormatterFactory.Register(easb);
                    JT808_0X0200_Factory.Register(easb);
                    JT808_0X0200_Custom_Factory.Register(easb);
                    JT808_0X8103_Factory.Register(easb);
                    JT808_0X8103_Custom_Factory.Register(easb);
                    JT808_0x0900_Custom_Factory.Register(easb);
                    JT808_0x8900_Custom_Factory.Register(easb);
                    JT808_0x8500_2019_Factory.Register(easb);
                    JT808_CarDVR_Up_Factory.Register(easb);
                    JT808_CarDVR_Down_Factory.Register(easb);
                    JT808_0x8105_Cusotm_Factory.Register(easb);
                }
            }
            return this;
        }
        /// <summary>
        /// 替换原有消息
        /// </summary>
        /// <typeparam name="TSourceJT808Bodies"></typeparam>
        /// <typeparam name="TTargetJT808Bodies"></typeparam>
        public void ReplaceMsgId<TSourceJT808Bodies, TTargetJT808Bodies>()
            where TSourceJT808Bodies : JT808Bodies
            where TTargetJT808Bodies : JT808Bodies, new()
        {
            TTargetJT808Bodies bodies = new TTargetJT808Bodies();
            MsgIdFactory.Map[bodies.MsgId] = bodies;
            FormatterFactory.FormatterDict.Remove(typeof(TSourceJT808Bodies).GUID);
            FormatterFactory.FormatterDict.Add(typeof(TTargetJT808Bodies).GUID, (IJT808MessagePackFormatter)bodies);
        }
    }
}
