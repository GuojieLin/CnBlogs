using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CnBlogs.Entities;
using CnBlogs.Common;
using CnBlogs.Core;

namespace CnBlogs.ViewModels
{
    internal class LoginViewModel: NotifyPropertyChanged
    {
        public LoginViewModel(LoginUserInfo loginUserInfo)
        {
            this.UserName = loginUserInfo?.UserName;
            this.Password = loginUserInfo?.Password;
            this.IsLogin = loginUserInfo?.IsLogin ?? false;
            this.Cookies = loginUserInfo?.Cookies ?? new CookieCollection();

        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private bool _isLogin;
        public bool IsLogin
        {
            get { return _isLogin; }
            set
            {
                _isLogin = value;
                OnPropertyChanged(nameof(IsLogin));
            }
        }

        private bool _isRemerber;
        public bool IsRemerber
        {
            get { return _isRemerber; }
            set
            {
                _isRemerber = value;
                OnPropertyChanged(nameof(IsRemerber));
            }
        }
        public CookieCollection Cookies { get; set; }
        public void UpdateCookies()
        {
            CacheManager.Current.UpdateCookies(Cookies);
        }

        public void Login()
        {
        }
        public void LogOut()
        {
            CacheManager.Current.UpdateLogout();
        }
    }
}
