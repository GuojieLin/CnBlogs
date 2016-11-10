using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	6/19/2016 3:44:55 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace CnBlogs.Entities
{
    //  <entry>
    //    <id>5598234</id>
    //    <title type = "text" > Android课程表的设计开发 </ title >
    //    < summary type="text">实现了教务系统中课程的导入，分类显示课程。学期的修改，增加，修改。课程按照周的显示。课程修改上课星期和上课周。上课课程的自动归类。</summary>
    //    <published>2016-06-19T16:12:00+08:00</published>
    //    <updated>2016-06-19T08:13:19Z</updated>
    //    <author>
    //        <name>mangues</name>
    //        <uri>http://www.CnBlogs.com/Mangues/</uri>
    //        <avatar>http://pic.CnBlogs.com/face/712144/20150108215754.png</avatar>
    //    </author>
    //    <link rel = "alternate" href="http://www.CnBlogs.com/Mangues/p/5598234.html"/>
    //    <blogapp>Mangues</blogapp>
    //    <diggs>0</diggs>
    //    <views>1</views>
    //    <comments>0</comments>
    //</entry>
    /// <summary>
    /// 博客
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime Published { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime Updated { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public Author Author { get; set; }
        /// <summary>
        /// 博客内容
        /// <string>String content</string>
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 文章地址
        /// </summary>
        public string BlogUrl { get; set; }
        /// <summary>
        /// 博客用户？？
        /// </summary>
        public string BlogApp { get; set; }
        /// <summary>
        /// 推荐数
        /// </summary>
        public int Diggs { get; set; }
        /// <summary>
        /// 阅读数
        /// </summary>
        public int Views { get; set; }
        /// <summary>
        /// 评论数
        /// </summary>
        public int Comments { get; set; }

        /// <summary>
        ///  <entry>
        //    <id>5598234</id>
        //    <title type = "text" > Android课程表的设计开发 </ title >
        //    <summary type="text">实现了教务系统中课程的导入，分类显示课程。学期的修改，增加，修改。课程按照周的显示。课程修改上课星期和上课周。上课课程的自动归类。</summary>
        //    <published>2016-06-19T16:12:00+08:00</published>
        //    <updated>2016-06-19T08:25:59Z</updated>
        //    <author>
        //        <name>mangues</name>
        //        <uri>http://www.CnBlogs.com/Mangues/</uri>
        //        <avatar>http://pic.CnBlogs.com/face/712144/20150108215754.png</avatar>
        //    </author>
        //    <link rel = "alternate" href="http://www.CnBlogs.com/Mangues/p/5598234.html"/>
        //    <blogapp>Mangues</blogapp>
        //    <diggs>0</diggs>
        //    <views>15</views>
        //    <comments>0</comments>
        //</entry>
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static Blog Load(XElement element)
        {
            if (element == null) throw new ArgumentNullException("element");
            Blog blog = new Blog();
            blog.Id = element?.Element("id")?.Value;
            blog.Title = element?.Element("title")?.Value;
            blog.Summary = element?.Element("summary")?.Value;
            blog.Published = Convert.ToDateTime(element?.Element("published")?.Value);
            blog.Updated = Convert.ToDateTime(element?.Element("updated")?.Value);
            blog.Author = Author.Load(element?.Element("author"));
            blog.BlogUrl = element?.Element("link")?.Attribute("href")?.Value;
            blog.BlogApp = element?.Element("blogapp")?.Value;
            blog.Diggs = Convert.ToInt32(element?.Element("diggs")?.Value);
            blog.Views = Convert.ToInt32(element?.Element("views")?.Value);
            blog.Comments = Convert.ToInt32(element?.Element("comments")?.Value);
            return blog;
        }
    }
}
