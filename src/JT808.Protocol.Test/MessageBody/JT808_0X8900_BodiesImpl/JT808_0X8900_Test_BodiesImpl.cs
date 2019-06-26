using JT808.Protocol.Attributes;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Test.MessageBody.JT808Formatters;

namespace JT808.Protocol.Test.MessageBody.JT808_0X8900_BodiesImpl
{
    [JT808Formatter(typeof(JT808_0X8900_Test_BodiesImplFormatter))]
    public class JT808_0X8900_Test_BodiesImpl: JT808_0x8900_BodyBase
    {
         public uint Id { get; set; }

         public byte Sex { get; set; }
    }
}
