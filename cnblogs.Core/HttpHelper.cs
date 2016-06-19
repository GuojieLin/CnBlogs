using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Windows.Web.Http;
using System.Text;
using System.Threading.Tasks;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	6/19/2016 3:21:43 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace CnBlogs.Core
{
    public class HttpHelper
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
        public async static Task<string> Get(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uri = new Uri(url);

                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (TaskCanceledException taskCanceledException)
            {
                // 因超时取消请求的逻辑
                throw;
            }
            catch (System.Net.Http.HttpRequestException httpRequestException)
            { 
                // 处理其它可能异常的逻辑{
                throw;
            }
            catch (Exception exception)
            {
                // 处理其它可能异常的逻辑{
                throw;
            }

        }
        /// <summary>
        /// 向服务器发送post请求 返回服务器回复数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async static Task<string> Post(string url, string body)
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

            catch (TaskCanceledException taskCanceledException)
            {
                throw;
                // 因超时取消请求的逻辑
            }
            catch (System.Net.Http.HttpRequestException httpRequestException)
            {
                throw;
                // 处理其它可能异常的逻辑
            }
            catch (Exception exception)
            {
                throw;
                // 处理其它可能异常的逻辑
            }

        }
    }
}
