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
                LoginUserInfo.IsLogin = (bool)(composite[nameof(LoginUserInfo.IsLogin)] ?? false);
                ApplicationDataCompositeValue cookiesComposite;
                //存在cookies则获取
                if (_setting.GetSetting(nameof(LoginUserInfo.Cookies), out cookiesComposite))
                {
                    LoginUserInfo.Cookies = new CookieCollection();
                    foreach (string key in cookiesComposite.Keys)
                    {
                        LoginUserInfo.Cookies.Add(new Cookie(key, (string)cookiesComposite[key]));
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
        public void UpdateIsLogin(bool isLogin)
        {
            LoginUserInfo.IsLogin = isLogin;
            ApplicationDataCompositeValue composite;
            if (!_setting.GetSetting(nameof(LoginUserInfo), out composite))
            {
                composite = new ApplicationDataCompositeValue();
            }
            composite[nameof(LoginUserInfo.IsLogin)] = isLogin;
            _setting.SetSetting(nameof(LoginUserInfo), composite);
        }
        public void UpdateLoginUserInfo(string userName,string password,bool isRemerber = false)
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
            _setting.SetSetting(nameof(LoginUserInfo), composite);
        }
    }
}
