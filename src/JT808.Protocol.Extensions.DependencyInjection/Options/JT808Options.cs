using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.DependencyInjection.Options
{
    public class JT808Options:IOptions<JT808Options>
    {
        public JT808Options()
        {
            SkipCRCCode = false;
            JT808LocationAttachIds = new List<byte>();
            JT808_0x8900Method = new Dictionary<byte, Type>();
            JT808_0x8103Method = new Dictionary<uint, Type>();
        }

        /// <summary>
        /// 设置跳过校验码
        /// 场景：测试的时候，可能需要收到改数据，所以测试的时候有用
        /// </summary>
        public bool SkipCRCCode { get; set; }
        /// <summary>
        /// 注册自定义定位信息附加数据
        /// <see cref="typeof(JT808.Protocol.MessageBody.JT808_0x0900_BodyBase)"/>
        /// <see cref="typeof(实现JT808_0x0900_BodyBase)"/>
        /// </summary>
        public List<byte> JT808LocationAttachIds { get; set; }
        /// <summary>
        /// 注册自定义数据下行透传信息
        /// <see cref="typeof(JT808.Protocol.MessageBody.JT808_0x8900_BodyBase)"/>
        /// <see cref="typeof(实现JT808_0x8900_BodyBase)"/>
        /// </summary>
        public Dictionary<byte, Type> JT808_0x8900Method { get; set; }
        /// <summary>
        /// 注册自定义设置终端参数Id
        /// <see cref="typeof(JT808.Protocol.MessageBody.JT808_0x8103_BodyBase)"/>
        /// <see cref="typeof(实现JT808_0x8103_BodyBase)"/>
        /// </summary>
        public Dictionary<uint, Type> JT808_0x8103Method { get; private set; }

        JT808Options IOptions<JT808Options>.Value
        {
            get { return this; }
        }
    }
}
