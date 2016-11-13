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
    //  <id>http://www.cnblogs.com/fish-li/</id>
    //  <title type = "text" > Fish Li</title>
    //  <updated>2013-11-18T14:31:32+08:00</updated>
    //  <link rel = "alternate" href="http://www.cnblogs.com/fish-li/" />
    //  <blogapp>fish-li</blogapp>
    //  <avatar>http://pic.cnblogs.com/face/u281816.png?id=28134852</avatar>
    //  <postcount>60</postcount>
    //</entry>
    public class RecommentBlogger
    {
        public int Index { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string BlogApp { get; set; }
        public string BloggerHome { get; set; }
        public DateTime Updated { get; set; }
        public string Avatar { get; set; }
        public int PostCount { get; set; }
        public static RecommentBlogger Load(XElement element,int index )
        {
            if (element == null) throw new ArgumentNullException("element");
            RecommentBlogger blogger = new RecommentBlogger();
            blogger.Index = index;
            blogger.Id = element.Element("id")?.Value;
            blogger.Name = element.Element("title")?.Value;
            blogger.Updated = Convert.ToDateTime(element?.Element("updated")?.Value);
            blogger.BloggerHome = element?.Element("link")?.Attribute("href")?.Value;
            blogger.BlogApp = element.Element("blogapp")?.Value;
            blogger.Avatar = element?.Element("avatar")?.Value;
            if (blogger.Avatar.IsNullOrEmpty()) blogger.Avatar = Configuration.DefalutImagePath;
            blogger.PostCount = Convert.ToInt32(element?.Element("postcount")?.Value);
            return blogger;
        }
    }
}
