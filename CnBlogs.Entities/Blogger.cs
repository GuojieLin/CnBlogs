using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
        public int FollowerAmount { get; set; }
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

        public Blogger()
        {
            GroupId = "00000000-0000-0000-0000-000000000000";
        }
    }
}
