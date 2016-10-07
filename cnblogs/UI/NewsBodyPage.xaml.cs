using CnBlogs.Common;
using CnBlogs.Entities;
using CnBlogs.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class NewsBodyPage : Page
    {
        public News News { get; private set; }

        public NewsBodyPage()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            News = e.Parameter as News;
            DataLoading();
            var body = await NewsService.GetNewsBodyAsync(News.Id);
            News.SetBody(body);
            News.Body.Content = OptimizationDisplayHelper.OptimizationHtmlDisplay(body.Content);
            NewsBodyWebView.NavigateToString(News.Body.Content);

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

        private void ShareButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            App.NavigationService.TertiaryFrameNavigate(typeof(NewsCommentListPage), this.News);
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
    }
}
