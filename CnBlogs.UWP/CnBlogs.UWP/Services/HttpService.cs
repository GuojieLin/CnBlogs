﻿using System;
using System.Net;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace CnBlogs.UWP.Services
{
    /// <summary>
    /// 访问HTTP服务器基础服务
    /// </summary>
    static class HttpService
    {
        /// <summary>
        /// 访问服务器时的cookies
        /// </summary>
        public static CookieContainer CookiesContainer;
        /// <summary>
        /// 向服务器发送get请求  返回服务器回复数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<string> SendGetRequest(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uri = new Uri(url);

                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                return  await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// 向服务器发送post请求 返回服务器回复数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async static Task<string> SendPostRequest(string url, string body)
        {
            try
            {
                HttpRequestMessage mSent = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                mSent.Content = new HttpStringContent(body, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json; charset=utf-8");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.SendRequestAsync(mSent);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
