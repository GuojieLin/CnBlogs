using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Common
{ 
    public class Constants
    {
        public const string AuthenticationCookiesName = ".CNBlogsCookie";
        public const string Host = "https://www.cnblogs.com";
        public const string Domain = ".cnblogs.com";
        public const string HadLogined = "您已处于登录状态";
        public const string AspxAutoDetectCookieSupport = "AspxAutoDetectCookieSupport";
        public const string DefaultAvatar = "ms-appx:///Asserts/sample_face.png";
        public const string XmlNameSpace = "xmlns=\"http://www.w3.org/2005/Atom\"";
        public const string ServerId = "SERVERID";
        public const string XRequestWith = "X-Requested-With";
        public const string XmlHttpRequest = "XMLHttpRequest";
        public const string VerificationToken = "VerificationToken";
        public const string JsonMediaType = "application/json";
        public const string FindVerificationTokenRegexString = "'VerificationToken'\\s*:\\s*'([^\\s\\n]+)'";
        public const string Url = "url";
        public const string GetImageSrc = @"<img.*?src=(['""]?)(?<url>[^'"" ]+)(?=\1)[^>]*>";
    }
}
