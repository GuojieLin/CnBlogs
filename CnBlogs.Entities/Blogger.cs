using CnBlogs.Core.Constants;
using CnBlogs.Core.Extentsions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CnBlogs.Entities
{
    [DataContract(Name = "Users")]
    /// <div id="profile_block">昵称：<a href="http://home.cnblogs.com/u/Mangues/">mangues</a><br/>园龄：<a href="http
    //://home.cnblogs.com/u/Mangues/" title="入园时间：2015-01-08">1年9个月</a><br/>粉丝：<a href="http://home.cnblogs
    //.com/u/Mangues/followers/">4</a><br/>关注：<a href="http://home.cnblogs.com/u/Mangues/followees/">1</a>
    //<div id = "p_b_follow" ></ div >< script > getFollowStatus('b5f14557-3d97-e411-b908-9dcfd8948a71') </ script ><
    /// div >
    /// </summary>
    public class Blogger
    {
        public int Index { get; set; }
        [DataMember(Name = "uid")]
        public string Guid { get; set; }
        [DataMember(Name="DisplayName")]
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }
        [DataMember]
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        [DataMember(Name = "Alisa")]
        public string BlogApp { get; set; }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FollowerAmount { get; set; }
        /// <summary>
        /// 关注数
        /// </summary>
        public int FolloweeAmount { get; set; }
        public DateTime RegiestDate { get; set; }
        [DataMember]
        /// <summary>
        /// 头像
        /// </summary>
        public string IconName { get; set; }
        [DataMember(Name = "isFollowes")]
        public bool IsFlowee { get; set; }
        [DataMember(Name = "groupId")]
        public string GroupId { get; set; }
        /// <summary>
        /// 是否是推荐博客
        /// </summary>
        public bool IsRecomment { get; set; }
        /// <summary>
        /// 主页地址
        /// </summary>
        public string BloggerHome { get; set; }
        public DateTime Updated { get; set; }
        public int PostCount { get; set; }

        public Blogger()
        {
            GroupId = "00000000-0000-0000-0000-000000000000";
        }
        public static Blogger Load(XElement element, int index)
        {
            if (element == null) throw new ArgumentNullException("element");
            Blogger blogger = new Blogger();
            blogger.Index = index;
            //blogger.Id = element.Element("id")?.Value;
            blogger.Name = element.Element("title")?.Value;
            blogger.Updated = Convert.ToDateTime(element?.Element("updated")?.Value);
            blogger.BloggerHome = element?.Element("link")?.Attribute("href")?.Value;
            blogger.BlogApp = element.Element("blogapp")?.Value;
            blogger.IconName = element?.Element("avatar")?.Value;
            if (blogger.IconName.IsNullOrEmpty()) blogger.IconName = Configuration.DefalutImagePath;
            blogger.PostCount = Convert.ToInt32(element?.Element("postcount")?.Value);
            blogger.IsRecomment = true;
            return blogger;
        }
    }
}
