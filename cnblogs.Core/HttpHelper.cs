using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using CnBlogs.Core.Extentsions;

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
        public static HttpClient HttpClient { get; private set; }
        public static HttpClientHandler HttpClientHandler { get; private set; }
        private static CookieContainer _cookieContainer;
        static HttpHelper()
        {
            HttpClientHandler = new HttpClientHandler();
            //HttpClientHandler.AllowAutoRedirect = false;
            _cookieContainer = new CookieContainer();
            HttpClientHandler.CookieContainer = _cookieContainer;
            HttpClient = new HttpClient(HttpClientHandler);
            var userAgent = "Mozilla/5.0 (Windows Phone 10.0; Android 6.0.0; WebView/3.0; Microsoft; Virtual) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Mobile Safari/537.36 Edge/12.10240 sample/1.0";
            HttpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        public static void AddCookies(Uri uri,Cookie cookie)
        {
            HttpHelper.HttpClientHandler.CookieContainer.Add(uri, cookie);

        }
        /// <summary>
        /// 访问服务器时的cookies
        /// </summary>
        public static CookieContainer CookiesContainer;
        /// <summary>
        /// 向服务器发送get请求  返回服务器回复数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<string> GetAsync(string url)
        {
            Uri uri = new Uri(url);

            using (HttpResponseMessage response = await HttpClient.GetAsync(uri))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// 向服务器发送post请求 返回服务器回复数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async static Task<string> Post(string url, string data, CookieCollection cookieCollection)
        {
            Uri uri = new Uri(url);
            foreach (Cookie cookie in cookieCollection)
            {
                _cookieContainer.Add(uri, cookie);
            }
            HttpContent content = new StringContent(data,Encoding.UTF8, "application/json");
            HttpResponseMessage response = await HttpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public async static Task<string> Post(string url, string data, string cookies)
        {
            Uri uri = new Uri(url);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpClient.DefaultRequestHeaders.Add("Set-Cookie", cookies);
            HttpResponseMessage response = await HttpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


        public static Cookie LoadCookieFromHeader(HttpResponseHeaders headers, string key)
        {
            IEnumerable<string> temps;
            if (headers.TryGetValues("Set-Cookie", out temps))
            {
                string temp = temps.FirstOrDefault(t => t.Contains(key));
                if (!temp.IsNullOrEmpty())
                {
                    string value = temp.Split(';')[0].Split('=')[1];
                    Cookie cookie = new Cookie(key, value);
                    return cookie;
                }
            }
            return null;
        }
    }
}
