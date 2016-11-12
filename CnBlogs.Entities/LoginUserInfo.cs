using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Entities
{

    [DataContract]
    public class LoginUserInfo
    {
        [DataMember(Name = "input1")]
        public string UserName { get; set; }
        [DataMember(Name = "input2")]
        public string Password { get; set; }
        public bool IsLogin { get { return Cookies[".CNBlogsCookie"] != null; } }
        [DataMember(Name = "remember")]
        public bool IsRemerber { get; set; }
        public string VerificationToken { get; set; }
        public string ImageSrc { get; set; }
        public string ServerId { get; set; }
        public CookieCollection Cookies { get; set; }
        public LoginUserInfo()
        {
            Cookies = new CookieCollection();
        }
    }
}
