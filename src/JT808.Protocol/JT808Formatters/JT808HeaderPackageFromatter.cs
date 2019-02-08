using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.JT808Internal;
using System;

namespace JT808.Protocol.JT808Formatters
{
    public class JT808HeaderPackageFromatter : IJT808Formatter<JT808HeaderPackage>
    {
        public JT808HeaderPackage Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808HeaderPackage jT808HeaderPackage = new JT808HeaderPackage();
            // 转义还原——>验证校验码——>解析消息
            // 1. 解码（转义还原）
            ReadOnlySpan<byte> buffer = JT808Utils.JT808DeEscape(bytes);
            // 2. 验证校验码
            //  2.1. 获取校验位索引
            int checkIndex = buffer.Length - 2;
            //  2.2. 获取校验码
            int checkCode1 = buffer[checkIndex];
            //  2.3. 从消息头到校验码前一个字节
            byte checkCode2 = buffer.ToXor(1, checkIndex);
            //  2.4. 验证校验码
            if (!JT808GlobalConfig.Instance.SkipCRCCode)
            {
                if (checkCode1 != checkCode2)
                {
                    throw new JT808Exception(JT808ErrorCode.CheckCodeNotEqual, $"{checkCode1}!={checkCode2}");
                }
            }
            offset = offset + 1;
            // 3.初始化消息头
            try
            {
                jT808HeaderPackage.Header = JT808FormatterExtensions.GetFormatter<JT808Header>().Deserialize(buffer.Slice(offset, 13), out readSize);
            }
            catch (Exception ex)
            {
                throw new JT808Exception(JT808ErrorCode.HeaderParseError, ex);
            }
            offset = readSize;
            if (jT808HeaderPackage.Header.MessageBodyProperty.DataLength != 0)
            {
                Type jT808BodiesImplType = JT808MsgIdFactory.GetBodiesImplTypeByMsgId(jT808HeaderPackage.Header.MsgId);
                if (jT808BodiesImplType != null)
                {
                    if (jT808HeaderPackage.Header.MessageBodyProperty.IsPackge)
                    {//4.分包消息体 从17位开始  或   未分包消息体 从13位开始
                     //消息总包数2位+包序号2位=4位
                        offset = offset + 2 + 2;
                    }
                    if (jT808HeaderPackage.Header.MessageBodyProperty.DataLength != 0)
                    {
                        try
                        {
                            //5.处理消息体
                            jT808HeaderPackage.Bodies = buffer.Slice(offset + 1, jT808HeaderPackage.Header.MessageBodyProperty.DataLength).ToArray();
                        }
                        catch (Exception ex)
                        {
                            throw new JT808Exception(JT808ErrorCode.BodiesParseError, ex);
                        }
                    }
                }
            }
            offset = readSize;
            readSize = buffer.Length;
            return jT808HeaderPackage;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808HeaderPackage value)
        {
            throw new NotImplementedException("只适用反序列化");
        }
    }
}
