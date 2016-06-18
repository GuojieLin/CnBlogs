using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.UWP.Constants
{
    //GetData 	GET 	获取新闻列表
    //hot/{itemcount} 	GET 	获取热门新闻列表
    //item/{contentId} 	GET 	获取新闻内容
    //item/{contentId}/comments/{pageIndex}/{pageSize} 	GET 	获取新闻评论
    //recent/{itemcount} 	GET 	获取最新新闻列表
    //recent/paged/{pageIndex}/{pageSize} 	GET 	分页获取最新新闻列表
    //recommend/paged/{pageIndex}/{pageSize} 	GET 	分页获取推荐新闻列表
    internal class NewsConstant
    {
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        internal const string LoadNewsList = "http://wcf.open.cnblogs.com/news/GetData";

        /// <summary>
        /// 获取新闻列表
        /// itemcount
        /// </summary>
        internal const string LoadHotNewsList = "http://wcf.open.cnblogs.com/news/hot/{0}";
        /// <summary>
        /// 获取新闻内容
        /// contentId
        /// </summary>
        internal const string LoadNewsContent = "http://wcf.open.cnblogs.com/news/item/{contentId}";
        /// <summary>
        /// 获取新闻评论
        /// contentId,pageIndex,pageSize
        /// </summary>
        internal const string LoadNewsComment = "http://wcf.open.cnblogs.com/news/item/{0}/comments/{1}/{2}";
        /// <summary>
        /// 获取最新新闻列表
        /// itemcount
        /// </summary>
        internal const string LoadRecentlyNewsList = "http://wcf.open.cnblogs.com/news/recent/{0}";
        /// <summary>
        /// 分页获取最新新闻列表
        /// pageIndex,pageSize
        /// </summary>
        internal const string LoadPagingRecentlyNewsList = "http://wcf.open.cnblogs.com/news/recent/paged/{0}/{1}";
        /// <summary>
        /// 分页获取推荐新闻列表
        /// pageIndex,pageSize
        /// </summary>
        internal const string LoadPagingReCommentNewsList = "http://wcf.open.cnblogs.com/news/recommend/paged/{0}/{1}";


    }
}
