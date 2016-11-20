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
    internal class ReadRankBlogViewModel : BaseViewModel<Blog>
    {

        public ReadRankBlogViewModel():base()
        {
        }
        protected override async Task<IList<Blog>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            _isLoading = true;
            var actualCount = 0;
            if (base.Count > 0)
            {
                _hasMoreItems = false;
                return new List<Blog>();
            }
            List<Blog> blogs = null;
            try
            {
                blogs = await BlogService.GetFortyEightHoursTopViewPostsArticlesAsync(48);
                HadLoading = true;
            }
            catch (Exception exception)
            {

                System.Diagnostics.Debug.WriteLine(exception.Message);
                _hasMoreItems = false;
            }

            if (blogs != null && blogs.Any())
            {
                actualCount = blogs.Count;
                base.AddTotalCount(actualCount);
                _currentPage++;
                _hasMoreItems = true;
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
