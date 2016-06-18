using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CnBlogs.UWP.Constants;

namespace CNBlogs.UWP.Models
{
    /// <summary>
    /// 博主
    /// </summary>
    internal class CnBloger
    {
        public int Index { get; set; }
        public string BlogerName { get; set; }
        public string BlogApp { get; set; }
        public string BlogerHome { get; set; }
        public string UpdateTime { get; set; }
        public string BlogerAvator { get; set; }
        public string PostCount { get; set; }
        public CnBloger()
        {
        }
        //<entry>
        //    <id>5186332</id>
        //    <title type = "text" > 程序员狂想曲 </ title >
        //    < summary type="text">【前序】博客园首页对博文的规定有：原创精品、排版整齐、有足够的篇幅、与程序员相关、能够让读者从中学到知识的基本要求。我心想，除非不同时空，否则这绝对是我原创的；段落分明排版自然问题不大；一千三百来字的文章远超八百字的高考作文篇幅的要求；出于程序员手里的，难道这还不跟程序员相关吗；能给读者带来情感波动</summary>
        //    <published>2016-02-11T10:27:00+08:00</published>
        //    <updated>2016-02-11T02:58:45Z</updated>
        //    <author>
        //        <name>wc的一些事一些情</name>
        //        <uri>http://www.cnblogs.com/wcd144140/</uri>
        //        <avatar/>
        //    </author>
        //    <link rel = "alternate" href="http://www.cnblogs.com/wcd144140/p/5186332.html"/>
        //    <blogapp>wcd144140</blogapp>
        //    <diggs>0</diggs>
        //    <views>22</views>
        //    <comments>0</comments>
        //</entry>

        public CnBloger(XContainer entry)
        {
            this.Index = Convert.ToInt32(entry.Element("index").Value);
            this.BlogerName = entry.Element("blogerName").Value;
            this.BlogApp = entry.Element("blogApp").Value;
            this.BlogerHome = entry.Element("blogerHome").Value;
            this.UpdateTime = entry.Element("updated").Value;
            this.BlogerAvator = entry.Element("blogerAvator").Value;
            this.PostCount = entry.Element("postCount").Value;
        }
    }
}
