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
    internal class NewsBodyViewModel
    {
        public News News { get; private set; }
        public SettingManager SettingManager { get; private set; }
        public AppDomain AppDomain { get; private set; }
        internal NewsBodyViewModel(News news)
        {
            SettingManager = SettingManager.Current;
            AppDomain = AppDomain.Current;
            News = news;
        }
        internal async Task Init()
        {
            //加载内容
            await LoadNewsBody();
        }
        internal async Task LoadNewsBody()
        {
            var body = await NewsService.GetNewsBodyAsync(News.Id);
            News.SetBody(body);
            News.Body.Content = OptimizationDisplayHelper.OptimizationHtmlDisplay(body.Content);
        }

    }
}
