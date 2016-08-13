using CnBlogs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CnBlogs.Common
{
    public class OptimizationDisplayHelper
    {
        public const string BaseCss = "<style>"
                        + "html{-ms-content-zooming:none;font-family:微软雅黑;}"
                        + ".author{font-weight:bold;} .bio{color:gray;}"
                        + "body{padding:10px;word-break:break-all;} p{margin:10px auto;} a{color:skyblue;} img{width:100% !important; }"
                        + "body{line-height:150%;}"
                        + "p{ word-wrap: break-word;word-break: normal; }"
                        + "pre{ word-wrap: break-word;word-break: normal; font-size:30px !important; }"
                        //word-break设置强行换行;normal 亚洲语言和非亚洲语言的文本规则，允许在字内换行
                        + "</style>";   //基础css
        public const string DarkThemeCss = "<style>"
                        + "body{background-color:black !important;color:gray !important;}"
                        + "</style>";
        public const string BigFontSizeCss = "<style>body{font-size:45px;} h1{font-size:62px;} h2{font-size:58px;} h3{font-size:52px;} h4,h5,h6{font-size:48px;}</style>";
        public const string SmallFontSizeCss = "<style>body{font-size:35px !important;} h1{font-size:45px !important;} h2{font-size:42px !important;} h3{font-size:40px !important;} h4,h5,h6{font-size:38px !important;}</style>";
        public static string OptimizationHtmlDisplay(string html)
        {
            string ex_mark = "<base target='_blank'/>";
            string js = "";   //图片加载脚本
            string body = html;

            //#region 移除自带标签属性

            //body = Regex.Replace(html, @"<p(/<[^>]*>/g", (m) =>
            //{
            //    //if (m.Value.Contains("avatar"))
            //    //{
            //    //    return m.Value;
            //    //}
            //    //else
            //    Match match = Regex.Match(m.Value.ToString(), @"<p(/<[^>]*>/g>");
            //    if (match.Success)
            //    {
            //        //移除长宽标签 最大化图片
            //        return "<p>";
            //    }
            //    else
            //    {
            //        return m.Value;
            //    }
            //}, RegexOptions.IgnoreCase);  //替换所有img标签 为本地图片


            //#endregion

            #region 无图模式
            //if (DataShareManager.Current.NOImagesMode)  //无图模式
            //{
            if (NetworkManager.Current.Network != NewWorkType.WIFI)  //非wifi
            {
                body = Regex.Replace(html, @"<img.*?src=(['""]?)(?<url>[^'"" ]+)(?=\1)[^>]*>", (m) =>
                {
                    //if (m.Value.Contains("avatar"))
                    //{
                    //    return m.Value;
                    //}
                    //else
                    Match match = Regex.Match(m.Value.ToString(), @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>");
                    if (match.Success)
                    {
                        //移除长宽标签 最大化图片
                        //return "<img src='" + match.Groups["imgUrl"].Value + "'/>";
                        return @"<img src=""ms-appx-web:///Assets/default_image.png"" onclick=""click2loadimage(this,'" + match.Groups["imgUrl"].Value + @"');""/>";
                    }
                    else
                    {
                        return m.Value;
                    }
                }, RegexOptions.IgnoreCase);  //替换所有img标签 为本地图片

                js = "<script>"  //点击加载图片
                    + "function click2loadimage(obj,source)"
                    + "{"
                    + "obj.setAttribute('src','ms-appx-web:///Assets/default_image_loading.png');"
                    + "obj.setAttribute('src',source);"
                    + "}"
                    + "</script>";
            }
            //else
            //{
            //    body = sc.Body;
            //}
            #endregion
            //合并
            return "<html><head>" + ex_mark + BaseCss + SmallFontSizeCss + js + "</head>" + "<body>" + body + "</body></html>";  //附加css

        }
    }
}
