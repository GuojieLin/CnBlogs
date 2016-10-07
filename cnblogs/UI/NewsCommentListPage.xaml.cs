using CnBlogs.Entities;
using CnBlogs.ViewModels;
using CnBlogs.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CnBlogs.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsCommentListPage : Page
    {
        internal NewsCommentViewModel NewsCommentViewModel { get; private set; }
        public NewsCommentListPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            News news = (News)e.Parameter;
            NewsCommentViewModel = new NewsCommentViewModel(news);
            NewsCommentViewModel.OnLoadMoreStarted += count => LoadingProgressRing.IsActive = true;
            NewsCommentViewModel.OnLoadMoreCompleted += count => LoadingProgressRing.IsActive = false;
            NewsCommentViewModel.Refresh();
            //App.NavigationService.DetailFrame.Navigating += (sender, args) => LoadingProgressRing.IsActive = true;
            //App.NavigationService.DetailFrame.Navigated += (sender, args) => LoadingProgressRing.IsActive = false;
            base.OnNavigatedTo(e);
        }
        private void BlogsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //App.NavigationService.DetailFrameNavigate(typeof(BlogBodyPage),e.ClickedItem );
        }
    }
}
