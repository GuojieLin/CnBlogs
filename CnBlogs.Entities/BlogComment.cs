using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	6/19/2016 16:07:54 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace CnBlogs.Entities
{
    #region sample
    //<entry>
    //    <id>3454999</id>
    //    <title type = "text" />
    //    < published > 2016 - 06 - 19T16:01:42+08:00</published>
    //    <updated>2016-06-19T08:10:17Z</updated>
    //    <author>
    //        <name>* 平*</name>
    //        <uri>http://home.CnBlogs.com/u/980309/</uri>
    //    </author>
    //    <content type = "text" > 一直使用7z，但没用过命令行，长知识了，感谢楼主分享。</content>
    //</entry>
    #endregion
    /// <summary>
    /// 博客评论
    /// </summary>
    public class BlogComment
    {
        /// <summary>
        /// 当前评论的Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 博客的Id
        /// </summary>
        public string BlogId { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime Published { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime Updated { get; set; }
        /// <summary>
        /// 评论作者
        /// </summary>
        public Author Author{ get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// <entry>
        ///   <id>3454999</id>
        ///   <title type = "text" />
        ///   < published > 2016 - 06 - 19T16:01:42+08:00</published>
        ///   <updated>2016-06-19T09:04:55Z</updated>
        ///   <author>
        ///       <name>*平*</name>
        ///       <uri>http://home.CnBlogs.com/u/980309/</uri>
        ///   </author>
        ///   <content type = "text" > 一直使用7z，但没用过命令行，长知识了，感谢楼主分享。</content>
        /// </entry>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static BlogComment Load(XElement element)
        {
            if (element == null) throw new ArgumentNullException("element");
            BlogComment blogComment = new BlogComment();
            blogComment.Id = element?.Element("id").Value;
            blogComment.Published = Convert.ToDateTime(element?.Element("published").Value);
            blogComment.Updated = Convert.ToDateTime(element?.Element("updated").Value);
            blogComment.Author = Author.Load(element?.Element("author"));
            blogComment.Content = element?.Element("content").Value;
            return blogComment;
        }
    }
}
