using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Extensions.DependencyInjection.Options
{
    public static  class JT808OptionsExtensions
    {
        /// <summary>
        /// 注册自定义数据下行透传信息
        /// </summary>
        /// <typeparam name="TJT808_0x0900_Ext"></typeparam>
        /// <param name="passthroughType"></param>
        public static JT808Options Register_0x8900_Ext<TJT808_0x8900_Ext>(this JT808Options jT808Options, byte passthroughType)
               where TJT808_0x8900_Ext : JT808_0x8900_BodyBase
        {
            if (!jT808Options.JT808_0x8900Method.ContainsKey(passthroughType))
            {
                jT808Options.JT808_0x8900Method.Add(passthroughType, typeof(TJT808_0x8900_Ext));
            }
            return jT808Options;
        }
        /// <summary>
        /// 注册电子运单内容实现类
        /// </summary>
        /// <typeparam name="TJT808_0x0701Body"></typeparam>
        /// <returns></returns>
        public static JT808Options Register_JT808_0x0701Body<TJT808_0x0701Body>(this JT808Options jT808Options)
               where TJT808_0x0701Body : JT808_0x0701.JT808_0x0701Body
        {
            JT808GlobalConfig.Instance.Register_JT808_0x0701Body<TJT808_0x0701Body>();
            return jT808Options;
        }
        /// <summary>
        /// 注册自定义消息
        /// </summary>
        /// <typeparam name="TJT808Bodies"></typeparam>
        /// <param name="customMsgId"></param>
        /// <returns></returns>
        public static JT808Options Register_CustomMsgId<TJT808Bodies>(this JT808Options jT808Options, ushort customMsgId)
            where TJT808Bodies : JT808Bodies
        {
            JT808GlobalConfig.Instance.Register_CustomMsgId<TJT808Bodies>(customMsgId);
            return jT808Options;
        }
        /// <summary>
        /// 重写消息
        /// </summary>
        /// <typeparam name="TJT808Bodies"></typeparam>
        /// <param name="overwriteMsgId"></param>
        /// <returns></returns>
        public static JT808Options Overwrite_MsgId<TJT808Bodies>(this JT808Options jT808Options,ushort overwriteMsgId)
            where TJT808Bodies : JT808Bodies
        {
            JT808GlobalConfig.Instance.Overwrite_MsgId<TJT808Bodies>(overwriteMsgId);
            return jT808Options;
        }
        /// <summary>
        /// 注册自定义数据下行透传信息
        /// </summary>
        /// <see cref="typeof(JT808.Protocol.MessageBody.JT808_0x8103_BodyBase)"/>
        /// <see cref="typeof(实现JT808_0x8103_BodyBase)"/>
        public static JT808Options Register_0x8103_ParamId<TJT808_0x8103_BodyBase>(this JT808Options jT808Options,uint paramId)
               where TJT808_0x8103_BodyBase : JT808_0x8103_BodyBase
        {
            if (!jT808Options.JT808_0x8103Method.ContainsKey(paramId))
            {
                jT808Options.JT808_0x8103Method.Add(paramId, typeof(TJT808_0x8103_BodyBase));
            }
            return jT808Options;
        }

    }
}
