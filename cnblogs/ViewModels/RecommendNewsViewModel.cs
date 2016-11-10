using CnBlogs.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.Foundation;
using CnBlogs.Service;
using System.Threading;
using CnBlogs.Core.Data;
using CnBlogs.Core;
using CnBlogs.Common;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	6/19/2016 11:10:43 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace CnBlogs.ViewModels
{
    internal class RecommendNewsViewModel : BaseViewModel<News>
    {
        public RecommendNewsViewModel():base()
        {
        }
        protected override async Task<IList<News>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            _isLoading = true;
            var actualCount = 0;
            List<News> newes = null;
            try
            {
                newes = await NewsService.GetRecentRecommendNewsByPagingArticlesAsync(_currentPage, _pageSize);
                HadLoading = true;
            }
            catch (Exception)
            {
                _hasMoreItems = false;
            }

            if (newes != null && newes.Any())
            {
                actualCount = newes.Count;
                base.AddTotalCount(actualCount);
                _currentPage++;
                _hasMoreItems = true;
            }
            else
            {
                _hasMoreItems = false;
            }
            _isLoading = false;
            return newes;
        }
        
    }
}
