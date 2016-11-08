using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Entities
{
    [DataContract]
    public class VoteBlogComment
    {
        [DataMember(Name ="blogApp")]
        public string BlogApp { get; set; }
        [DataMember(Name = "postId")]
        public string Id { get; set; }
        [DataMember(Name = "voteType")]
        public string VoteType { get; set; }
        [DataMember(Name = "isAbandoned")]
        public string IsAbandoned { get; set; }
    }
}
