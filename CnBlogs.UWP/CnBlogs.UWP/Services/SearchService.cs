using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using CnBlogs.UWP.Constants;
using CNBlogs.UWP.Models;

namespace CnBlogs.UWP.Services
{
    /// <summary>
    /// 搜索服务
    /// </summary>
    internal class SearchService
    {
        /// <summary>
        /// 搜索博客
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="page_index"></param>
        /// <returns></returns>
        public async static Task<List<CnBlog>> SearchBlogs(string keywords,int page_index)
        {
            try
            {
                string url = string.Format(SearchConstant.SearchBlogs, keywords, page_index);
                string html = await HttpService.SendGetRequest(url);

                if (html != null)
                {
                    html = html.Split(new string[] { "<div class=\"forflow\">" }, StringSplitOptions.None)[1]
                        .Split(new string[] { "<div class=\"forflow\" id=\"sidebar\">" },StringSplitOptions.None)[0]
                        .Split(new string[] {"<div id=\"paging_block\""},StringSplitOptions.None)[0];
                    html = "<?xml version=\"1.0\" encoding=\"utf - 8\" ?> " + "<result>" + html + "</result>";
                    
                    XElement xElement = XElement.Parse(html);
                    return xElement.Elements("entry")
                        .Select(entry => new CnBlog(entry))
                        .ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 搜索博主
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public async static Task<List<CnBloger>> SearchBloger(string keywords)
        {
            try
            {
                string url = string.Format(SearchConstant.SearchBlogger, keywords);
                string xml = await HttpService.SendGetRequest(url);
                if (xml == null) return null;
                XElement xElement = XElement.Parse(xml);
                return xElement.Elements("entry")
                    .Select(entry => new CnBloger(entry))
                    .ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
