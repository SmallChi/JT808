using JT808.Protocol.Enums;
using System;

namespace JT808.Protocol.Exceptions
{
    /// <summary>
    /// JT808异常处理类
    /// Exception handling class
    /// </summary>
    [Serializable]
    public class JT808Exception : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        public JT808Exception(JT808ErrorCode errorCode) : base(errorCode.ToString())
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        public JT808Exception(JT808ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="ex"></param>
        public JT808Exception(JT808ErrorCode errorCode, Exception ex) : base(ex.Message, ex)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public JT808Exception(JT808ErrorCode errorCode, string message, Exception ex) : base(message, ex)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// JT808统一错误码
        /// Unified error code
        /// </summary>
        public JT808ErrorCode ErrorCode { get; }
    }
}
