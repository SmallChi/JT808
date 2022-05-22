using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.SuBiao.Enums
{
    /// <summary>
    /// 主动安全消息Id
    /// Active security message Id
    /// </summary>
    public enum JT808_SuBiao_MsgId : ushort
    {
        /// <summary>
        /// 报警附件信息消息
        /// Alarm attachment information message
        /// </summary>
        alarm_attachment_information_message = 0x1210,
        /// <summary>
        /// 文件信息上传
        /// Uploading File Information
        /// </summary>
        uploading_file_information = 0x1211,
        /// <summary>
        /// 文件上传完成消息
        /// Message indicating that file uploading is complete
        /// </summary>
        message_indicating_that_file_uploading_complete = 0x1212,
        /// <summary>
        /// 报警附件上传指令
        /// Alarm attachment upload instruction
        /// </summary>
        alarm_attachment_upload_instruction = 0x9208,
        /// <summary>
        /// 文件上传完成消息应答
        /// File upload complete reply message
        /// </summary>
        file_upload_complete_reply_message = 0x9212,
    }
}
