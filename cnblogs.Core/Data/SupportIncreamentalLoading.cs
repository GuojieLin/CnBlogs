using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	6/20/2016 12:32:55 AM			//
//			创建日期:	2016				            //
//======================================================//
namespace CnBlogs.Core.Data
{
    public abstract class SupportIncreamentalLoading<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        protected bool _busy = false;
        public bool HasMoreItems
        {
            get { return HasMoreItemsOverride(); }
        }


        public delegate void LoadMoreStarted(uint count);
        public delegate void LoadMoreCompleted(uint count);

        public event LoadMoreStarted OnLoadMoreStarted;
        public event LoadMoreCompleted OnLoadMoreCompleted;

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (_busy)
            {
                throw new InvalidOperationException("Only one operation in flight at a time");
            }

            _busy = true;
            return AsyncInfo.Run(c => LoadMoreItemsAsync(c, count));
        }

        protected async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count)
        {
            try
            {
                // 加载开始事件
                this.OnLoadMoreStarted?.Invoke(count);

                var items = await LoadMoreItemsOverrideAsync(c, count);

                uint itemCount = (uint)AddItems(items);

                // 加载完成事件
                this.OnLoadMoreCompleted?.Invoke(itemCount);

                return new LoadMoreItemsResult { Count = itemCount };
            }
            finally
            {
                _busy = false;
            }
        }

        /// <summary>
        /// 将新项目添加进来，之所以是virtual的是为了方便特殊要求，比如不重复之类的
        /// </summary>
        protected virtual int AddItems(IList<T> items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    this.Add(item);
                }
            }
            return items?.Count??0;
        }

        protected abstract Task<IList<T>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count);

        protected abstract bool HasMoreItemsOverride();
    }
}
