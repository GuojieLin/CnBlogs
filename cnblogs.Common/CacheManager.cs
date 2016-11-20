using CnBlogs.Core;
using CnBlogs.Core.Constants;
using CnBlogs.Core.Enums;
using CnBlogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace CnBlogs.Common
{
    /// <summary>
    /// 缓存只保留在本地
    /// </summary>
    public sealed class CacheManager : NotifyPropertyChanged,IManager
    {
        public static LoginUserInfo LoginUserInfo { get; set; }
        public readonly static CacheManager Current = new CacheManager();
        private CacheManager()
        {
            _setting = new LocalSetting();
            Load();
        }
        private ISetting _setting;
        public void Load()
        {
            ApplicationDataCompositeValue composite;
            LoginUserInfo = new LoginUserInfo();
            //获取用户登录信息
            if (_setting.GetSetting(nameof(LoginUserInfo), out composite))
            {
                //存在缓存
                LoginUserInfo.UserName = (string)composite[nameof(LoginUserInfo.UserName)];
                LoginUserInfo.Password = (string)composite[nameof(LoginUserInfo.Password)];
                LoginUserInfo.Blogger.Guid = (string)composite[nameof(LoginUserInfo.Blogger.Guid)];
                LoginUserInfo.Blogger.BlogApp = (string)composite[nameof(LoginUserInfo.Blogger.BlogApp)];
                ApplicationDataCompositeValue cookiesComposite;
                //存在cookies则获取
                if (_setting.GetSetting(nameof(LoginUserInfo.Cookies), out cookiesComposite))
                {
                    foreach (string key in cookiesComposite.Keys)
                    {
                        var cookie = new Cookie(key, (string)cookiesComposite[key],"/", Constants.Domain);
                        LoginUserInfo.Cookies.Add(cookie);
                        HttpHelper.AddCookies(new Uri(Constants.Host), cookie);
                    }
                }
            }
        }
        public void UpdateCookies(CookieCollection cookies)
        {
            LoginUserInfo.Cookies = cookies;
            ApplicationDataCompositeValue cookiesComposite;
            if (!_setting.GetSetting(nameof(LoginUserInfo.Cookies), out cookiesComposite))
            {
                cookiesComposite = new ApplicationDataCompositeValue();
            }
            foreach (Cookie c in cookies)
            {
                cookiesComposite[c.Name] = c.Value;
            }
            _setting.SetSetting(nameof(LoginUserInfo.Cookies), cookiesComposite);
        }
        public void UpdateCookies()
        {
            ApplicationDataCompositeValue cookiesComposite;
            if (!_setting.GetSetting(nameof(LoginUserInfo.Cookies), out cookiesComposite))
            {
                cookiesComposite = new ApplicationDataCompositeValue();
            }
            foreach (Cookie cookie in LoginUserInfo.Cookies)
            {
                cookiesComposite[cookie.Name] = cookie.Value;
            }
            _setting.SetSetting(nameof(LoginUserInfo.Cookies), cookiesComposite);
        }
        public void UpdateCookies(Cookie cookie)
        {
            LoginUserInfo.Cookies.Add(cookie);
            UpdateCookies();
        }
        public void UpdatePassword(string password)
        {
            LoginUserInfo.Password = password;
            ApplicationDataCompositeValue composite;
            if (!_setting.GetSetting(nameof(LoginUserInfo), out composite))
            {
                composite = new ApplicationDataCompositeValue();
            }
            composite[nameof(LoginUserInfo.Password)] = password;
            _setting.SetSetting(nameof(LoginUserInfo), composite);
        }
        public void UpdateLogout()
        {
            ApplicationDataCompositeValue cookiesComposite;
            if (_setting.GetSetting(nameof(LoginUserInfo.Cookies), out cookiesComposite))
            {
                cookiesComposite = null;
            }
            _setting.SetSetting(nameof(LoginUserInfo.Cookies), cookiesComposite);

            ApplicationDataCompositeValue composite;
            if (!_setting.GetSetting(nameof(LoginUserInfo), out composite))
            {
                composite = new ApplicationDataCompositeValue();
            }
            composite[nameof(LoginUserInfo.Blogger.BlogApp)] = null;
            composite[nameof(LoginUserInfo.Blogger.Guid)] = null;
            _setting.SetSetting(nameof(LoginUserInfo), composite);
        }

        public void UpdateLoginBlogger(Blogger blogger)
        {
            LoginUserInfo.Blogger = blogger;
            ApplicationDataCompositeValue composite;
            if (!_setting.GetSetting(nameof(LoginUserInfo), out composite))
            {
                composite = new ApplicationDataCompositeValue();
            }
            composite[nameof(LoginUserInfo.Blogger.BlogApp)] = LoginUserInfo.Blogger.BlogApp;
            composite[nameof(LoginUserInfo.Blogger.Guid)] = LoginUserInfo.Blogger.Guid;
            _setting.SetSetting(nameof(LoginUserInfo), composite);
        }
        public void UpdateLoginUserInfo(string userName, string password, bool isRemerber = false, Cookie cookie = null)
        {
            LoginUserInfo.UserName = userName;
            LoginUserInfo.Password = password;
            LoginUserInfo.IsRemerber = isRemerber;
            ApplicationDataCompositeValue composite;
            if (!_setting.GetSetting(nameof(LoginUserInfo), out composite))
            {
                composite = new ApplicationDataCompositeValue();
            }
            composite[nameof(LoginUserInfo.UserName)] = userName;
            composite[nameof(LoginUserInfo.Password)] = password;
            composite[nameof(LoginUserInfo.IsRemerber)] = isRemerber;
            if (cookie != null) UpdateCookies(cookie);
            _setting.SetSetting(nameof(LoginUserInfo), composite);
        }
    }
}
