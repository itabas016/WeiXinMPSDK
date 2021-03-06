﻿/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：AppApi.cs
    文件功能描述：管理企业号应用接口
    
    
    创建标识：Senparc - 20150316
  
    增加功能：获取应用概况列表
    修改标识：Bemguin - 20150614 
 
    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170313
    修改描述：v4.2.3 AppApi.SetApp()方法改为POST请求方式
    
    -----------------------------------
    
    修改标识：Senparc - 20170616
    修改描述：从QY移植，同步Work接口

----------------------------------------------------------------*/

/*
    官方文档：http://work.weixin.qq.com/api/doc#10025
 */

using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work.AdvancedAPIs.App;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 应用管理
    /// </summary>
    public static class AppApi
    {
        #region 同步请求

        /// <summary>
        /// 获取企业号应用信息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppKey（推荐使用AppKey，需要先注册，可使用AccessTokenContainer.BuildingKey()方法生成）</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetAppInfoResult GetAppInfo(string accessTokenOrAppKey, int agentId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/get?access_token={0}&agentid={1}", accessToken.AsUrlData(), agentId.ToString("d").AsUrlData());
                return Get.GetJson<GetAppInfoResult>(url);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 设置企业号应用
        /// 此App只能修改现有的并且有权限管理的应用，无法创建新应用（因为新应用没有权限）
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="data">设置应用需要Post的数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WorkJsonResult SetApp(string accessToken, SetAppPostData data, int timeOut = Config.TIME_OUT)
        {
            //TODO:需要对SetAppPostData中的null值过滤
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/set?access_token={0}", accessToken.AsUrlData());

            return Post.PostGetJson<WorkJsonResult>(url, formData: null);
        }

        /// <summary>
        /// 获取应用概况列表【QY移植修改】
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetAppListResult GetAppList(string accessToken, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/list?access_token={0}", accessToken.AsUrlData());

            return Get.GetJson<GetAppListResult>(url);
        }
        #endregion

        #region 异步请求

        /// <summary>
        /// 【异步方法】获取企业号应用信息【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppKey（推荐使用AppKey，需要先注册，可使用AccessTokenContainer.BuildingKey()方法生成）</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetAppInfoResult> GetAppInfoAsync(string accessTokenOrAppKey, int agentId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/get?access_token={0}&agentid={1}", accessToken.AsUrlData(), agentId.ToString("d").AsUrlData());

                return await Get.GetJsonAsync<GetAppInfoResult>(url);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 【异步方法】设置企业号应用【QY移植修改】
        /// 此App只能修改现有的并且有权限管理的应用，无法创建新应用（因为新应用没有权限）
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="data">设置应用需要Post的数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SetAppAsync(string accessToken, SetAppPostData data, int timeOut = Config.TIME_OUT)
        {
            //TODO:需要对SetAppPostData中的null值过滤
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/set?access_token={0}", accessToken.AsUrlData());

            return await Post.PostGetJsonAsync<WorkJsonResult>(url, formData: null);
        }

        /// <summary>
        /// 【异步方法】获取应用概况列表【QY移植修改】
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetAppListResult> GetAppListAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/list?access_token={0}", accessToken.AsUrlData());

            return await Get.GetJsonAsync<GetAppListResult>(url);
        }
        #endregion
    }
}
