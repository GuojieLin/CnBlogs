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
using CnBlogs.Common;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	6/19/2016 11:10:43 PM			//
//			创建日期:	2016				            //
//======================================================//
namespace CnBlogs.ViewModels
{
    internal class BloggerHomeViewModel : BaseViewModel<Blog>
    {
        public string Alias { get; set; }
        public string BlogApp { get; set; }
        public DateTime BlogRegistDate { get; set; }
        /// <summary>
        /// 关注数量
        /// </summary>
        public int FollowerAmount { get; set; }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FolloweeAmount { get; set; }
        public int BlogAmount { get; set; }
        public string Photo { get; set; }
        public string Id { get;set;}
        public BloggerHomeViewModel(Blogger blogger):base()
        {
            this.Id = blogger.Guid;
            this.BlogApp = blogger.BlogApp;
            this.FollowerAmount = blogger.FollowerAmount;
            this.FolloweeAmount = blogger.FolloweeAmount;
            this.BlogRegistDate = blogger.RegiestDate;
            this.Photo = blogger.IconName;
            if (string.IsNullOrEmpty(Photo)) this.Photo = Constants.DefaultAvatar;
            this.Alias = blogger.Name;
        }
        protected override async Task<IList<Blog>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            _isLoading = true;
            var actualCount = 0;
            List<Blog> blogs = null;
            try
            {
                blogs = await BlogService.GetPersonalBlogsAsync(BlogApp, _currentPage, _pageSize);
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
