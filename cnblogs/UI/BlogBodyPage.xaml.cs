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
    public sealed partial class BlogBodyPage : Page
    {
        public Blog Blog { get; private set; }

        public BlogBodyPage()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Blog = e.Parameter as Blog;
            DataLoading();
            var body = await BlogService.GetBlogBodyAsync(Blog.Id);
            Blog.Body = OptimizationDisplayHelper.OptimizationHtmlDisplay(body); ;
            BlogBodyWebView.NavigateToString(Blog.Body);

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
        private void FixImage()
        {
            XElement xelement = XElement.Parse(Blog.Body);
            //foreach (XElement imageXElement in xelement.Elements("img"))
            //{
            //    //width="685" height="89" 
            //    int width = Convert.ToInt32(imageXElement.Attribute("width").Value);
            //    int height = Convert.ToInt32(imageXElement.Attribute("height").Value);
            //    imageXElement.Attribute("width").SetValue(BlogBodyWebView.ActualWidth);
            //    imageXElement.Attribute("height").SetValue(BlogBodyWebView.ActualWidth / width * height);

            //}
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

        }
    }
}
