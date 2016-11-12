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
    public sealed partial class BlogHomePage : Page
    {
        internal BlogViewModel BlogViewModel;
        internal ReadRankBlogViewModel ReadRankBlogViewModel;
        internal RecommendBlogViewModel RecommendBlogViewModel;
        public BlogHomePage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            BlogViewModel = new BlogViewModel();
            BlogViewModel.OnLoadMoreStarted += count => LoadingProgressRing.IsActive = true;
            BlogViewModel.OnLoadMoreCompleted += count => LoadingProgressRing.IsActive = false;
            ReadRankBlogViewModel = new ReadRankBlogViewModel();
            ReadRankBlogViewModel.OnLoadMoreStarted += count => LoadingProgressRing.IsActive = true;
            ReadRankBlogViewModel.OnLoadMoreCompleted += count => LoadingProgressRing.IsActive = false;
            RecommendBlogViewModel = new RecommendBlogViewModel();
            RecommendBlogViewModel.OnLoadMoreStarted += count => LoadingProgressRing.IsActive = true;
            RecommendBlogViewModel.OnLoadMoreCompleted += count => LoadingProgressRing.IsActive = false;
        }
        private void BlogGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            App.NavigationService.DetailFrameNavigate(typeof(BlogBodyPage), e.ClickedItem);
        }
        private void RefreshBlogListButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshList(rootPivot.SelectedItem);
        }
        private void RefreshList(object selectItem)
        {
            if (selectItem == BlogPivotItem)
            {
                BlogViewModel.Refresh();
            }
            else if (selectItem == ReadRankPivotItem)
            {
                ReadRankBlogViewModel.Refresh();
            }
            else if (selectItem == RecommendBloggerPivotItem)
            {
                RecommendBlogViewModel.Refresh();
            }
        }
        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            RefreshList(rootPivot.SelectedItem);
        }

        private void rootPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rootPivot.SelectedItem == BlogPivotItem)
            {
                if(!BlogViewModel.HadLoading) BlogViewModel.Refresh();
            }
            else if (rootPivot.SelectedItem == ReadRankPivotItem)
            {
                if (!ReadRankBlogViewModel.HadLoading) ReadRankBlogViewModel.Refresh();
            }
            else if (rootPivot.SelectedItem == RecommendBloggerPivotItem)
            {
                if (!RecommendBlogViewModel.HadLoading) RecommendBlogViewModel.Refresh();
            }
        }

        private void RecommendBloggerGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
