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
        //没有验证码的时候忽略
        [DataMember(Name = "captchaId", EmitDefaultValue = false, IsRequired = false)]
        public string CaptchaId { get; set; }
        [DataMember(Name = "captchaInstanceId", EmitDefaultValue = false)]
        public string CaptchaInstanceId { get; set; }
        //没有验证码的时候忽略
        [DataMember(Name = "captchaUserInput", EmitDefaultValue = false)]
        public string ValidateCode { get; set; }

        public string VerificationToken { get; set; }
        public string ImageSrc { get; set; }
        public string ServerId { get; set; }
        public CookieCollection Cookies { get; set; }
        public Blogger Blogger { get; set; }
        public LoginUserInfo()
        {
            Cookies = new CookieCollection();
            Blogger = new Blogger();
        }

        public void Logout()
        {
            Cookies = new CookieCollection();
            CaptchaId = "LoginCaptcha";
        }
    }
}
