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
using System.IO;

namespace CnBlogs.Service
{
    public class AuthenticationService
    {
        private static Type _nativeLoginPageType;
        public static bool NeedReturn { get; private set; }
        /// <summary>
        /// 是否已登陆 
        /// </summary>
        public static bool IsLogin { get { return CacheManager.LoginUserInfo.IsLogin; } }
        public static void RedictLoginPage()
        {
            NavigationService.Instance.LastFrameNavigate(_nativeLoginPageType);
            NeedReturn = true;
        }
        public static void ReturnPreviousPage()
        {
            NavigationService.Instance.GoBack(null);
        }
        public static void SetLoginPage(Type nativeLoginType)
        {
            _nativeLoginPageType = nativeLoginType;
        }

        public async static Task<Blogger> LoadUserInfoAsync()
        {
            Blogger temp = await BlogService.LoadCurrentUserBlogAppAsync();
            if (temp == null) return null;
            Blogger blogger =  await BlogService.LoadCurrentUserInfoAsync(temp.BlogApp);
            if (blogger == null) return null;
            blogger.BlogApp = temp.BlogApp;
            blogger.IconName = temp.IconName;
            return blogger;
        }
        public async static Task InitLoginUserInfo(LoginUserInfo loginUserInfo)
        {
            Uri uri = new Uri(WcfApiUrlConstants.LoginUrl);
            //先请求一次获取cookies
            var response = await HttpHelper.HttpClient.GetAsync(uri);
            string html = await response.Content.ReadAsStringAsync();
            if (loginUserInfo.VerificationToken.IsNullOrEmpty())
            {
                //LoginCaptcha_CaptchaImage 验证码 获取 src
                Match match = Regex.Match(html, Constants.GetImageSrc);
                if (match.Success)
                {
                    loginUserInfo.ImageSrc = match.Groups[Constants.Url].Value;
                }
                match = Regex.Match(html, Constants.FindVerificationTokenRegexString);
                if (match.Success)
                {
                    loginUserInfo.VerificationToken = match.Groups[1].Value;
                }
            }
            Cookie serverId = HttpHelper.LoadCookieFromHeader(response.Headers, Constants.ServerId);
            loginUserInfo.ServerId = serverId.Value;
        }

        public async static Task<byte[]> LoadValidateImage(LoginUserInfo loginUserInfo)
        {
            //获取验证码实例ID
            //uri.Query,t=f8d90cc2aa2d4972bd2f1e46ae61364a
            loginUserInfo.CaptchaInstanceId = loginUserInfo.ImageSrc.Substring(loginUserInfo.ImageSrc.IndexOf(";t=") + 3);
            return await HttpHelper.HttpClient.GetByteArrayAsync(WcfApiUrlConstants.BaseLoginUrl + loginUserInfo.ImageSrc);
        }

        public async static Task<LoginResult> SignInAsync(LoginUserInfo loginUserInfo)
        {
            try
            {
                string data = JsonSerializeHelper.Serialize(loginUserInfo);
                var content = new StringContent(data, Encoding.UTF8, Constants.JsonMediaType);

                Uri uri = new Uri(WcfApiUrlConstants.LoginUrl);
                content.Headers.Add(Constants.XRequestWith, Constants.XmlHttpRequest);
                content.Headers.Add(Constants.VerificationToken, loginUserInfo.VerificationToken);
                HttpHelper.HttpClientHandler.CookieContainer.Add(uri, new Cookie(Constants.ServerId, loginUserInfo.ServerId));
                HttpHelper.HttpClientHandler.CookieContainer.Add(uri, new Cookie(Constants.AspxAutoDetectCookieSupport, "1"));
                var response = await HttpHelper.HttpClient.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                LoginResult postResult = JsonSerializeHelper.Deserialize<LoginResult>(responseContent);
                if (postResult.Success)// || postResult.Message == Constants.HadLogined)//提示已登录过cnblogs不能从响应中获取cookie。
                {
                    Cookie cookie = HttpHelper.LoadCookieFromHeader(response.Headers, Constants.AuthenticationCookiesName);
                    HttpHelper.HttpClientHandler.CookieContainer.Add(uri, cookie);
                    //登录成功先保存cookie
                    CacheManager.Current.UpdateCookies(cookie);
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
