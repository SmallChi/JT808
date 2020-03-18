using JT808.Protocol.MessageBody.Recorder;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 记录仪工厂
    /// </summary>
   public  interface IJT808_Recorder_Factory : IJT808ExternalRegister
    {
        IDictionary<byte, JT808_RecorderBody> Map { get; }
        IJT808_Recorder_Factory SetMap<TJT808_RecorderBody>() ;
    }
}
