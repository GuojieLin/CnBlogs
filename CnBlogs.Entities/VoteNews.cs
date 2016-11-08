using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Entities
{
    [DataContract]
    public class VoteNews
    {
        [DataMember(Name = "contentId")]
        public string Id { get; set; }
        [DataMember(Name = "commentId")]
        public string CommentId { get; set; }
        [DataMember(Name = "action")]
        public string VoteType { get; set; }
    }
}
