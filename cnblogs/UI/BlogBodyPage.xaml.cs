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
using System.Windows.Input;
using System.Xml.Linq;
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
    public sealed partial class BlogBodyPage : Page
    {
        internal BlogBodyViewModel BlogBodyViewModel;

        public BlogBodyPage()
        {
            this.InitializeComponent();
            //NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                BlogBodyViewModel = new BlogBodyViewModel(e.Parameter as Blog);
                DataLoading();
                await BlogBodyViewModel.LoadBlogBody();
                BlogBodyWebView.NavigateToString(BlogBodyViewModel.Blog.Body);
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


        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            App.NavigationService.TertiaryFrameNavigate(typeof(BlogCommentListPage), this.BlogBodyViewModel.Blog);
        }

        private void BlogsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private async void ShareButton_Click(object sender, RoutedEventArgs e)
        {
            ShareDialog.Default.Init(this.BlogBodyViewModel.Blog.BlogUrl, this.BlogBodyViewModel.Blog.Title);
            ContentDialogResult result = await ShareDialog.Default.ShowAsync();
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

        private void NightModeButton_Click(object sender, RoutedEventArgs e)
        {
            ElementTheme theme = ElementTheme.Default;
            if (this.BlogBodyViewModel.SettingManager.Theme == ElementTheme.Dark)
                theme = ElementTheme.Light;
            else if (this.BlogBodyViewModel.SettingManager.Theme == ElementTheme.Light)
                theme = ElementTheme.Dark;
            this.BlogBodyViewModel.SettingManager.UpdateTheme(theme);
        }

        private async void NavigateBrowserButton_Click(object sender, RoutedEventArgs e)
        {
            // The URI to launch
            var blogUri = new Uri(BlogBodyViewModel.Blog.BlogUrl);
            // Launch the URI
            var success = await Windows.System.Launcher.LaunchUriAsync(blogUri);
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
