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
    public sealed partial class NewsHomePage : Page
    {
        internal NewsViewModel NewsViewModel;
        internal HotNewsViewModel HotNewsViewModel;
        internal RecommendNewsViewModel RecommendNewsViewModel;
        public NewsHomePage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            NewsViewModel = new NewsViewModel();
            NewsViewModel.OnLoadMoreStarted += count => LoadingProgressRing.IsActive = true;
            NewsViewModel.OnLoadMoreCompleted += count => LoadingProgressRing.IsActive = false;
            HotNewsViewModel = new HotNewsViewModel();
            HotNewsViewModel.OnLoadMoreStarted += count => LoadingProgressRing.IsActive = true;
            HotNewsViewModel.OnLoadMoreCompleted += count => LoadingProgressRing.IsActive = false;
            RecommendNewsViewModel = new RecommendNewsViewModel();
            RecommendNewsViewModel.OnLoadMoreStarted += count => LoadingProgressRing.IsActive = true;
            RecommendNewsViewModel.OnLoadMoreCompleted += count => LoadingProgressRing.IsActive = false;
        }
        private void NewsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            App.NavigationService.DetailFrameNavigate(typeof(NewsBodyPage), e.ClickedItem);
        }
        private void RefreshNewsListButton_Click(object sender, RoutedEventArgs e)
        {
            NewsViewModel.Refresh();
        }
        
        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            if (rootPivot.SelectedItem == NewsPivotItem)
            {
                NewsViewModel.Refresh();
            }
            else if (rootPivot.SelectedItem == HotNewsPivotItem)
            {
                HotNewsViewModel.Refresh();
            }
            else if (rootPivot.SelectedItem == RecommendNewsPivotItem)
            {
                RecommendNewsViewModel.Refresh();
            }
        }

        private void rootPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rootPivot.SelectedItem == NewsPivotItem)
            {
                if(!NewsViewModel.HadLoading) NewsViewModel.Refresh();
            }
            else if (rootPivot.SelectedItem == HotNewsPivotItem)
            {
                if (!HotNewsViewModel.HadLoading) HotNewsViewModel.Refresh();
            }
            else if (rootPivot.SelectedItem == RecommendNewsPivotItem)
            {
                if (!RecommendNewsViewModel.HadLoading) RecommendNewsViewModel.Refresh();
            }
        }
        
    }
}
