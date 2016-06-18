using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.UWP.Constants
{
    #region http://wcf.open.cnblogs.com/blog/
    //48HoursTopViewPosts/{itemCount} 	GET 	48小时阅读排行
    //bloggers/recommend/{pageIndex}/{pageSize} 	GET 	分页获取推荐博客列表
    //bloggers/recommend/count 	GET 	获取推荐博客总数
    //bloggers/search 	GET 	根据作者名搜索博主
    //post/{postId}/comments/{pageIndex}/{pageSize} 	GET 	获取文章评论
    //post/body/{postId} 	GET 	获取文章内容
    //sitehome/paged/{pageIndex}/{pageSize} 	GET 	分页获取首页文章列表
    //sitehome/recent/{itemcount} 	GET 	获取首页文章列表
    //TenDaysTopDiggPosts/{itemCount} 	GET 	10天内推荐排行
    //u/{blogapp}/posts/{pageIndex}/{pageSize} 	GET 	分页获取个人博客文章列表
    #endregion
    internal class BlogsConstant
    {
        /// <summary>
        /// 48小时阅读排行
        /// itemCount
        /// </summary>
        internal const string FortyEightHoursTopViewPosts = "http://wcf.open.cnblogs.com/blog/48HoursTopViewPosts/{0}";  //itemCount
        /// <summary>
        /// 分页获取推荐博客列表
        /// pageIndex,pageSize
        /// </summary>
        internal const string PagingLoadRecommendBloggers = "http://wcf.open.cnblogs.com/blog/bloggers/recommend/{0}/{1}";
        /// <summary>
        /// 分页获取推荐博客列表
        /// pageIndex,pageSize
        /// </summary>
        internal const string GetRecommendBlogsAmount = "http://wcf.open.cnblogs.com/blog/bloggers/recommend/{0}/{1}";
        /// <summary>
        /// 根据作者名搜索博主
        /// </summary>
        internal const string SearchBloggers = "http://wcf.open.cnblogs.com/blog/bloggers/search";
        /// <summary>
        /// 获取文章评论
        /// postId,pageIndex,pageSize
        /// </summary>
        internal const string LoadArticleComment = "http://wcf.open.cnblogs.com/blog/post/{0}/comments/{1}/{2}";
        /// <summary>
        /// 获取文章内容
        /// postId
        /// </summary>
        internal const string LoadArticleContent = "http://wcf.open.cnblogs.com/blog/post/body/{0}";
        /// <summary>
        /// 分页获取首页文章列表
        /// pageIndex,pageSize
        /// </summary>
        internal const string PagingLoadHomeArticleList = "http://wcf.open.cnblogs.com/blog/sitehome/paged/{0}/{1}";
        /// <summary>
        /// 分页获取首页文章列表
        /// pageIndex,pageSize
        /// </summary>
        internal const string PagingLoadHomeArticle = "http://wcf.open.cnblogs.com/blog/sitehome/paged/{0}/{1}"; //page_index page_size
        /// <summary>
        /// 获取首页文章列表
        /// itemCount
        /// </summary>
        internal const string LoadHomeArticleList = "http://wcf.open.cnblogs.com/blog/sitehome/recent/{0}";
        /// <summary>
        /// 10天内推荐排行
        /// itemCount
        /// </summary>
        internal const string TenDaysTopDiggPosts = "http://wcf.open.cnblogs.com/blog/TenDaysTopDiggPosts/{0}";  //item_count
        /// <summary>
        /// 分页获取个人博客文章列表
        /// blogapp(Jack-Blog),pageIndex,pageSize
        /// </summary>
        internal const string PagingLoadPersonalBlogsArticleList = "http://wcf.open.cnblogs.com/blog/u/{0}/posts/{1}/{2}";
        /// <summary>
        /// 默认用户头像
        /// </summary>
        internal const string DefaultUserImageUrl = "http://pic.cnblogs.com/avatar/simple_avatar.gif";
    }
}
