﻿using System;
using System.Collections.Generic;

namespace Workman.Util
{
    /// <summary>
    /// 版 本  Workman-Lib V1.0.0CoreMvc
    /// Copyright (c) 2013-2018 水墨剑客
    /// 创建人：水墨剑客
    /// 日 期：2017.03.08
    /// 当前上下文执行用户信息
    /// </summary>
    public class UserInfo
    {
        #region 用户信息

        /// <summary>
        /// 用户主键
        /// </summary>		
        public string userId { get; set; }
        /// <summary>
        /// 工号
        /// </summary>	
        public string enCode { get; set; }
        /// <summary>
        /// 账户
        /// </summary>	
        public string account { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>		
        public string password { get; set; }
        /// <summary>
        /// 密码秘钥
        /// </summary>	
        public string secretkey { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string realName { get; set; }
        /// <summary>
        /// 呢称
        /// </summary>	
        public string nickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>	
        public string headIcon { get; set; }
        /// <summary>
        /// 性别
        /// </summary>	
        public int? gender { get; set; }
        /// <summary>
        /// 手机
        /// </summary>	
        public string mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>		
        public string telephone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>	
        public string email { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>		
        public string oICQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>	
        public string weChat { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>		
        public string companyId { get; set; }
        /// <summary>
        /// 所在公司及下属公司
        /// </summary>
        public List<string> companyIds { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>		
        public string departmentId { get; set; }
        /// <summary>
        /// 所在部门及下属部门
        /// </summary>
        public List<string> departmentIds { get; set; }
        /// <summary>
        /// 单点登录标识
        /// </summary>		
        public int? openId { get; set; }
        /// <summary>
        /// 角色信息
        /// </summary>
        public string roleIds { get; set; }
        /// <summary>
        /// 岗位信息
        /// </summary>
        public string postIds { get; set; }
        /// <summary>
        /// 是否是水墨剑客
        /// </summary>
        public bool isSystem { get; set; }
        #endregion

        #region 扩展信息
        /// <summary>
        /// 应用Id
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime logTime { get; set; }
        /// <summary>
        /// 登录IP地址
        /// </summary>
        public string iPAddress { get; set; }
        /// <summary>
        /// 浏览器名称
        /// </summary>
        public string browser { get; set; }
        /// <summary>
        /// 登录者标识
        /// </summary>
        public string loginMark { get; set; }
        /// <summary>
        /// 票据信息
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 即时通讯地址
        /// </summary>
        public string imUrl { get; set; }
        /// <summary>
        /// 即时通讯是否开启
        /// </summary>
        public string imOpen { get; set; }
        /// <summary>
        /// 流程实例Id
        /// </summary>
        public string wfProcessId { get; set; }
        #endregion

        /// <summary>
        /// 信息加载时间
        /// </summary>
        public DateTime? loadTime { get; set; }
    }
}
