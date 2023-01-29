using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 文件上传指令
    /// </summary>
    public class JT808_0x9206 : JT808MessagePackFormatter<JT808_0x9206>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 文件上传指令
        /// </summary>
        public string Description => "文件上传指令";
        /// <summary>
        /// 0x9206
        /// </summary>
        public ushort MsgId => 0x9206;
        /// <summary>
        /// 服务器IP地址服务
        /// </summary>
        public byte ServerIpLength { get; set; }
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string ServerIp { get; set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public ushort Port { get; set; }
        /// <summary>
        /// 用户名长度
        /// </summary>
        public byte UserNameLength { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码长度
        /// </summary>
        public byte PasswordLength { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 文件上传路径长度
        /// </summary>
        public byte FileUploadPathLength { get;  set; }
        /// <summary>
        /// 文件上传路径
        /// </summary>
        public string FileUploadPath { get; set; }
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelNo { get; set; }
        /// <summary>
        /// 起始时间 BCD[6]
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间 BCD[6]
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 报警标志
        /// </summary>
        public UInt64 AlarmFlag { get; set; }
        /// <summary>
        /// 音视频资源类型
        /// </summary>
        public byte MediaType { get; set; }
        /// <summary>
        /// 码流类型
        /// </summary>
        public byte StreamType { get; set; }
        /// <summary>
        /// 存储位置
        /// </summary>
        public byte MemoryPositon { get; set; }
        /// <summary>
        /// 任务执行条件
        /// </summary>
        public byte TaskExcuteCondition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9206 value = new JT808_0x9206();
            value.ServerIpLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ServerIpLength.ReadNumber()}]服务器IP地址长度", value.ServerIpLength);
            string ipHex = reader.ReadVirtualArray(value.ServerIpLength).ToArray().ToHexString();
            value.ServerIp = reader.ReadString(value.ServerIpLength);
            writer.WriteString($"[{ipHex}]服务器IP地址", value.ServerIp);
            value.Port = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Port.ReadNumber()}]服务器端口", value.Port);
            value.UserNameLength = reader.ReadByte();
            writer.WriteNumber($"[{value.UserNameLength.ReadNumber()}]用户名长度", value.UserNameLength);
            string userNameHex = reader.ReadVirtualArray(value.UserNameLength).ToArray().ToHexString();
            value.UserName = reader.ReadString(value.UserNameLength);
            writer.WriteString($"[{userNameHex}]用户名", value.UserName);
            value.PasswordLength = reader.ReadByte();
            writer.WriteNumber($"[{value.PasswordLength.ReadNumber()}]密码长度", value.PasswordLength);
            string passwordHex = reader.ReadVirtualArray(value.PasswordLength).ToArray().ToHexString();
            value.Password = reader.ReadString(value.PasswordLength);
            writer.WriteString($"[{passwordHex}]密码", value.Password);
            value.FileUploadPathLength = reader.ReadByte();
            writer.WriteNumber($"[{value.FileUploadPathLength.ReadNumber()}]文件上传路径长度", value.FileUploadPathLength);
            string fileUploadPathHex = reader.ReadVirtualArray(value.FileUploadPathLength).ToArray().ToHexString();
            value.FileUploadPath = reader.ReadString(value.FileUploadPathLength);
            writer.WriteString($"[{fileUploadPathHex}]文件上传路径", value.FileUploadPath);

            value.ChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.ChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.ChannelNo));
            value.BeginTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.BeginTime.ToString("yyMMddHHmmss")}]起始时间", value.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.EndTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.EndTime.ToString("yyMMddHHmmss")}]起始时间", value.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.AlarmFlag = reader.ReadUInt64();
            writer.WriteNumber($"[{value.AlarmFlag.ReadNumber()}]报警标志", value.AlarmFlag);
            value.MediaType = reader.ReadByte();
            writer.WriteString($"[{value.MediaType.ReadNumber()}]音视频类型", AVResourceTypeDisplay(value.MediaType));
            value.StreamType = reader.ReadByte();
            writer.WriteString($"[{value.StreamType.ReadNumber()}]码流类型", StreamTypeDisplay(value.StreamType));
            value.MemoryPositon = reader.ReadByte();
            writer.WriteString($"[{value.MemoryPositon.ReadNumber()}]存储器类型", MemoryPositonDisplay(value.MemoryPositon));
            value.TaskExcuteCondition = reader.ReadByte();
            writer.WriteString($"[{value.TaskExcuteCondition.ReadNumber()}]任务执行条件", TaskExcuteConditionDisplay(value.TaskExcuteCondition));

            static string AVResourceTypeDisplay(byte AVResourceType)
            {
                return AVResourceType switch
                {
                    0 => "音视频",
                    1 => "音频",
                    2 => "视频",
                    3 => "音频或视频",
                    _ => "未知",
                };
            }
            static string StreamTypeDisplay(byte StreamType)
            {
                return StreamType switch
                {
                    0 => "所有码流",
                    1 => "主码流",
                    2 => "子码流",
                    _ => "未知",
                };
            }
            static string MemoryPositonDisplay(byte MemoryPositon)
            {
                return MemoryPositon switch
                {
                    0 => "主存储器或灾备服务器",
                    1 => "主存储器",
                    2 => "灾备服务器",
                    _ => "未知",
                };
            }
            static string LogicalChannelNoDisplay(byte LogicalChannelNo)
            {
                return LogicalChannelNo switch
                {
                    1 => "驾驶员",
                    2 => "车辆正前方",
                    3 => "车前门",
                    4 => "车厢前部",
                    5 => "车厢后部",
                    7 => "行李舱",
                    8 => "车辆左侧",
                    9 => "车辆右侧",
                    10 => "车辆正后方",
                    11 => "车厢中部",
                    12 => "车中门",
                    13 => "驾驶席车门",
                    33 => "驾驶员",
                    36 => "车厢前部",
                    37 => "车厢后部",
                    _ => "预留",
                };
            }
            static string TaskExcuteConditionDisplay(byte TaskExcuteCondition) {
                var taskExcuteConditionDisplay = string.Empty;
                taskExcuteConditionDisplay += (TaskExcuteCondition & 0x01) == 1 ? ",WIFI":"";
                taskExcuteConditionDisplay += (TaskExcuteCondition & 0x01) == 1 ? ",LAN" : "";
                taskExcuteConditionDisplay += (TaskExcuteCondition & 0x01) == 1 ? ",3G/4G" : "";
                return taskExcuteConditionDisplay.Length > 0 ? taskExcuteConditionDisplay.Substring(1) : "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x9206 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            var jT808_0x9206 = new JT808_0x9206();
            jT808_0x9206.ServerIpLength = reader.ReadByte();
            jT808_0x9206.ServerIp = reader.ReadString(jT808_0x9206.ServerIpLength);
            jT808_0x9206.Port = reader.ReadUInt16();
            jT808_0x9206.UserNameLength = reader.ReadByte();
            jT808_0x9206.UserName = reader.ReadString(jT808_0x9206.UserNameLength);
            jT808_0x9206.PasswordLength = reader.ReadByte();
            jT808_0x9206.Password = reader.ReadString(jT808_0x9206.PasswordLength);
            jT808_0x9206.FileUploadPathLength = reader.ReadByte();
            jT808_0x9206.FileUploadPath = reader.ReadString(jT808_0x9206.FileUploadPathLength);
            jT808_0x9206.ChannelNo = reader.ReadByte();
            jT808_0x9206.BeginTime = reader.ReadDateTime_yyMMddHHmmss();
            jT808_0x9206.EndTime = reader.ReadDateTime_yyMMddHHmmss();
            jT808_0x9206.AlarmFlag = reader.ReadUInt64();
            jT808_0x9206.MediaType = reader.ReadByte();
            jT808_0x9206.StreamType = reader.ReadByte();
            jT808_0x9206.MemoryPositon = reader.ReadByte();
            jT808_0x9206.TaskExcuteCondition = reader.ReadByte();
            return jT808_0x9206;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x9206 value, IJT808Config config)
        {
            writer.Skip(1, out int serverIpLengthposition);
            writer.WriteString(value.ServerIp);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - serverIpLengthposition - 1), serverIpLengthposition);
            writer.WriteUInt16(value.Port);
            writer.Skip(1, out int userNameLengthposition);
            writer.WriteString(value.UserName);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - userNameLengthposition - 1), userNameLengthposition);
            writer.Skip(1, out int passwordLengthLengthposition);
            writer.WriteString(value.Password);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - passwordLengthLengthposition - 1), passwordLengthLengthposition);
            writer.Skip(1, out int fileUploadPathLengthLengthposition);
            writer.WriteString(value.FileUploadPath);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - fileUploadPathLengthLengthposition - 1), fileUploadPathLengthLengthposition);
            writer.WriteByte(value.ChannelNo);
            writer.WriteDateTime_yyMMddHHmmss(value.BeginTime);
            writer.WriteDateTime_yyMMddHHmmss(value.EndTime);
            writer.WriteUInt64(value.AlarmFlag);
            writer.WriteByte(value.MediaType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemoryPositon);
            writer.WriteByte(value.TaskExcuteCondition);
        }
    }
}
