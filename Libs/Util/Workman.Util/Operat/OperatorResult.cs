﻿
namespace Workman.Util.Operat
{
    /// <summary>
    /// 
    
    /// 创建人：水墨剑客
    /// 日 期：2017.03.08
    /// 描 述：当前连接用户信息返回数据
    /// </summary>
    public class OperatorResult
    {
        /// <summary>
        /// 状态码-1未登录,1登录成功,0登录过期
        /// </summary>
        public int stateCode { get; set; }
        /// <summary>
        /// 登录者用户信息
        /// </summary>
        public UserInfo userInfo { get; set; }
    }
}
