using CnBlogs.Common;
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
        public async static Task<List<News>> GetHotNewsDataArticlesAsync(int count)
        {
            string url = string.Format(WcfApiUrlConstants.HotNewsData, count);
            return await GetNewsArticlesAsync(url);
        }
        /// <summary>
        /// 分页获取首页新闻
        /// </summary>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async static Task<List<News>> GetRecentRecommendNewsByPagingArticlesAsync(int pageIndex, int pageSize)
        {
            string url = string.Format(WcfApiUrlConstants.RecentRecommendNewsByPaging, pageIndex, pageSize);
            return await GetNewsArticlesAsync(url);
        }
        /// <summary>
        /// 分页获取首页新闻
        /// </summary>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async static Task<List<News>> GetSiteHomeArticlesAsync(int pageIndex, int pageSize)
        {
            string url = string.Format(WcfApiUrlConstants.RecentNewsByPaging, pageIndex, pageSize);
            return await GetNewsArticlesAsync(url);
        }

        public async static Task<List<News>> GetNewsArticlesAsync(string url)
        {
            try
            {
                string xml = await HttpHelper.GetAsync(url);
                List<News> newses = new List<News>();
                xml = xml.Replace(Constants.XmlNameSpace, "");
                XElement xElement = XElement.Parse(xml);
                foreach (XElement entry in xElement.Elements("entry"))
                {
                    News news = News.Load(entry);
                    newses.Add(news);
                }
                return newses;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
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
                string xml = await HttpHelper.GetAsync(url);
                xml = xml.Replace(Constants.XmlNameSpace, "");
                XElement xElement = XElement.Parse(xml);
                return NewsBody.Load(xElement);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
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
                string xml = await HttpHelper.GetAsync(url);
                List<BlogComment> blogComments = new List<BlogComment>();
                xml = xml.Replace(Constants.XmlNameSpace, "");
                XElement xElement = XElement.Parse(xml);
                int i = 0;
                foreach (XElement entry in xElement.Elements("entry"))
                {
                    BlogComment blogComment = BlogComment.Load(entry, i++);
                    blogComments.Add(blogComment);
                }
                return blogComments;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
                return null;
            }
        }

        public async static Task<PostResult> PostCommentAsync(PostNewsComment postNewsComment)
        {
            try
            {
                string data = JsonSerializeHelper.Serialize(postNewsComment);
                string json = await HttpHelper.Post(WcfApiUrlConstants.PostBlogComment, data, CacheManager.LoginUserInfo.Cookies);
                PostResult response = JsonSerializeHelper.Deserialize<PostResult>(json);
                return response;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
                return new PostResult() { IsSuccess = false, Message = "提交时发送异常" };
            }
        }
        public async static Task<PostResult> PostNewsVoteAsync(VoteNews voteNews)
        {
            try
            {
                string data = JsonSerializeHelper.Serialize(voteNews);
                string json = await HttpHelper.Post(WcfApiUrlConstants.VoteNews, data, CacheManager.LoginUserInfo.Cookies);
                PostResult response = JsonSerializeHelper.Deserialize<PostResult>(json);
                return response;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
                return new PostResult() { IsSuccess = false, Message = "提交时发送异常" };
            }
        }
        public async static Task<PostResult> PostNewsCommentVoteAsync(VoteNewsComment voteNewsComment)
        {
            try
            {
                string data = JsonSerializeHelper.Serialize(voteNewsComment);
                string json = await HttpHelper.Post(WcfApiUrlConstants.VoteNewsComment, data, CacheManager.LoginUserInfo.Cookies);
                PostResult response = JsonSerializeHelper.Deserialize<PostResult>(json);
                return response;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
                return new PostResult() { IsSuccess = false, Message = "提交时发送异常" };
            }
        }
    }
}
