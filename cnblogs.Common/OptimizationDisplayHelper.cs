using CnBlogs.Core;
using CnBlogs.Core.Constants;
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
        public const string BaseCss = @"<style>html, body, div, span, applet, object, iframe, h1, h2, h3, h4, h5, h6, p, blockquote, pre, a, abbr, acronym, address, big, cite, code, del, dfn, em, img, ins, kbd, q, s, samp, small, strike, strong, sub, sup, tt, var, b, u, i, center, dl, dt, dd, ol, ul, li, fieldset, form, label, legend, table, caption, tbody, tfoot, thead, tr, th, td, article, aside, canvas, details, embed, figure, figcaption, footer, header, hgroup, menu, nav, output, ruby, section, summary, time, mark, audio, video {{
                                    margin: 2px; padding: 5px; border: 0; font: inherit; vertical-align: baseline; outline: none; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;}}" +
                                    "html {{ font-size:{0}px; -ms-content-zooming:none;font-family:微软雅黑;}}" +
                                    "body {{  word-wrap: break-word;word-break: normal; }}" +
                                    "article, aside, details, figcaption, figure, footer, header, hgroup, menu, nav, section {{ display: block; }}" +
                                    "strong {{ font-weight: bold; }}" +
                                    "table {{ border-collapse: collapse; border-spacing: 0; }}" +
                                    "img {{ border: 0; width: 100%; }}" +
                                    "p {{ color: #333;}}" +
                                    "</style>"; 
        public const string DarkThemeCss = "<style>"
                        + "body{background-color:black !important;color:gray !important;}"
                        + "</style>";
        public const string FontSizeCss = "<style>body{line-height: 1.2em;font-size:1.0em !important;} h1{font-size:1.4em !important;} h2{font-size:1.3em !important;} h3{font-size:1.2em !important;} h4,h5,h6{font-size:1.1em !important;}pre {font-size:1.0em ;background-color: #f5f5f5; border: #cccccc 1px solid; padding: 8px; margin:5px;}</style>";

        public const string ClickToLoadImageJs = "<script>"  //点击加载图片
                        + "function click2loadimage(obj,source)"
                        + "{"
                        + "obj.setAttribute('src','ms-appx-web:///Assets/default_image_loading.png');"
                        + "obj.setAttribute('src',source);"
                        + "}"
                        + "</script>";
        public static string OptimizationHtmlDisplay(string html)
        {
            if (html == null) return "";// throw new ArgumentNullException("html");
            string ex_mark = "<base target='_blank'/>";
            string body = html;

            html = RemoveAttributes(html);
            //移除图片的其他标签
            html = Regex.Replace(html, @"<img.*?src=(['""]?)(?<url>[^'"" ]+)(?=\1)[^>]*>", (m) =>
                {
                    #region 无图模式
                    string url = m.Groups["url"].Value;
                    if (string.IsNullOrEmpty(url)) url = Configuration.DefalutImagePath;
                    if (m.Groups["url"].Value.StartsWith("//", StringComparison.CurrentCultureIgnoreCase))
                    {
                        url = "http:" + url;
                    }
                    if (NetworkManager.Current.Network != NetWorkType.WIFI &&
                    SettingManager.Current.IsNoImagesMode)  //非wifi下无图模式，则不直接显示图片
                    {
                        Match match = Regex.Match(m.Value.ToString(), @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>");
                        if (match.Success)
                        {
                            //移除长宽标签 最大化图片
                            //return "<img src='" + match.Groups["imgUrl"].Value + "'/>";
                            return $"<img src=\"{Configuration.DefalutImagePath} \" onclick=\"click2loadimage(this,'{url}');\"/>";

                        }
                        else
                        {
                            return $"<img src='{url}'>";
                        }
                    }
                    #endregion
                    else
                    {
                        return $"<img src='{url}'>";
                    }
                }, RegexOptions.IgnoreCase);  //替换所有img标签 为本地图片


            StringBuilder builder = new StringBuilder();
            builder.Append("<html>");
            builder.Append("<head>");
            builder.Append(ex_mark);
            if (SettingManager.Current.Theme == Windows.UI.Xaml.ElementTheme.Dark)
                builder.Append(DarkThemeCss);
            builder.AppendFormat(BaseCss, (int)SettingManager.Current.FontSize);
            builder.Append(FontSizeCss);
            builder.Append(ClickToLoadImageJs);
            builder.Append("</head><body>");
            builder.Append(html);
            builder.Append("</body></html>");
            return builder.ToString();
            //合并
            //附加css

        }
        /// <summary>
        /// 移除自带标签属性
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static string RemoveAttributes(string html)
        {
            html = Regex.Replace(html, @"<.* style=['|""].*(font[\s]*-[\s]*size.*)['|""]>", (m) =>
            {
                return m.Groups[0].Value.Replace(m.Groups[1].Value, "");
            }, RegexOptions.IgnoreCase);  //移除所有font-size

            html = Regex.Replace(html, "<pre[^>]+>([^<]+)</pre>", (m) =>
            {
                if (string.IsNullOrEmpty(m.Groups[1].Value.Trim()))
                {
                    return "";
                }
                else return m.Groups[0].Value;
            }, RegexOptions.IgnoreCase);  //替换所有img标签 为本地图片
            return html;
        }
    }
}
