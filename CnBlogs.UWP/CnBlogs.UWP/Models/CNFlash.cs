using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.UWP.Models
{
    /// <summary>
    /// 闪存
    /// </summary>
    internal class CnFlash
    {
        public string Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAvator { get; set; }
        public string PublishTime { get; set; }
        public string ReplyCount { get; set; }
        public string FlashContent { get; set; }
    }
}
