using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

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
        private static HttpClient _httpClient;
        private static HttpClientHandler _httpClientHander;
        private static CookieContainer _cookieContainer;
        static HttpHelper()
        {
            _httpClientHander = new HttpClientHandler();
            _cookieContainer = new CookieContainer();
            _httpClientHander.CookieContainer = _cookieContainer;
            _httpClient = new HttpClient(_httpClientHander);
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
        public async static Task<string> Get(string url)
        {
            Uri uri = new Uri(url);

            using (HttpResponseMessage response = await _httpClient.GetAsync(uri))
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
            var userAgent = "Mozilla/5.0 (Windows Phone 10.0; Android 6.0.0; WebView/3.0; Microsoft; Virtual) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Mobile Safari/537.36 Edge/12.10240 sample/1.0";
            _httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
