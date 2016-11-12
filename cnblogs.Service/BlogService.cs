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
            string url = string.Format(WcfApiUrlConstants.SiteHomeArticles, pageIndex, pageSize);
            return await GeBlogArticlesAsync(url);
        }
        public async static Task<List<Blog>> GetFortyEightHoursTopViewPostsArticlesAsync(int count)
        {
            string url = string.Format(WcfApiUrlConstants.FortyEightHoursTopViewPosts, count);
            return await GeBlogArticlesAsync(url);
        }
        public async static Task<List<Blogger>> SearchBlogger(string name)
        {
            string url = string.Format(WcfApiUrlConstants.SearchBloggerByAuthorName,name);
            return await GetBloggerAsync(url);
        }
        public async static Task<List<Blog>> SearchBlogg(string content,int page)
        {
            string url = string.Format(WcfApiUrlConstants.SearchBlog, content,page);
            return await GeBlogArticlesAsync(url);
        }
        /// <summary>
        /// 加载推荐博客
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async static Task<List<Blogger>> GetRecommendedBloggerListAsync(int pageIndex, int pageSize)
        {
            string url = string.Format(WcfApiUrlConstants.RecommendedBlogs, pageIndex, pageSize);
            return await GetBloggerAsync(url);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async static Task<List<Blogger>> GetBloggerAsync(string url)
        {
            try
            {
                string xml = await HttpHelper.GetAsync(url);
                List<Blogger> bloggers = new List<Blogger>();
                xml = xml.Replace(Constants.XmlNameSpace, "");//.Replace("&", "");
                xml = RemoveInvalidCharacter(xml);
                XElement xElement = XElement.Parse(xml);
                int i = 1;
                foreach (XElement entry in xElement.Elements("entry"))
                {
                    Blogger blogger = Blogger.Load(entry, i++);
                    bloggers.Add(blogger);
                }
                return bloggers;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async static Task<List<Blog>> GeBlogArticlesAsync(string url)
        {
            try
            {
                string xml = await HttpHelper.GetAsync(url);
                List<Blog> blogs = new List<Blog>();
                xml = xml.Replace(Constants.XmlNameSpace, "");//.Replace("&", "");
                xml = RemoveInvalidCharacter(xml);
                XElement xElement = XElement.Parse(xml);
                foreach (XElement entry in xElement.Elements("entry"))
                {
                    Blog Blog = Blog.Load(entry);
                    if (Blog.Author.Avatar.IsNullOrEmpty()) Blog.Author.Avatar = Constants.DefaultAvatar;
                    blogs.Add(Blog);
                }
                return blogs;
            }
            catch (Exception exception)
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

        public static async Task<PostResult> PostCommentAsync(PostBlogComment postBlogComment)
        {
            try
            {
                string data = JsonSerializeHelper.Serialize(postBlogComment);
                string json = await HttpHelper.Post(WcfApiUrlConstants.PostBlogComment, data, CacheManager.LoginUserInfo.Cookies);
                PostResult response =  JsonSerializeHelper.Deserialize<PostResult>(json);
                return response;
            }
            catch (Exception exception)
            {
                return new PostResult() { IsSuccess = false, Message = "提交时发送异常" };
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
                string xml = await HttpHelper.GetAsync(url);
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
                string xml = await HttpHelper.GetAsync(url);
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

        public async static Task<PostResult> PostBlogVoteAsync(VoteBlog voteBlog)
        {
            try
            {
                string data = JsonSerializeHelper.Serialize(voteBlog);
                string json = await HttpHelper.Post(WcfApiUrlConstants.VoteBlogPost, data, CacheManager.LoginUserInfo.Cookies);
                PostResult response = JsonSerializeHelper.Deserialize<PostResult>(json);
                return response;
            }
            catch (Exception exception)
            {
                return new PostResult() { IsSuccess = false, Message = "提交时发送异常" };
            }
        }
        public async static Task<PostResult> PostBlogCommentVoteAsync(VoteBlogComment voteBlogComment)
        {
            try
            {
                string data = JsonSerializeHelper.Serialize(voteBlogComment);
                string json = await HttpHelper.Post(WcfApiUrlConstants.VoteBlogComment, data, CacheManager.LoginUserInfo.Cookies);
                PostResult response = JsonSerializeHelper.Deserialize<PostResult>(json);
                return response;
            }
            catch (Exception exception)
            {
                return new PostResult() { IsSuccess = false, Message = "提交时发送异常" };
            }
        }
    }
}
