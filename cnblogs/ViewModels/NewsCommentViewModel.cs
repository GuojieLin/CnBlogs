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

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	6/19/2016 11:10:43 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace CnBlogs.ViewModels
{
    internal class NewsCommentViewModel : BaseViewModel<BlogComment>
    {
        public News News { get; private set; }
        public NewsCommentViewModel(News news):base()
        {
            this.News = news;
        }
        protected override async Task<IList<BlogComment>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            _isLoading = true;
            var actualCount = 0;
            List<BlogComment> news = null;
            try
            {
                news = await NewsService.GetNewsCommentsAsync(News.Id, _currentPage, _pageSize);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
                _hasMoreItems = false;
            }

            if (news != null && news.Any())
            {
                actualCount = news.Count;
                base.AddTotalCount(actualCount);
                _currentPage++;
                _hasMoreItems = _pageSize <= actualCount;
            }
            else
            {
                _hasMoreItems = false;
            }
            _isLoading = false;
            return news;
        }

        protected override bool HasMoreItemsOverride()
        {
            if (_isLoading) return false;
            else return _hasMoreItems;
        }
    }
}
