using CnBlogs.Common;
using CnBlogs.Entities;
using CnBlogs.Service;
using CnBlogs.UserControls;
using CnBlogs.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class NewsBodyPage : Page
    {
        internal NewsBodyViewModel NewsBodyViewModel;

        public NewsBodyPage()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                NewsBodyViewModel = new NewsBodyViewModel(e.Parameter as News);
                DataLoading();
                await NewsBodyViewModel.LoadNewsBody();
                NewsBodyWebView.NavigateToString(NewsBodyViewModel.News.Body.Content);
            }
            DataLoaded();
            base.OnNavigatedTo(e);
        }
        /// <summary>
        /// 博客列表开始加载
        /// </summary>
        private void DataLoading()
        {
            LoadingProgressRing.IsActive = true;
        }
        /// <summary>
        /// 博客列表加载完毕
        /// </summary>
        private void DataLoaded()
        {
            LoadingProgressRing.IsActive = false;
        }

        private void CommandBarPanel_Opening(object sender, object e)
        {
            CommandBar cb = sender as CommandBar;
            if (cb != null) cb.Background.Opacity = 1.0;
        }

        private void CommandBarPanel_Closing(object sender, object e)
        {
            CommandBar cb = sender as CommandBar;
            if (cb != null) cb.Background.Opacity = 0.5;
        }

        private void DarkModeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void JumpToBrown_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void LikeAppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            VoteNews voteNews= new VoteNews();
            voteNews.VoteType = "agree";// voteType == VoteType.Support ? "agree" : "anti";
            voteNews.Id = NewsBodyViewModel.News.Id;
            var result = await NewsService.PostNewsVoteAsync(voteNews);
            if (!result.IsSuccess)
            {
                MessageDialog messageDialog = new MessageDialog(result.Message);
                await messageDialog.ShowAsync();
            }
            else
            {
                NewsBodyViewModel.News.Diggs++;
                LikeAppBarButton.IsEnabled = false;
            }
        }

        private void CommentAppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.NavigationService.TertiaryFrameNavigate(typeof(NewsCommentListPage), this.NewsBodyViewModel.News);
        }

        private async void ShareAppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ShareDialog.Default.Init(this.NewsBodyViewModel.News.NewsUrl, this.NewsBodyViewModel.News.Title);
            ContentDialogResult result = await ShareDialog.Default.ShowAsync();
        }
    }
}
