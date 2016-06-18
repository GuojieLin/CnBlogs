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
    /// 博客
    /// </summary>
    internal class CnBlog
    {
        public int Index { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string PublishTime { get; set; }
        public string UpdateTime { get; set; }
        public Author Author { get; set; } 
        public string BlogRawUrl { get; set; }
        public string BlogApp { get; set; }
        public string DiggCount { get; set; }
        public string ViewCount { get; set; }
        public string CommentCount { get; set; }

        public CnBlog()
        {
        }
        public CnBlog(XContainer entry)
        {
            this.Id = entry.Element("id").Value;
            this.Title = entry.Element("title").Value;
            this.Summary = entry.Element("summary").Value;
            this.PublishTime = entry.Element("published").Value;
            this.UpdateTime = entry.Element("updated").Value;
            XElement author = entry.Element("author");
            if(author != null )
            {
                this.Author = new Author
                {
                    Name = author.Element("name").Value,
                    Uri = author.Element("uri").Value,
                    Avatar = author.Element("avatar").Value
                };
            }
            if (string.IsNullOrEmpty(this.Author.Avatar)) this.Author.Avatar = BlogsConstant.DefaultUserImageUrl;
            this.BlogRawUrl = entry.Element("link").Attribute("href").Value;
            this.BlogApp = entry.Element("blogapp").Value;
            this.DiggCount = entry.Element("diggs").Value;
            this.ViewCount = entry.Element("views").Value;
            this.CommentCount = entry.Element("comments").Value;
        }
        
    }
    public class Author
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Avatar { get; set; }
    }
}
