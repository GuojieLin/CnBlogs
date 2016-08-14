using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CnBlogs.Entities
{

    //    <NewsBody>
    //	<Title>Google的两项安全警告让Gmail服务变得更加安全</Title>
    //	<SourceName>cnBeta</SourceName>
    //	<SubmitDate>2016-08-13 23:41:00</SubmitDate>
    //	<Content>
    //		<p>借助于两项新的安全警告 Google 让自家邮件服务变得更加安全。Gmail 用户通过网页或者 Android 客户端进行访问的时候，如果接收到尚未认证的信息那么 Sender Policy Framework（SPF）或者 DKIM 就会对发送者的头像、企业 LOGO 或者企业形象代言进行标记，将原本的人物造型转换成为显眼的红色问号。</p>

    //		<p style = "text-align: center;" >

    //            < img title="Unauth Profile Pictures.png" src="//images2015.cnblogs.com/news/66372/201608/66372-20160813234054750-701136153.png" alt="" />
    //		</p>

    //		<p>另外一项调整是，当你在网页上收到一条已经被标记为钓鱼、恶意软件或者垃圾应用的危险网站链接，当你点击这条链接的时候就会看到下面这条警告。自今天开始这些警告将作为安全浏览保护功能的延伸出现在各类浏览器上。全屏的警告页面如下：</p>

    //		<p style = "text-align: center;" >

    //            < img title="Safer Links in Gmail.png" src="//images2015.cnblogs.com/news/66372/201608/66372-20160813234054593-1828163312.png" alt="" />
    //		</p>
    //	</Content>
    //	<ImageUrl>//images2015.cnblogs.com/news/66372/201608/66372-20160813234054750-701136153.png;//images2015.cnblogs.com/news/66372/201608/66372-20160813234054593-1828163312.png</ImageUrl>
    //	<PrevNews>551466</PrevNews>
    //	<NextNews>0</NextNews>
    //	<CommentCount>0</CommentCount>
    //</NewsBody>
    public class NewsBody
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string SourceName { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmitDate { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 显示在最上方的图片
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// PrevNews的id
        /// </summary>
        public string PrevNewsId { get; set; }
        /// <summary>
        /// 下个新闻id
        /// </summary>
        public string NextNewsId { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        public string CommentCount { get; set; }

        public static NewsBody Load(XElement element)
        {
            if (element == null) throw new ArgumentNullException("element");
            NewsBody blog = new NewsBody();
            blog.Title = element?.Element("Title").Value;
            blog.SourceName = element?.Element("SourceName").Value;
            blog.SubmitDate = Convert.ToDateTime(element?.Element("SubmitDate").Value);
            blog.Content = element?.Element("Content").Value;
            blog.ImageUrl = element?.Element("ImageUrl").Value;
            blog.PrevNewsId = element?.Element("PrevNews").Value;
            blog.NextNewsId = element?.Element("NextNews").Value;
            blog.CommentCount = element?.Element("CommentCount").Value;
            return blog;
        }
    }
}
