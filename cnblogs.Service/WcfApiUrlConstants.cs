using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	6/19/2016 3:02:04 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace CnBlogs.Service
{
    internal class WcfApiUrlConstants
    {
        /// <summary>
        /// 博客园wcf
        /// </summary>
        internal const string CnblogsWcfBaseUrl = "http://wcf.open.CnBlogs.com/";
        #region http://wcf.open.CnBlogs.com/blog/
        internal const string CnblogsWcfBlogUrl = CnblogsWcfBaseUrl + "blog/";
        /// <summary>
        /// itemCount
        /// 48小时阅读排行
        /// </summary>
        internal const string FortyEightHoursTopViewPosts = CnblogsWcfBlogUrl + "48HoursTopViewPosts/{0}";
        /// <summary>
        /// pageIndex,pageSize
        /// 分页获取推荐博客列表
        /// </summary>
        internal const string RecommendedBlogs = CnblogsWcfBlogUrl + "bloggers/recommend/{0}/{1}";
        /// <summary>
        /// 获取推荐博客总数
        /// </summary>
        internal const string RecommendedBlogAmount = CnblogsWcfBlogUrl + "bloggers/recommend/count";
        /// <summary>
        /// 根据作者名搜索博主
        /// </summary>
        internal const string SearchBloggerByAuthorName = CnblogsWcfBlogUrl + "bloggers/search";
        /// <summary>
        /// postId,pageIndex,pageSize
        /// 获取文章评论
        /// </summary>
        internal const string GetBlogComments = CnblogsWcfBlogUrl + "post/{0}/comments/{1}/{2}";
        /// <summary>
        /// postId
        /// 获取文章内容
        /// </summary>
        internal const string GetBlogBody = CnblogsWcfBlogUrl + "post/body/{0}";
        /// <summary>
        /// pageIndex,pageSize 
        /// 分页获取首页文章列表
        /// </summary>
        internal const string SiteHomeArticles = CnblogsWcfBlogUrl + "sitehome/paged/{0}/{1}";
        /// <summary>
        /// 评论
        /// </summary>
        internal const string PostBlogComment = "http://www.cnblogs.com/mvc/PostComment/Add.aspx";
        /// <summary>
        /// 投票博客
        /// {"blogApp":"savorboard","postId":6029984,"voteType":"Digg","isAbandoned":false}
        /// {"blogApp":"savorboard","postId":6029984,"voteType":"Bury","isAbandoned":false}
        /// {"Id":0,"IsSuccess":true,"Message":"推荐成功","Data":null}
        /// {"Id":0,"IsSuccess":false,"Message":"您已经推荐过","Data":null}
        /// </summary>
        internal const string VoteBlogPost = "http://www.cnblogs.com/mvc/vote/VoteBlogPost.aspx";
        /// <summary>
        /// {"commentId":3548659,"voteType":"Digg"}
        /// {"commentId":3548659,"voteType":"Bury"}
        /// {"Id":0,"IsSuccess":false,"Message":"不能推荐自己的内容","Data":null}
        /// {"Id":0,"IsSuccess":false,"Message":"不能反对自己的内容","Data":null}
        /// </summary>
        internal const string VoteBlogComment = "http://www.cnblogs.com/mvc/vote/VoteComment.aspx";
        ///// <summary>
        /////
        ///// itemcount 
        ///// 获取首页文章列表
        ///// </summary>
        //internal const string SiteHomeRecentArticles = CnblogsWcfBlogUrl + "sitehome/paged/{0}";
        /// <summary>
        /// itemcount 
        /// 10天内推荐排行
        /// </summary>
        internal const string TenDaysTopDiggPosts = CnblogsWcfBlogUrl + "TenDaysTopDiggPosts/{0}";
        /// <summary>
        /// blogapp,pageIndex,pageSize 
        /// 分页获取个人博客文章列表
        /// </summary>
        internal const string PersonalBlogs = CnblogsWcfBlogUrl + "u/{blogapp}/posts/{pageIndex}/{pageSize}";
        #endregion
        #region http://wcf.open.CnBlogs.com/news/
        internal const string CnblogsWcfNewsUrl = CnblogsWcfBaseUrl + "news/";
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        internal const string NewsData = CnblogsWcfNewsUrl + "GetData";
        /// <summary>
        /// itemcount
        /// 获取热门新闻列表
        /// </summary>
        internal const string HotNewsData = CnblogsWcfNewsUrl + "hot/{0}";
        /// <summary>
        /// contentId
        /// 获取新闻内容
        /// </summary>
        internal const string NewsContent = CnblogsWcfNewsUrl + "item/{0}";
        /// <summary>
        /// contentId,pageIndex,pageSize
        /// 获取新闻评论
        /// </summary>
        internal const string GetNewsComment = CnblogsWcfNewsUrl + "item/{0}/comments/{1}/{2}";
        /// <summary>
        /// pageIndex,pageSize
        /// 分页获取最新新闻列表
        /// </summary>
        internal const string RecentNewsByPaging = CnblogsWcfNewsUrl + "recent/paged/{0}/{1}";
        /// <summary>
        /// pageIndex,pageSize
        /// 分页获取推荐新闻列表
        /// </summary>
        internal const string RecentRecommendNewsByPaging = CnblogsWcfNewsUrl + "recommend/paged/{0}/{1}";
        /// <summary>
        /// 投票新闻
        /// {"contentId":556505,"commentId":350423,"action":"agree"}
        /// {"contentId":556505,"commentId":350423,"action":"anti"}
        /// {"IsSucceed":true,"Message":"推荐成功"}
        /// {"IsSucceed":false,"Message":"您已经推荐过，不能再反对"}
        /// </summary>
        internal const string VoteNewsComment = "https://news.cnblogs.com/Comment/VoteNewsComment";

        /// <summary>
        /// {"contentId":556505,"action":"agree"}
        /// {"IsSucceed":true,"Message":"推荐成功"}
        /// </summary>
        internal const string VoteNews = "https://news.cnblogs.com/News/VoteNews";
        #endregion
    }
}
