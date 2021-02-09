using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 行车记录仪命令字
    /// </summary>
    public enum JT808CarDVRCommandID:byte
    {
        /// <summary>
        /// 采集记录仪执行标准版本
        /// </summary>
        采集记录仪执行标准版本 = 0x00,
        /// <summary>
        /// 采集当前驾驶人信息
        /// </summary>
        采集当前驾驶人信息 = 0x01,
        /// <summary>
        /// 采集记录仪实时时间
        /// </summary>
        采集记录仪实时时间 = 0x02,
        /// <summary>
        /// 采集累计行驶里程
        /// </summary>
        采集累计行驶里程 = 0x03,
        /// <summary>
        /// 采集记录仪脉冲系数
        /// </summary>
        采集记录仪脉冲系数 = 0x04,
        /// <summary>
        /// 采集车辆信息
        /// </summary>
        采集车辆信息 = 0x05,
        /// <summary>
        /// 采集记录仪状态信号配置信息
        /// </summary>
        采集记录仪状态信号配置信息 = 0x06,
        /// <summary>
        /// 采集记录仪唯一性编号
        /// </summary>
        采集记录仪唯一性编号 = 0x07,
        /// <summary>
        /// 采集指定的行驶速度记录
        /// </summary>
        采集指定的行驶速度记录 = 0x08,
        /// <summary>
        /// 采集指定的位置信息记录
        /// </summary>
        采集指定的位置信息记录 = 0x09,
        /// <summary>
        /// 采集指定的事故疑点记录
        /// </summary>
        采集指定的事故疑点记录 = 0x10,
        /// <summary>
        /// 采集指定的超时驾驶记录
        /// </summary>
        采集指定的超时驾驶记录 = 0x11,
        /// <summary>
        /// 采集指定的驾驶人身份记录
        /// </summary>
        采集指定的驾驶人身份记录 = 0x12,
        /// <summary>
        /// 采集指定的外部供电记录
        /// </summary>
        采集指定的外部供电记录 = 0x13,
        /// <summary>
        /// 采集指定的参数修改记录
        /// </summary>
        采集指定的参数修改记录 = 0x14,
        /// <summary>
        /// 采集指定的速度状态日志
        /// </summary>
        采集指定的速度状态日志 = 0x15,
        /// <summary>
        /// 设置车辆信息
        /// </summary>
        设置车辆信息 = 0x82,
        /// <summary>
        /// 设置记录仪初次安装日期
        /// </summary>
        设置记录仪初次安装日期 = 0x83,
        /// <summary>
        /// 设置状态量配置信息
        /// </summary>
        设置状态量配置信息 = 0x84,
        /// <summary>
        /// 设置记录仪时间
        /// </summary>
        设置记录仪时间 = 0xC2,
        /// <summary>
        /// 设置记录仪脉冲系数
        /// </summary>
        设置记录仪脉冲系数 = 0xC3,
        /// <summary>
        /// 设置初始里程
        /// </summary>
        设置初始里程 = 0xC4,
        /// <summary>
        /// 进入或保持检定状态
        /// </summary>
        进入或保持检定状态 = 0xE0,
        /// <summary>
        /// 进入里程误差测量
        /// </summary>
        进入里程误差测量 = 0xE1,
        /// <summary>
        /// 进入脉冲系数误差测量
        /// </summary>
        进入脉冲系数误差测量 = 0xE2,
        /// <summary>
        /// 进入实时时间误差测量
        /// </summary>
        进入实时时间误差测量 = 0xE3,
        /// <summary>
        /// 返回正常工作状态
        /// </summary>
        返回正常工作状态 = 0xE4,
    }
}
