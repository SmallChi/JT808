using System;
using System.Collections.Generic;
using System.Text;
using JT808.Protocol.Extensions;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Attributes;
using System.Buffers;

namespace JT808.Protocol.JT808Formatters
{
    /// <summary>
    /// JT808包序列化器
    /// </summary>
    public class JT808PackageFromatter : IJT808Formatter<JT808Package>
    {
        public JT808Package Deserialize(ReadOnlySpan<byte> bytes,  out int readSize)
        {
            int offset = 0;
            JT808Package jT808Package = new JT808Package();
            // 转义还原——>验证校验码——>解析消息
            // 1. 解码（转义还原）
            ReadOnlySpan<byte> buffer = JT808DeEscape(bytes, 0, bytes.Length);
            // 2. 验证校验码
            //  2.1. 获取校验位索引
            int checkIndex = buffer.Length - 2;
            //  2.2. 获取校验码
            jT808Package.CheckCode = buffer[checkIndex];
            //  2.3. 从消息头到校验码前一个字节
            byte checkCode = buffer.ToXor(1, checkIndex);
            //  2.4. 验证校验码
            if (jT808Package.CheckCode != checkCode)
            {
                throw new JT808Exception($"{jT808Package.CheckCode}!={checkCode}");
            }
            jT808Package.Begin = buffer[offset];
            offset = offset + 1;
            // 3.初始化消息头
            try
            {
                jT808Package.Header = JT808FormatterExtensions.GetFormatter<JT808Header>().Deserialize(buffer.Slice(offset), out readSize);
            }
            catch (Exception ex)
            {
                throw new JT808Exception($"消息头解析错误,offset:{offset.ToString()}", ex);
            }
            offset = readSize;
            if (jT808Package.Header.MessageBodyProperty.DataLength != 0)
            {
                JT808BodiesTypeAttribute jT808BodiesTypeAttribute = jT808Package.Header.MsgId.GetAttribute<JT808BodiesTypeAttribute>();
                if (jT808BodiesTypeAttribute != null)
                {
                    if (jT808Package.Header.MessageBodyProperty.IsPackge)
                    {//4.分包消息体 从17位开始  或   未分包消息体 从13位开始
                     //消息总包数2位+包序号2位=4位
                        offset = offset + 2 + 2;
                    }
                    if (jT808Package.Header.MessageBodyProperty.DataLength != 0)
                    {
                        try
                        {
                            //5.处理消息体
                            jT808Package.Bodies = JT808FormatterResolverExtensions.JT808DynamicDeserialize(JT808FormatterExtensions.GetFormatter(jT808BodiesTypeAttribute.JT808BodiesType), buffer.Slice(offset+1, jT808Package.Header.MessageBodyProperty.DataLength),  out readSize);
                        }
                        catch (Exception ex)
                        {
                            throw new JT808Exception($"消息体解析错误,offset:{offset.ToString()}", ex);
                        }
                    }
                }
            }
            jT808Package.End = buffer[buffer.Length - 1];
            readSize = buffer.Length;
            return jT808Package;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808Package value)
        {
            // 1. 先判断是否分包（理论下发不需分包，但为了统一还是加上分包处理）
            // 2. 先序列化数据体，根据数据体的长度赋值给头部，在序列化头部。
            int messageBodyOffset = 0;
            if (value.Header.MessageBodyProperty.IsPackge)
            {   //3. 先写入分包消息总包数、包序号 
                messageBodyOffset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, messageBodyOffset, value.Header.MessageBodyProperty.PackgeCount);
                messageBodyOffset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, messageBodyOffset, value.Header.MessageBodyProperty.PackageIndex);
            }
            // 4. 数据体 
            //JT808.Protocol.Enums.JT808MsgId 映射对应消息特性
            JT808BodiesTypeAttribute jT808BodiesTypeAttribute = value.Header.MsgId.GetAttribute<JT808BodiesTypeAttribute>();
            if (jT808BodiesTypeAttribute != null)
            {
                if (value.Bodies != null)
                {
                    // 4.1 处理数据体
                    messageBodyOffset = JT808FormatterResolverExtensions.JT808DynamicSerialize(JT808FormatterExtensions.GetFormatter(jT808BodiesTypeAttribute.JT808BodiesType), memoryOwner, offset, value.Bodies);
                }
            }
            Memory<byte> messageBodyBytes = null;
            if (messageBodyOffset != 0)
            {
                messageBodyBytes = new Memory<byte>(new byte[messageBodyOffset]);
                memoryOwner.Memory.Slice(0, messageBodyOffset).CopyTo(messageBodyBytes);
            }
            // ------------------------------------开始组包
            // 1.起始符
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.Begin);
            // 2.赋值头数据长度
            value.Header.MessageBodyProperty.DataLength = messageBodyOffset;
            offset = JT808FormatterExtensions.GetFormatter<JT808Header>().Serialize(memoryOwner, offset, value.Header);
            if (messageBodyOffset != 0)
            {
                JT808BinaryExtensions.CopyTo(messageBodyBytes.Span, memoryOwner.Memory.Span, offset);
                offset += messageBodyOffset;
                messageBodyBytes = null;
            }
            // 4.校验码
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, memoryOwner.Memory.Span.ToXor(1, offset));
            // 5.终止符
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.End);
            byte[] temp = JT808Escape(memoryOwner.Memory.Slice(0, offset).Span);
            memoryOwner.Memory.Span.Clear();
            JT808BinaryExtensions.CopyTo(temp, memoryOwner.Memory.Span, 0);
            return temp.Length;
        }

        /// <summary>
        /// 转义
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static ReadOnlySpan<byte> JT808DeEscape(ReadOnlySpan<byte> buf, int offset, int length)
        {
            List<byte> bytes = new List<byte>();
            int n = 0;
            int i = offset;
            int len = offset + length;
            while (i < len)
            {
                if (buf[i] == 0x7d)
                {
                    if (len > i + 1)
                    {
                        if (buf[i + 1] == 0x01)
                        {
                            bytes.Add(0x7d);
                            i++;
                        }
                        else if (buf[i + 1] == 0x02)
                        {
                            bytes.Add(0x7e);
                            i++;
                        }
                        else
                        {
                            bytes.Add(buf[i]);
                        }
                    }
                }
                else
                {
                    bytes.Add(buf[i]);
                }
                n++;
                i++;
            }
            return bytes.ToArray();
        }

        /// <summary>
        /// 转码
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        private static byte[] JT808Escape(Span<byte> buf)
        {
            List<byte> bytes = new List<byte>();
            int n = 0;
            bytes.Add(buf[0]);
            for (int i = 1; i < buf.Length - 1; i++)
            {
                if (buf[i] == 0x7e)
                {
                    bytes.Add(0x7d);
                    bytes.Add(0x02);
                    n++;
                }
                else if (buf[i] == 0x7d)
                {
                    bytes.Add(0x7d);
                    bytes.Add(0x01);
                    n++;
                }
                else
                {
                    bytes.Add(buf[i]);
                }
            }
            if (n > 0)
            {
                bytes.Add(buf[buf.Length - 1]);
                return bytes.ToArray();
            }
            else
            {
                return buf.ToArray();
            }
        }
    }
}
