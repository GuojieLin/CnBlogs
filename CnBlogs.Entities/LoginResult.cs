using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Entities
{
    [DataContract]
    public class LoginResult
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
