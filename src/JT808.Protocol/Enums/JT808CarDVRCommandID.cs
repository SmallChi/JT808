using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 行车记录仪命令字
    /// Dashcam command words
    /// </summary>
    public enum JT808CarDVRCommandID:byte
    {
        /// <summary>
        /// 采集记录仪执行标准版本
        /// The collect recorder performs the standard version
        /// </summary>
        collect_recorder_performs_standard_version = 0x00,
        /// <summary>
        /// 采集当前驾驶人信息
        /// Collect current driver information
        /// </summary>
        collect_driver = 0x01,
        /// <summary>
        /// 采集记录仪实时时间
        /// Collect of real time recorder
        /// </summary>
        collect_realtime = 0x02,
        /// <summary>
        /// 采集累计行驶里程
        /// Collect accumulated mileage
        /// </summary>
        collect_accumulated_mileage = 0x03,
        /// <summary>
        /// 采集记录仪脉冲系数
        /// Pulse coefficient of collect recorder
        /// </summary>
        collect_recorder_pulse_coefficient = 0x04,
        /// <summary>
        /// 采集车辆信息
        /// Collection of vehicle information
        /// </summary>
        collect_vehicle_information = 0x05,
        /// <summary>
        /// 采集记录仪状态信号配置信息
        /// Collect the configuration information of recorder status signal
        /// </summary>
        collect_recorder_status_signal_configuration_information = 0x06,
        /// <summary>
        /// 采集记录仪唯一性编号
        /// Collection recorder unique number
        /// </summary>
        collect_recorder_unique_number = 0x07,
        /// <summary>
        /// 采集指定的行驶速度记录
        /// Collect the specified speed record
        /// </summary>
        collect_recorder_specified_speed = 0x08,
        /// <summary>
        /// 采集指定的位置信息记录
        /// Collect the specified location information record
        /// </summary>
        collect_specified_location_information = 0x09,
        /// <summary>
        /// 采集指定的事故疑点记录
        /// Collect specified incident suspect records
        /// </summary>
        collect_specified_incident_suspect_records = 0x10,
        /// <summary>
        /// 采集指定的超时驾驶记录
        /// Collect specified driving overtime records
        /// </summary>
        collect_specified_driving_overtime_records = 0x11,
        /// <summary>
        /// 采集指定的驾驶人身份记录
        /// Collect identification records of designated drivers
        /// </summary>
        collect_drivers_identification_records = 0x12,
        /// <summary>
        /// 采集指定的外部供电记录
        /// Collect the specified external power supply records
        /// </summary>
        collect_specified_external_power_supply_records = 0x13,
        /// <summary>
        /// 采集指定的参数修改记录
        /// Collect the modification records of specified parameters
        /// </summary>
        collect_specified_modify_parameters_records = 0x14,
        /// <summary>
        /// 采集指定的速度状态日志
        /// Collects the specified speed status logs
        /// </summary>
        collect_specified_speed_status_logs = 0x15,
        /// <summary>
        /// 设置车辆信息
        /// Setting vehicle Information
        /// </summary>
        setting_vehicle_information = 0x82,
        /// <summary>
        /// 设置记录仪初次安装日期
        /// Set the first installation date of recorder
        /// </summary>
        set_first_install_date_recorder = 0x83,
        /// <summary>
        /// 设置状态量配置信息
        /// Set state quantity configuration information
        /// </summary>
        set_state_quantity_configuration_information = 0x84,
        /// <summary>
        /// 设置记录仪时间
        /// Setting recorder time
        /// </summary>
        set_recorder_time = 0xC2,
        /// <summary>
        /// 设置记录仪脉冲系数
        /// Set the pulse coefficient of recorder
        /// </summary>
        set_pulse_coefficient_recorder = 0xC3,
        /// <summary>
        /// 设置初始里程
        /// Setting the initial mileage
        /// </summary>
        set_init_mileage = 0xC4,
        /// <summary>
        /// 进入或保持检定状态
        /// Enters or maintains the check state
        /// </summary>
        enters_maintains_check_state = 0xE0,
        /// <summary>
        /// 进入里程误差测量
        /// Enter the mileage error measurement
        /// </summary>
        enter_mileage_error_measurement = 0xE1,
        /// <summary>
        /// 进入脉冲系数误差测量
        /// Enter the pulse coefficient error measurement
        /// </summary>
        enter_pulse_coefficient_error_measurement = 0xE2,
        /// <summary>
        /// 进入实时时间误差测量
        /// Enter real-time time error measurement
        /// </summary>
        enter_realtime_time_error_measurement = 0xE3,
        /// <summary>
        /// 返回正常工作状态
        /// Return to normal working status
        /// </summary>
        return_normal_working_status = 0xE4,
    }
}
