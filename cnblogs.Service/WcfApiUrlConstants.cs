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
    public class WcfApiUrlConstants
    {
        public const string BaseUrl = "https://www.cnblogs.com/";
        public const string HomeUrl = "https://home.cnblogs.com/";
        public const string BaseLoginUrl = "https://passport.cnblogs.com/user/signin";
        public const string LoginUrl = "https://passport.cnblogs.com/user/signin";
        
        public const string SetAccount = "https://home.cnblogs.com/set/account/";
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
        /// 根据作者名搜索博主 http://wcf.open.cnblogs.com/blog/bloggers/search?t={TERM} 
        /// </summary>
        internal const string SearchBloggerByAuthorName = CnblogsWcfBlogUrl + "bloggers/search?t={0}";
        /// <summary>
        /// postId,pageIndex,pageSize
        /// 获取文章评论
        /// </summary>
        internal const string GetBlogComments = CnblogsWcfBlogUrl + "post/{0}/comments/{1}/{2}";
        //http://www.cnblogs.com/mvc/blog/GetComments.aspx?postId=6029984&blogApp=savorboard&pageIndex=0&anchorCommentId=0&_=1478444141047
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
        /// <summary>
        /// 博客中获取用户信息，可以提取当前博客的反对数量
        /// </summary>
        internal const string BlogPostInfo = "http://www.cnblogs.com/mvc/blog/BlogPostInfo.aspx?blogId=280011&postId=6036786&blogApp=zhangxiongcn&blogUserGuid=b34ebaaa-1b02-e611-9fc1-ac853d9f53cc";
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
        internal const string PersonalBlogs = CnblogsWcfBlogUrl + "u/{0}/posts/{1}/{2}";
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

        #region User

        /// <summary>
        /// 登录成功后从这里获取当前用户blogapp
        /// <h1 id="header_user_left">
        //        欢迎你，杰哥很忙
        //</h1>
        //<div id = "header_user_right" >
        //        < a href="/u/Jack-Blog/"><img class="pfs" src="//pic.cnblogs.com/face/sample_face.gif" alt=""
        ///></a>
        //        <a href = "/u/Jack-Blog/" > 杰哥很忙 </ a >
        //            · <a href = "http://www.cnblogs.com/Jack-Blog/" > 我的博客 </ a >
        //        · <a href = "//home.cnblogs.com/set/account/" > 设置 </ a >
        //        · <a href = "javascript:void;" onclick="return logout();">退出</a>
        //</div>
        /// </summary>
        internal const string CurrentUserBlogApp = "https://home.cnblogs.com/user/CurrentUserInfo";
        // 关注      {"uid":"b5f14557-3d97-e411-b908-9dcfd8948a71","groupId":"00000000-0000-0000-0000-000000000000","page"
        //:1,"isFollowes":true}
        // 粉丝       {"uid":"b5f14557-3d97-e411-b908-9dcfd8948a71","groupId":"00000000-0000-0000-0000-000000000000","page"
        //:1,"isFollowes":false}
        //        {"Users":[{"DisplayName":"陌上花开123","Alias":"1002860","Remark":null,"IconName":"//pic.cnblogs.com/face
        ///sample_face.gif"},{"DisplayName":"Happy Day","Alias":"203145","Remark":null,"IconName":"//pic.cnblogs
        //.com/face/sample_face.gif"},{"DisplayName":"哲的小石头","Alias":"726645","Remark":null,"IconName":"//pic.cnblogs
        //.com/face/sample_face.gif"},{"DisplayName":"lulu5858","Alias":"lulu5858","Remark":null,"IconName":"/
        ///pic.cnblogs.com/face/sample_face.gif"}],"Pager":""}
        ///post
        internal const string RelationUsers = "https://home.cnblogs.com/relation_users";

        /// <summary>
        /// 获取用户信息
        /// <div id="profile_block">昵称：<a href="http://home.cnblogs.com/u/Mangues/">mangues</a><br/>园龄：<a href="http
        //://home.cnblogs.com/u/Mangues/" title="入园时间：2015-01-08">1年9个月</a><br/>粉丝：<a href="http://home.cnblogs
        //.com/u/Mangues/followers/">4</a><br/>关注：<a href="http://home.cnblogs.com/u/Mangues/followees/">1</a>
        //<div id = "p_b_follow" ></ div >< script > getFollowStatus('b5f14557-3d97-e411-b908-9dcfd8948a71') </ script ><
        /// div >
        /// </summary>
        internal const string UserInfo = "http://www.cnblogs.com/mvc/blog/news.aspx?blogApp={0}";
        //http://www.cnblogs.com/mvc/blog/BlogPostInfo.aspx?blogId=114902&postId=4605212&blogApp=yanweidie&blogUserGuid=91ae0150-efe9-e011-8ee0-842b2b196315&_=1478882037590
        //写博客情况
        //http://www.cnblogs.com/mvc/blog/calendar.aspx?blogApp=Jack-Blog&dateStr=
        //标签，最新随便，评论等信息http://www.cnblogs.com/Jack-Blog/mvc/blog/sidecolumn.aspx?blogApp=Jack-Blog
        #endregion
        #region 博问

        internal const string GetUnReadFeedCount = "https://q.cnblogs.com/list/GetUnReadFeedCount";
        internal const string GetUnReadMsgCount = "https://q.cnblogs.com/common/GetUnReadMsgCount";
        /// <summary>
        /// 我的排行
        /// </summary>
        internal const string myrank = "https://q.cnblogs.com/u/myrank?_=1478881407605";
        /// <summary>
        /// 热门标签
        /// </summary>
        internal const string hottag = "https://q.cnblogs.com/tag/hottag";
        /// <summary>
        /// 本周活跃用户
        /// </summary>
        internal const string Weekexpert = "https://q.cnblogs.com/list/Weekexpert";

        /// <summary>
        /// 本周活跃用户
        /// </summary>
        internal const string NewQuest = "https://q.cnblogs.com/q/new";

        internal const string shoucang = "http://wz.cnblogs.com/create?t=TmV3dG9uc29mdC5Kc29u6auY57qn55So5rOVIC0g54Sw5bC+6L+tIC0g5Y2a5a6i5Zut&u=http%3A%2F%2Fwww.cnblogs.com%2Fyanweidie%2Fp%2F4605212.html&c=&bid=4605212&i=0&base64=1";

        #endregion
        #region 其他
        internal const string SearchBlog = "http://zzk.cnblogs.com/s?t=b&w={0}&pageindex={1}";

        internal const string Categories = "https://www.cnblogs.com/aggsite/SubCategories";

        internal const string Home = "https://home.cnblogs.com/";
        #endregion
    }
}
