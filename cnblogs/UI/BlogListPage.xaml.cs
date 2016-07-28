using CnBlogs.Entities;
using CnBlogs.Factories;
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
    public sealed partial class BlogListPage : Page
    {
        internal BlogFactory BlogFactory;
        public BlogListPage()
        {
            this.InitializeComponent();
            BlogFactory = new BlogFactory();
            BlogFactory.OnLoadMoreStarted += count=> LoadingProgressRing.IsActive = true;
            BlogFactory.OnLoadMoreCompleted += count => LoadingProgressRing.IsActive = false;
            BlogFactory.Refresh();
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
        private void BlogsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            App.NavigationService.DetailFrameNavigate(typeof(BlogBodyPage),e.ClickedItem );
        }
        private void RefreshBlogListButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
