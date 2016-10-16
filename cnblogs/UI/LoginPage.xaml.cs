﻿using CnBlogs.Common;
using CnBlogs.Entities;
using CnBlogs.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CnBlogs.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private LoginViewModel _loginViewModel;
        public const string HomeUri = "http://www.cnblogs.com/";
        public const string PublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCp0wHYbg/NOPO3nzMD3dndwS0MccuMeXCHgVlGOoYyFwLdS24Im2e7YyhB0wrUsyYf0/nhzCzBK8ZC9eCWqd0aHbdgOQT6CuFQBMjbyGYvlVYU2ZP7kG9Ft6YV6oc9ambuO7nPZh+bvXH0zDKfi02prknrScAKC0XhadTHT3Al0QIDAQAB";
        public const  string LoginUri = "https://passport.cnblogs.com/user/signin?ReturnUrl=https%3A%2F%2Fhome.cnblogs.com%2F";
        public LoginPage()
        {
            this.InitializeComponent();
            //从缓存中获取用户名和密码
            LoadLoginUserInfoFromCache();
            //var bytes = RSACryptoHelper.Encrypt(PublicKey, "a604572782");
            //string encry = Convert.ToBase64String(bytes);
            LoginWebView.LoadCompleted += LoginWebView_LoadCompleted;
            LoginWebView.NavigationStarting += LoginWebView_NavigationStarting;
            NavigationWithCookies();
        }

        private void LoadLoginUserInfoFromCache()
        {
            LoginUserInfo loginUserInfo = CacheManager.LoginUserInfo;
            _loginViewModel = new LoginViewModel(loginUserInfo);
        }
        /// <summary>
        /// 带上cookies
        /// </summary>
        private void NavigationWithCookies()
        {
            string loginuri = LoginUri;
            Uri uri = new Uri(loginuri);
            //var httpBaseProtocolFilter = new HttpBaseProtocolFilter();
            //httpBaseProtocolFilter.UseProxy = true;
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            var userAgent = "Mozilla/5.0 (Windows Phone 10.0; Android 6.0.0; WebView/3.0; Microsoft; Virtual) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Mobile Safari/537.36 Edge/12.10240 sample/1.0";
            httpRequestMessage.Headers.Add("User-Agent", userAgent);
            if (_loginViewModel.IsLogin && _loginViewModel.Cookies != null)
            {
                //如果用于已经登陆,则请求带上当前用户的Cookies
                foreach (Cookie c in _loginViewModel.Cookies)
                {
                    HttpCookiePairHeaderValue httpCookie = new HttpCookiePairHeaderValue(c.Name, c.Value);
                    httpRequestMessage.Headers.Cookie.Add(httpCookie);
                }
            }
            LoginWebView.NavigateWithHttpRequestMessage(httpRequestMessage);
        }
        bool isRemerberTemp;
        string userNameTemp;
        string passwordTemp;
        private async void LoginWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            userNameTemp = await GetUserName();
            passwordTemp = await GetPassword();
            isRemerberTemp = args.Uri.Query.Contains("remember");
        }

        private async void LoginWebView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.Uri.AbsoluteUri.Contains("passport.cnblogs.com"))
            {
                //若是登录页面,则设置用户名和密码
                string js = @"var username =document.getElementById('input1');
                    if(username!= null && username!='undefined')
                    username.value='" + _loginViewModel.UserName + "';" +
                    @"var password =document.getElementById('input2');
                    if(password!= null && password!='undefined')
                    password.value='" + _loginViewModel.Password + "';";
                await LoginWebView.InvokeScriptAsync("eval", new string[] { js });
            }
            //登录完成获取cookeis
            var cookiesCollection = GetBrowserCookie(e.Uri);
            bool isLoginSucceeded = cookiesCollection.FirstOrDefault(c => c.Name.ToUpperInvariant()
            .Contains(".CNBlogsCookie".ToUpperInvariant())) != null;

            if (cookiesCollection != null)
            {
                if (_loginViewModel.Cookies == null) _loginViewModel.Cookies = new System.Net.CookieCollection();
                foreach (var cookies in cookiesCollection)
                {
                    _loginViewModel.Cookies.Add(new System.Net.Cookie(cookies.Name, cookies.Value));
                }
                CacheManager.Current.UpdateCookies(_loginViewModel.Cookies);
            }
            if (isLoginSucceeded)//登录成功
            {
                CacheManager.Current.UpdateIsLogin(true);
                if (isLoginSucceeded)
                {
                    //获取用户名和登录密码,是否记住密码
                    _loginViewModel.UserName = userNameTemp;
                    _loginViewModel.Password = passwordTemp;
                    _loginViewModel.IsRemerber = isRemerberTemp;
                    CacheManager.Current.UpdateLoginUserInfo(_loginViewModel.UserName,_loginViewModel.Password,isRemerberTemp);
                }
            }
            else
            {
                CacheManager.Current.UpdateIsLogin(false);
            }
        }

        private async Task<string> InvokeScriptAsync(string js)
        {
            return await LoginWebView.InvokeScriptAsync("eval", new string[] { js });
        }
        private async Task<string> GetPassword()
        {
            string js = "var password = document.getElementById('input2');if(password!= null && password!='undefined') password.value";
            return await InvokeScriptAsync(js);
        }
        private async Task<string> GetUserName()
        {
            string js = "var username =document.getElementById('input1'); if(username!= null && username!='undefined') username.value";

            return await InvokeScriptAsync(js);
        }
        
        private HttpCookieCollection GetBrowserCookie(Uri targetUri)
        {
            var httpBaseProtocolFilter = new HttpBaseProtocolFilter();
            var cookieManager = httpBaseProtocolFilter.CookieManager;
            var cookieCollection = cookieManager.GetCookies(targetUri);
            return cookieCollection;
        }
    }
    public class RSACryptoHelper
    {
        public static byte[] Encrypt(string publicKey, string data)
        {
            //从base64获取keybuffer
            IBuffer keyBuffer = CryptographicBuffer.DecodeFromBase64String(publicKey);
            //1024位rsa加密
            AsymmetricKeyAlgorithmProvider asym = AsymmetricKeyAlgorithmProvider.OpenAlgorithm(AsymmetricAlgorithmNames.RsaPkcs1);
            //导入公钥
            CryptographicKey key = asym.ImportPublicKey(keyBuffer);
            //加密数据转化
            IBuffer plainBuffer = CryptographicBuffer.ConvertStringToBinary(data, BinaryStringEncoding.Utf8);
            //加密
            IBuffer encryptedBuffer = CryptographicEngine.Encrypt(key, plainBuffer, null);

            byte[] encryptedBytes;
            CryptographicBuffer.CopyToByteArray(encryptedBuffer, out encryptedBytes);
            return encryptedBytes;
        }
    }
}
