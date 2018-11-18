using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.DependencyInjection.Options
{
    public class JT808Options
    {
        /// <summary>
        /// 设置跳过校验码
        /// 场景：测试的时候，可能需要收到改数据，所以测试的时候有用
        /// </summary>
        public bool SkipCRCCode { get; set; }
    }
}
