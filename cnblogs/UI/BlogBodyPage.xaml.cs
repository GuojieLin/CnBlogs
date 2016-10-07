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
        public Blog Blog { get; private set; }

        public BlogBodyPage()
        {
            this.InitializeComponent();
            //NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Blog = e.Parameter as Blog;
            DataLoading();
            var body = await BlogService.GetBlogBodyAsync(Blog.Id);
            Blog.Body = OptimizationDisplayHelper.OptimizationHtmlDisplay(body); ;
            BlogBodyWebView.NavigateToString(Blog.Body);
            //BlogCommentViewModel = new BlogCommentViewModel(Blog);
            //BlogCommentViewModel.Refresh();
            //if (blog_body != null)
            //{
            //    if (App.Theme == ApplicationTheme.Dark)  //暗主题
            //    {
            //        blog_body += "<style>body{background-color:black;color:white;}</style>";
            //    }
            //    BlogContent.NavigateToString(blog_body);
            //}
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

        private void DiggsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            App.NavigationService.TertiaryFrameNavigate(typeof(BlogCommentListPage), this.Blog);
        }

        private void BlogsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private async void ShareButton_Click(object sender, RoutedEventArgs e)
        {
            ShareDialog.Default.Init(Blog.BlogUrl, Blog.Title);
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
    }
}
