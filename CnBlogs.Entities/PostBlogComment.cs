using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Entities
{
    /// <summary>
    /// http://www.cnblogs.com/mvc/PostComment/Add.aspx
    /// {"blogApp":"Jack-Blog","postId":5426956,"body":"test","parentCommentId":0}
    /// </summary>
    [DataContract]
    public class PostBlogComment
    {
        [DataMember(Name = "blogApp")]
        public string BlogApp { get; set; }
        [DataMember(Name = "postId")]
        public string PostId { get; set; }
        [DataMember(Name = "body")]
        public string Body { get; set; }
        [DataMember(Name = "parentCommentId")]
        public string ParentCommentId { get; set; }
    }
    /// <summary>
    /// {"IsSuccess":true,"Message":"\u003cdiv class=\"comment_my_posted\"\u003e\u003cimg style=\"vertical-align
    //:middle\" src=\"http://static.cnblogs.com/images/icon_comment.gif\"/\u003e \u003ca href=\"http://home
    //.cnblogs.com/u/580757/\"\u003e\u003cb\u003e杰哥很忙\u003c/b\u003e\u003c/a\u003e:\u003cblockquote class=\"bq_post_comment
    //\"\u003etest\u003c/blockquote\u003e\u003c/div\u003e","Duration":"93"}
    /// </summary>
    //[DataContract]
    //public class PostBlogCommentResponse
    //{
    //    [DataMember]
    //    public bool IsSuccess { get; set; }
    //    [DataMember]
    //    public string Message { get; set; }
    //    [DataMember]
    //    public int Duration { get; set; }
    //}
}
