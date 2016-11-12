using CnBlogs.Common;
using CnBlogs.Core;
using CnBlogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using CnBlogs.Core.Extentsions;

namespace CnBlogs.Service
{
    public class AuthenticationService
    {
        public static string LoginUri = "https://passport.cnblogs.com/user/signin";
        private static Type _nativeLoginPageType;
        public static bool NeedReturn { get; private set; }
        /// <summary>
        /// 是否已登陆 
        /// </summary>
        public static bool IsLogin { get { return CacheManager.LoginUserInfo.IsLogin; } }
        public static void RedictLoginPage()
        {
            NavigationService.Instance.LastFrame.Navigate(_nativeLoginPageType);
            NeedReturn = true;
        }
        public static void ReturnPreviousPage()
        {
            NavigationService.Instance.LastFrame.GoBack();
        }
        public static void SetLoginPage(Type nativeLoginType)
        {
            _nativeLoginPageType = nativeLoginType;
        }
        public async static Task InitLoginUserInfo(LoginUserInfo loginUserInfo)
        {

            Uri uri = new Uri(LoginUri);
            //先请求一次获取cookies
            var response = await HttpHelper.HttpClient.GetAsync(uri);
            string html = await response.Content.ReadAsStringAsync();
            loginUserInfo = new LoginUserInfo();
            if (loginUserInfo.VerificationToken.IsNullOrEmpty())
            {
                //LoginCaptcha_CaptchaImage 验证码 获取 src
                Match match = Regex.Match(html, @"<img.*?src=(['""]?)(?<url>[^'"" ]+)(?=\1)[^>]*>");
                if (match.Success)
                {
                    loginUserInfo.ImageSrc = match.Groups[2].Value;
                }
                match = Regex.Match(html, "'VerificationToken'\\s*:\\s*'([^\\s\\n]+)'");
                if (match.Success)
                {
                    loginUserInfo.VerificationToken = match.Groups[1].Value;
                }
            }
            Cookie serverId = HttpHelper.LoadCookieFromHeader(response.Headers, "SERVERID");
            loginUserInfo.ServerId = serverId.Value;
        }
        public async static Task<LoginResult> SignInAsync(LoginUserInfo loginUserInfo,Cookie cookie)
        {
            try
            {
                string data = JsonSerializeHelper.Serialize(loginUserInfo);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                Uri uri = new Uri(LoginUri);
                HttpHelper.HttpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                HttpHelper.HttpClient.DefaultRequestHeaders.Add("VerificationToken", loginUserInfo.VerificationToken);
                HttpHelper.HttpClientHandler.CookieContainer.Add(uri, new Cookie("SERVERID", loginUserInfo.ServerId));
                HttpHelper.HttpClientHandler.CookieContainer.Add(uri, new Cookie("AspxAutoDetectCookieSupport", "1"));
                var response = await HttpHelper.HttpClient.PostAsync(new Uri(LoginUri), content);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                LoginResult postResult = JsonSerializeHelper.Deserialize<LoginResult>(responseContent);
                if (postResult.Success)
                {
                    cookie = HttpHelper.LoadCookieFromHeader(response.Headers, ".CNBlogsCookie");
                }
                return postResult;
            }
            catch(Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
                return new LoginResult() { Success = false, Message = exception.Message };
            }
        }
    }
    
}
