using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.UWP.Models
{
    /// <summary>
    /// 博客园用户 注意与CNBloger不同
    /// </summary>
    internal class CnUserInfo
    {
        public string BlogApp { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        /// <summary>
        /// 园龄
        /// </summary>
        public string Age { get; set; }
        public string Followers { get; set; }
        public string Followees { get; set; }
        public string BlogHome { get; set; }
        /// <summary>
        /// 用户唯一标示
        /// </summary>
        public string Guid { get; set; }
    }
}
