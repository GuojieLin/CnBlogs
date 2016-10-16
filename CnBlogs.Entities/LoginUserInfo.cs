using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Entities
{
    public class LoginUserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsLogin { get; set; }
        public bool IsRemerber { get; set; }
        public CookieCollection Cookies { get; set; }

    }
}
