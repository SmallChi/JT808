using System;

namespace JT808.Protocol.Exceptions
{
    [Serializable]
    public class JT808Exception:Exception
    {
        public JT808Exception()
        {
  
        }

        public JT808Exception(string message, Exception exception) : base(message, exception)
        {

        }

        public JT808Exception(string message) : base(message)
        {

        }
    }
}
