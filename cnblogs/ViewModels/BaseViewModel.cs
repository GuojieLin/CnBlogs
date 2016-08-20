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
    internal class BaseViewModel<T> : SupportIncreamentalLoading<T>
    {
        protected bool _isLoading = false;
        protected bool _hasMoreItems = false;
        protected int _pageSize = 0;
        protected int _currentPage = 1;

        public int TotalCount { get; private set; }
        public BaseViewModel()
        {
            _pageSize = SettingManager.Current.PageSize;
            _hasMoreItems = true;
        }
        public void Refresh()
        {
            _currentPage = 1;
            TotalCount = 0;
            Clear();
            _hasMoreItems = true;
        }

        protected override Task<IList<T>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            throw new NotImplementedException("基类方法未实现");
        }

        protected override bool HasMoreItemsOverride()
        {
            if (_isLoading) return false;
            else return _hasMoreItems;
        }
        protected int AddTotalCount(int count)
        {
            this.TotalCount += count;
            return this.TotalCount;
        }
    }
}
