using CnBlogs.Common;
using CnBlogs.Core;
using CnBlogs.Core.Extentsions;
using CnBlogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	6/19/2016 3:01:44 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace CnBlogs.Service
{
    public class BlogService
    {
        /// <summary>
        /// 分页获取首页博客
        /// </summary>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async static Task<List<Blog>> GetSiteHomeArticlesAsync(int pageIndex, int pageSize)
        {
            try
            {
                string url = string.Format(WcfApiUrlConstants.SiteHomeArticles, pageIndex, pageSize);
                string xml = await HttpHelper.Get(url);
                List<Blog> blogs = new List<Blog>();
                xml = xml.Replace(Constants.XmlNameSpace, "");//.Replace("&", "");
                xml = RemoveInvalidCharacter(xml);
                XElement xElement = XElement.Parse(xml);
                foreach (XElement entry in xElement.Elements("entry"))
                {
                    Blog Blog=  Blog.Load(entry);
                    if (Blog.Author.Avatar.IsNullOrEmpty()) Blog.Author.Avatar = Constants.DefaultAvatar;
                    blogs.Add(Blog);
                }
                return blogs;
            }
            catch(Exception exception)
            {
                return null;
            }
        }

        private static string HtmlDecode(string xml)
        {
            var matche = Regex.Match(xml, @"&(.*?);");
            while (matche.Success)
            {
                xml = System.Net.WebUtility.HtmlDecode(xml);
                //解码完成继续判断是否仍然需要解码
                matche = Regex.Match(xml, @"&(.*?);");
            }
            return xml;
        }

        private static string RemoveInvalidCharacter(string html)
        {
            //"\v";
            //return html.Replace('\v', ' ');
            return html.Replace("&#xB;", " ");
            //var matches = Regex.Matches(html, @"&(.*?);");
            //foreach (Match match in matches)
            //{
            //    string content = match.Groups[0].Value;
            //    html = html.Replace(content, "");
            //}
            //return html;
        }

        public static async Task<PostBlogCommentResponse> PostCommentAsync(PostBlogComment postBlogComment)
        {
            try
            {
                string data = JsonSerializeHelper.Serialize(postBlogComment);
                string json = await HttpHelper.Post(WcfApiUrlConstants.PostBlogComment, data, CacheManager.LoginUserInfo.Cookies);
                PostBlogCommentResponse response =  JsonSerializeHelper.Deserialize<PostBlogCommentResponse>(json);
                return response;
            }
            catch (Exception exception)
            {
                return new PostBlogCommentResponse() { IsSuccess = false, Message = "提交时发送异常" };
            }
        }

        /// <summary>
        /// 获取文章内容
        /// </summary>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async static Task<string> GetBlogBodyAsync(string id)
        {
            try
            {
                string url = string.Format(WcfApiUrlConstants.GetBlogBody, id);
                string xml = await HttpHelper.Get(url);
                xml = RemoveInvalidCharacter(xml);
                //Match match = Regex.Match(xml, @"string[^>/]*>(?<content>[\s\S]*?)</string>");
                //string content = match.Groups["content"].Value;
                //直接取string节点中的内容
                XElement xElement = XElement.Parse(xml);
                return xElement.Value;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取文章内容
        /// </summary>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async static Task<List<BlogComment>> GetBlogCommentsAsync(string id,int pageIndex,int pageSize)
        {
            try
            {
                string url = string.Format(WcfApiUrlConstants.GetBlogComments, id, pageIndex,pageSize);
                string xml = await HttpHelper.Get(url);
                xml = xml.Replace(Constants.XmlNameSpace, "");//.Replace("&", "");
                xml = RemoveInvalidCharacter(xml);
                List<BlogComment> blogComments = new List<BlogComment>();
                XElement xElement = XElement.Parse(xml);
                int i = 1;
                foreach (XElement entry in xElement.Elements("entry"))
                {
                    BlogComment blogComment = BlogComment.Load(entry, i++);
                    blogComments.Add(blogComment);
                }
                return blogComments;
            }
            catch
            {
                return null;
            }
        }
    }
}
