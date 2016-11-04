using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Entities
{
    /// <summary>
    /// https://news.cnblogs.com/Comment/InsertComment
    /// {"ContentID":556421,"Content":"真会炒作啊","strComment":"","parentCommentId":"350211","title":"<a href=\"
//news.cnblogs.com/n/556421/\">阿里总部惊现高颜值妹子晒被子：为双11加班准备</a>"}
    /// </summary>
    [DataContract]
    public class PostNewsComment
    {
        [DataMember(Name = "ContentID")]
        public string ContentId { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember(Name = "StrComment")]
        public string StrComment { get; set; }
        [DataMember(Name = "parentCommentId")]
        public string ParentCommentId { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}
