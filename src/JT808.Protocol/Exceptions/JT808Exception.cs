using JT808.Protocol.Enums;
using System;

namespace JT808.Protocol.Exceptions
{
    [Serializable]
    public class JT808Exception : Exception
    {
        public JT808Exception(JT808ErrorCode errorCode) : base(errorCode.ToString())
        {
            ErrorCode = errorCode;
        }

        public JT808Exception(JT808ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public JT808Exception(JT808ErrorCode errorCode, Exception ex) : base(ex.Message, ex)
        {
            ErrorCode = errorCode;
        }

        public JT808Exception(JT808ErrorCode errorCode, string message, Exception ex) : base(ex.Message, ex)
        {
            ErrorCode = errorCode;
        }

        public JT808ErrorCode ErrorCode { get; }
    }
}
