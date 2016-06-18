using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.UWP.Constants
{
    internal class UserConstant
    {
        internal const string CurrentUserInfo = "http://home.cnblogs.com/ajax/user/CurrentIngUserInfo";
        internal const string UserInfo = "http://home.cnblogs.com/u/{0}/";
        internal const string UserFollowers = "http://home.cnblogs.com/u/{0}/followers/{1}/";
        internal const string UserFollowees = "http://home.cnblogs.com/u/{0}/followees/{1}/";
        internal const string InboxMessages = "http://msg.cnblogs.com/mobile/inbox/{0}";
        internal const string OutboxMessages = "http://msg.cnblogs.com/mobile/outbox/{0}";
        internal const string MessagesItems = "http://msg.cnblogs.com/mobile/item/{0}";
        internal const string Collections = "http://wz.cnblogs.com/my/{0}.html";
        internal const string DiggBlog = "http://www.cnblogs.com/mvc/vote/VoteBlogPost.aspx";
        internal const string DiggNews = "http://news.cnblogs.com/News/VoteNews";
        internal const string FollowUser = "http://home.cnblogs.com/ajax/follow/FollowUser";
        internal const string Sendmsg = "http://msg.cnblogs.com/ajax/msg/send";
        internal const string Replymsg = "http://msg.cnblogs.com/ajax/msg/reply";
        internal const string AddBlogComment = "http://www.cnblogs.com/mvc/PostComment/Add.aspx";
        internal const string AddNewsComment = "http://news.cnblogs.com/Comment/InsertComment";
        internal const string GetWztags = "http://wz.cnblogs.com/ajax/wz/GetUserTags";
        internal const string AddWz = "http://wz.cnblogs.com/ajax/wz/AddWzlink";

    }
}
