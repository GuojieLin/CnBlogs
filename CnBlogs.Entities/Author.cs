using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	6/19/2016 3:56:54 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace CnBlogs.Entities
{
    /// <summary>
    /// 博客作者
    /// </summary>
    public class Author
    {
        #region sample
        //<name>离落</name>
        //<uri>http://www.CnBlogs.com/-867259206/</uri>
        //<avatar>http://pic.CnBlogs.com/face/815903/20151111111106.png</avatar>
        #endregion
        /// <summary>
        /// 作者昵称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 作者首页
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        // <author>
        //     <name>mangues</name>
        //     <uri>http://www.CnBlogs.com/Mangues/</uri>
        //     <avatar>http://pic.CnBlogs.com/face/712144/20150108215754.png</avatar>
        // </author>
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static Author Load(XElement element)
        {
            if (element == null) throw new ArgumentNullException("element");
            Author author = new Author();
            author.Name = element?.Element("name")?.Value;
            author.Uri = element?.Element("uri")?.Value;
            author.Avatar = element?.Element("avatar")?.Value;
            return author;
        }
    }
}
