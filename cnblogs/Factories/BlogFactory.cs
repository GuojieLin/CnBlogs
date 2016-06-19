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
using CnBlogs.Factories;
using System.Threading;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	6/19/2016 11:10:43 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace CnBlogs.Factories
{
    internal class BlogFactory : SupportIncreamentalLoading<Blog>
    {
        private bool _isLoading = false;
        private bool _hasMoreItems = false;
        private int _pageSize = 0;
        private int _currentPage = 1;

        public int TotalCount { get; set; }
        public BlogFactory()
        {
            _pageSize = 20;
            _hasMoreItems = true;
        }
        public void Refresh()
        {
            _currentPage = 1;
            TotalCount = 0;
            Clear();
            _hasMoreItems = true;
        }

        protected override async Task<IList<Blog>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            _isLoading = true;
            var actualCount = 0;
            List<Blog> blogs = null;
            try
            {
                blogs = await BlogService.GetSiteHomeArticlesAsync(_currentPage, _pageSize);
            }
            catch (Exception)
            {
                _hasMoreItems = false;
            }

            if (blogs != null && blogs.Any())
            {
                actualCount = blogs.Count;
                TotalCount += actualCount;
                _currentPage++;
                _hasMoreItems = true;
                blogs.ForEach(this.Add);
            }
            else
            {
                _hasMoreItems = false;
            }
            _isLoading = false;
            return blogs;
        }

        protected override bool HasMoreItemsOverride()
        {
            if (_isLoading) return false;
            else return _hasMoreItems;
        }
    }
}
