using CnBlogs.Common;
using CnBlogs.Core;
using CnBlogs.Core.Extentsions;
using CnBlogs.Entities;
using CnBlogs.Service;
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
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
        public LoginPage()
        {
            this.InitializeComponent();
            LoadLoginUserInfoFromCache();
            Logining();
            AuthenticationService.InitLoginUserInfo(_loginViewModel.LoginUserInfo).ContinueWith(task =>
            {
                this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (_loginViewModel.LoginUserInfo.ImageSrc.IsNullOrEmpty())
                    {
                        //无需加载验证码
                        ValidateCodeGrid.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        //加载验证码
                        ValidateCodeGrid.Visibility = Visibility.Visible;
                        RefreshValidateImage();
                    }
                    LoginCompleted();
                });
            });
        }

        private async void RefreshValidateImage()
        {
            //加载验证码
            var bytes = await AuthenticationService.LoadValidateImage(_loginViewModel.LoginUserInfo);
            BitmapImage image = new BitmapImage();
            using (InMemoryRandomAccessStream memoryStream = new InMemoryRandomAccessStream())
            {
                DataWriter writer = new DataWriter(memoryStream.GetOutputStreamAt(0));
                writer.WriteBytes(bytes);
                await writer.StoreAsync();
                await image.SetSourceAsync(memoryStream);
            }
            ValidateCodeImage.Source = image;
        }

        public void Logining()
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            statusBar.BackgroundOpacity = 0.5; // 透明度  
            statusBar.ProgressIndicator.ShowAsync();
        }
        public void LoginCompleted()
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            statusBar.ProgressIndicator.HideAsync();
        }
        private void LoadLoginUserInfoFromCache()
        {
            LoginUserInfo loginUserInfo = CacheManager.LoginUserInfo;
            _loginViewModel = new LoginViewModel(loginUserInfo);
        }

        private async void LoginButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Logining();
            string userName = UserNameTextBox.Text.Trim();
            string password = PasswordWordBox.Password.Trim();

            _loginViewModel.LoginUserInfo.UserName = RSACryptoHelper.Encrypt(Uri.EscapeDataString(userName));
            _loginViewModel.LoginUserInfo.Password = RSACryptoHelper.Encrypt(Uri.EscapeDataString(password));
            Cookie cookie = null;
            var result = await AuthenticationService.SignInAsync(_loginViewModel.LoginUserInfo, cookie);
            if (!result.Success)
            {
                if (result.Message.Contains("验证码错误"))
                {
                    //刷新验证码
                    RefreshValidateImage();
                }
                else
                {
                    MessageDialog dialog = new MessageDialog(result.Message);
                    await dialog.ShowAsync();
                }
            }
            else
            {
                await LoginSuccess(cookie);
            }
            LoginCompleted();
        }
        private async Task LoginSuccess(Cookie cookie = null)
        {
            Blogger blogger = await AuthenticationService.LoadUserInfo();
            if (blogger == null)
            {
                MessageDialog dialog = new MessageDialog("加载用户信息出错!");
                await dialog.ShowAsync();
            }
            else
            {
                _loginViewModel.LoginUserInfo.Blogger = blogger;
            }
            CacheManager.Current.UpdateLoginUserInfo(_loginViewModel.UserName, _loginViewModel.Password, _loginViewModel.LoginUserInfo.Blogger, true, cookie);
            if (AuthenticationService.NeedReturn)
            {
                AuthenticationService.ReturnPreviousPage();
            }
        }
        #region htmlLogin
        private void GoHtmlLoginPageButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Grid.RowDefinitions[0].Height = new GridLength(0);
            Grid.RowDefinitions[1].Height = new GridLength(Grid.ActualHeight);
            LoadLoginUserInfoFromCache();
            LoginWebView.LoadCompleted += LoginWebView_LoadCompleted;
            LoginWebView.NavigationStarting += LoginWebView_NavigationStarting;
            NavigationWithCookies();
        }
        private void NavigationWithCookies()
        {
            Uri uri = new Uri(WcfApiUrlConstants.LoginUrl);
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
            //登陆成功取消跳转 
            Logining();
            userNameTemp = await GetUserName();
            passwordTemp = await GetPassword();
            isRemerberTemp = args.Uri.Query.Contains("remember");

            if (args.Uri.AbsoluteUri.StartsWith(WcfApiUrlConstants.HomeUrl))
            {
                LoginCompleted(args.Uri);
                args.Cancel = true;
            }
        }

        private async void LoginCompleted(Uri uri)
        {//登录完成获取cookeis
            var cookiesCollection = GetBrowserCookie(uri);
            bool isLoginSucceeded = cookiesCollection.FirstOrDefault(c => c.Name.ToUpperInvariant()
            .Contains(Common.Constants.AuthenticationCookiesName.ToUpperInvariant())) != null;

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
                //获取用户名和登录密码,是否记住密码
                _loginViewModel.UserName = userNameTemp;
                _loginViewModel.Password = passwordTemp;
                _loginViewModel.IsRemerber = isRemerberTemp;
                await LoginSuccess();
            }
            else
            {
                CacheManager.Current.UpdateLogout();
            }
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
            LoginCompleted();
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
        #endregion
    }
}
