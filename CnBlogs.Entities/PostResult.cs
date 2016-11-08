using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Entities
{
    [DataContract]
    public class PostResult
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public bool IsSuccess { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Data { get; set; }
        [DataMember]
        public int Duration { get; set; }
    }
}
