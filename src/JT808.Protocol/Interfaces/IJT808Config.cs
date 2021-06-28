using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static JT808.Protocol.MessageBody.JT808_0x8105;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808接口配置
    /// </summary>
    public interface IJT808Config
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        string ConfigId { get; }
        /// <summary>
        /// 消息流水号
        /// </summary>
        IJT808MsgSNDistributed MsgSNDistributed { get; set; }
        /// <summary>
        /// 消息工厂
        /// </summary>
        IJT808MsgIdFactory MsgIdFactory { get; set; }
        /// <summary>
        /// 压缩接口
        /// </summary>
        IJT808Compress Compress { get; set; }
        /// <summary>
        /// 分包策略
        /// 注意:处理808的分包读取完流需要先进行转义在进行分包
        /// </summary>
        IJT808SplitPackageStrategy SplitPackageStrategy { get; set; }
        /// <summary>
        /// 序列化器工厂
        /// </summary>
        IJT808FormatterFactory FormatterFactory { get; set; }
        /// <summary>
        /// 自定义附加信息工厂
        /// </summary>
        IJT808_0x0200_Custom_Factory JT808_0X0200_Custom_Factory { get; set; }
        /// <summary>
        /// 附加信息工厂
        /// </summary>
        IJT808_0x0200_Factory JT808_0X0200_Factory { get; set; }
        /// <summary>
        ///自定义设置终端参数工厂
        /// </summary>
        IJT808_0x8103_Custom_Factory JT808_0X8103_Custom_Factory { get; set; }
        /// <summary>
        ///设置终端参数工厂
        /// </summary>
        IJT808_0x8103_Factory JT808_0X8103_Factory { get; set; }
        /// <summary>
        ///数据上行透传工厂
        /// </summary>
        IJT808_0x0900_Custom_Factory JT808_0x0900_Custom_Factory { get; set; }
        /// <summary>
        ///数据下行透传工厂
        /// </summary>
        IJT808_0x8900_Custom_Factory JT808_0x8900_Custom_Factory { get; set; }
        /// <summary>
        /// 控制类型工厂
        /// </summary>
        IJT808_0x8500_2019_Factory JT808_0x8500_2019_Factory { get; set; }
        /// <summary>
        /// 终端控制自定义参数命令工厂
        /// </summary>
        IJT808_0x8105_Cusotm_Factory JT808_0x8105_Cusotm_Factory { get; set; }
        /// <summary>
        /// 记录仪上行命令字工厂
        /// </summary>
        IJT808_CarDVR_Up_Factory JT808_CarDVR_Up_Factory { get; set; }
        /// <summary>
        /// 记录仪下行命令字工厂
        /// </summary>
        IJT808_CarDVR_Down_Factory JT808_CarDVR_Down_Factory { get; set; }
        /// <summary>
        /// 统一编码
        /// </summary>
        Encoding Encoding { get; set; }
        /// <summary>
        /// 跳过校验码
        /// 测试的时候需要手动修改值，避免验证
        /// 默认：false
        /// </summary>
        bool SkipCRCCode { get; set; }
        /// <summary>
        /// 跳过行车记录仪校验码
        /// 测试的时候需要手动修改值，避免验证
        /// 默认：false
        /// </summary>
        bool SkipCarDVRCRCCode { get; set; }
        /// <summary>
        /// ReadBCD是否需要去0操作
        /// 默认是去0
        /// 注意:有时候对协议来说是有意义的0
        /// </summary>
        bool Trim { get; set; }
        /// <summary>
        /// 设备终端号(默认12位)
        /// </summary>
        int TerminalPhoneNoLength { get; set; }
        /// <summary>
        /// 全局注册外部程序集
        /// </summary>
        /// <param name="externalAssemblies"></param>
        /// <returns></returns>
        IJT808Config Register(params Assembly[] externalAssemblies);
        /// <summary>
        /// 替换原有消息
        /// </summary>
        void ReplaceMsgId<TSourceJT808Bodies, TTargetJT808Bodies>()
            where TSourceJT808Bodies : JT808Bodies
            where TTargetJT808Bodies : JT808Bodies,new();

    }
}
