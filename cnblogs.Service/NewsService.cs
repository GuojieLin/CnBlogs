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
    /// <summary>
    /// 获取新闻服务
    /// </summary>
    public class NewsService
    {
        /// <summary>
        /// 分页获取首页新闻
        /// </summary>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async static Task<List<News>> GetSiteHomeArticlesAsync(int pageIndex, int pageSize)
        {
            try
            {
                string url = string.Format(WcfApiUrlConstants.RecentNewsByPaging, pageIndex, pageSize);
                string xml = await HttpHelper.Get(url);
                List<News> newses = new List<News>();
                xml = xml.Replace(Constants.XmlNameSpace, "").Replace("&", "");
                XElement xElement = XElement.Parse(xml);
                foreach (XElement entry in xElement.Elements("entry"))
                {
                    News news = News.Load(entry);
                    newses.Add(news);
                }
                return newses;
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
        public async static Task<NewsBody> GetNewsBodyAsync(string id)
        {
            try
            {
                string url = string.Format(WcfApiUrlConstants.NewsContent, id);
                string xml = await HttpHelper.Get(url);
                xml = xml.Replace(Constants.XmlNameSpace, "").Replace("&", "");
                XElement xElement = XElement.Parse(xml);
                return NewsBody.Load(xElement);
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
        public async static Task<List<BlogComment>> GetNewsCommentsAsync(string id,int pageIndex,int pageSize)
        {
            try
            {
                string url = string.Format(WcfApiUrlConstants.GetNewsComment, id, pageIndex,pageSize);
                string xml = await HttpHelper.Get(url);
                List<BlogComment> blogComments = new List<BlogComment>();
                xml = xml.Replace(Constants.XmlNameSpace, "").Replace("&", "");
                XElement xElement = XElement.Parse(xml);
                int i = 0;
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
