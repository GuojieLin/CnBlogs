using CnBlogs.Common;
using CnBlogs.Core;
using CnBlogs.Entities;
using CnBlogs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.ViewModels
{
    internal class BlogBodyViewModel
    {
        public Blog Blog { get; private set; }
        public SettingManager SettingManager { get; private set; }
        public AppDomain AppDomain { get; private set; }
        internal BlogBodyViewModel(Blog blog)
        {
            SettingManager = SettingManager.Current;
            AppDomain = AppDomain.Current;
            Blog = blog;
        }
        internal async Task Init()
        {
            //加载内容
            await LoadBlogBody();
        }
        internal async Task LoadBlogBody()
        {
            var body = await BlogService.GetBlogBodyAsync(Blog.Id);
            this.Blog.Body = OptimizationDisplayHelper.OptimizationHtmlDisplay(body);
        }

    }
}
