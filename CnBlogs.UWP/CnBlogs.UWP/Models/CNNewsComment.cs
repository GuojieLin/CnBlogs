using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.UWP.Models
{
    /// <summary>
    /// 新闻评论
    /// </summary>
    internal class CnNewsComment
    {
        public int Index { get; set; }
        public string Id { get; set; }
        public string NewsId { get; set; }
        public string PublishTime { get; set; }
        public string UpdateTime { get; set; }
        public string AuthorName { get; set; }
        public string AuthorHome { get; set; }
        public string AuthorAvatar { get; set; }
        public string Content { get; set; }
    }
}
