using CnBlogs.Core;
using CnBlogs.Core.Extentsions;
using CnBlogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                xml = xml.Replace(Constants.XmlNameSpace,"");
                XElement xElement = XElement.Parse(xml);
                foreach (XElement entry in xElement.Elements("entry"))
                {
                    Blog Blog=  Blog.Load(entry);
                    if (Blog.Author.Avatar.IsNullOrEmpty()) Blog.Author.Avatar = Constants.DefaultAvatar;
                    blogs.Add(Blog);
                }
                return blogs;
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
        public async static Task<string> GetBlogBodyAsync(string id)
        {
            try
            {
                string url = string.Format(WcfApiUrlConstants.GetBody, id);
                string xml = await HttpHelper.Get(url);
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
                string url = string.Format(WcfApiUrlConstants.GetComments, id, pageIndex,pageSize);
                string xml = await HttpHelper.Get(url);
                List<BlogComment> blogComments = new List<BlogComment>();
                XElement xElement = XElement.Parse(xml);
                foreach (XElement entry in xElement.Elements("entry"))
                {
                    BlogComment blogComment= BlogComment.Load(entry);
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
