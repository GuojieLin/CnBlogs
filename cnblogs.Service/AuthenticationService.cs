using CnBlogs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Service
{
    public class AuthenticationService
    {
        private static Type _loginPageType;
        public static bool NeedReturn { get; private set; }
        /// <summary>
        /// 是否已登陆 
        /// </summary>
        public static bool IsLogin { get { return CacheManager.LoginUserInfo.IsLogin; } }
        public static void RedictLoginPage()
        {
            NavigationService.Instance.LastFrame.Navigate(_loginPageType);
            NeedReturn = true;
        }
        public static void ReturnPreviousPage()
        {
            NavigationService.Instance.LastFrame.GoBack();
        }
        public static void SetLoginPage(Type type)
        {
            _loginPageType = type;
        }
    }
}
