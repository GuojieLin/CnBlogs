using CnBlogs.ViewModels;
using System;
using System.Collections.Generic;
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
    public sealed partial class NewsListPage : Page
    {
        internal NewsViewModel NewsViewModel;
        public NewsListPage()
        {
            this.InitializeComponent();
            NewsViewModel = new NewsViewModel();
            NewsViewModel.OnLoadMoreStarted += count => LoadingProgressRing.IsActive = true;
            NewsViewModel.OnLoadMoreCompleted += count => LoadingProgressRing.IsActive = false;
            NewsViewModel.Refresh();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.NavigationService.DetailFrame.Navigating += (sender, args) => LoadingProgressRing.IsActive = true;
            App.NavigationService.DetailFrame.Navigated += (sender, args) => LoadingProgressRing.IsActive = false;
            base.OnNavigatedTo(e);
        }
        private void NewsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            App.NavigationService.DetailFrameNavigate(typeof(NewsBodyPage), e.ClickedItem);
        }
        private void RefreshNewsListButton_Click(object sender, RoutedEventArgs e)
        {
            NewsViewModel.Refresh();
        }
    }
}
