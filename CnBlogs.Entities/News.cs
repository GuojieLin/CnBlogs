using CnBlogs.Core.Constants;
using CnBlogs.Core.Extentsions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CnBlogs.Entities
{
    //<entry>
    // <id>551467</id>
    // <title type = "text" > Google的两项安全警告让Gmail服务变得更加安全 </ title >
    //    < summary type="text">借助于两项新的安全警告 Google 让自家邮件服务变得更加安全。Gmail 用户通过网页或者 Android 客户端进行访问的时候，如果接收到尚未认证的信息那么 Sender Policy Framework（SPF）或者 DKIM 就会对发送者的头像、企业 LOGO 或者企业形象代言进行标记...</summary>
    // <published>2016-08-13T23:41:00+08:00</published>
    // <updated>2016-08-14T01:27:25Z</updated>
    // <link rel = "alternate" href="http://news.cnblogs.com/n/551467/"/>
    // <diggs>0</diggs>
    // <views>93</views>
    // <comments>0</comments>
    // <topic/>
    // <topicIcon>http://images0.cnblogs.com/news_topic///images0.cnblogs.com/news_topic/gmail.gif</topicIcon>
    // <sourceName>cnBeta</sourceName>
    //</entry>
    public class News
    {

        /// <summary>
        /// id
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
        /// 新闻地址
        /// </summary>
        public string NewsUrl { get; set; }
        public NewsBody Body { get; set; }
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
        // <summary>
        /// 主题
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TopicIcon { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string SourceName { get; set; }
        public void SetBody(NewsBody newsBody)
        {
            this.Body = newsBody;
        }
        public static News Load(XElement element)
        {
            if (element == null) throw new ArgumentNullException("element");
            News blog = new News();
            blog.Id = element?.Element("id").Value;
            blog.Title = element?.Element("title").Value;
            blog.Summary = element?.Element("summary").Value;
            blog.Published = Convert.ToDateTime(element?.Element("published").Value);
            blog.Updated = Convert.ToDateTime(element?.Element("updated").Value);
            blog.NewsUrl = element?.Element("link")?.Attribute("href").Value;
            blog.Diggs = Convert.ToInt32(element?.Element("diggs").Value);
            blog.Views = Convert.ToInt32(element?.Element("views").Value);
            blog.Comments = Convert.ToInt32(element?.Element("comments").Value);
            blog.Topic = element?.Element("topic").Value;
            blog.TopicIcon =  element?.Element("topicIcon").Value;
            if (blog.TopicIcon.IsNullOrEmpty()) blog.TopicIcon = Configuration.DefalutPath;
            int errorIndex= blog.TopicIcon.IndexOf("///");
            if (errorIndex>0)
            {
                //博客园返的新闻图片有误需要把///去除
                //http://images0.cnblogs.com/news_topic///images0.cnblogs.com/news_topic/gmail.gif
                blog.TopicIcon = "http://" + blog.TopicIcon.Substring(errorIndex + 3);
            }
            blog.SourceName = element?.Element("sourceName").Value;
            return blog;
        }
    }

}
