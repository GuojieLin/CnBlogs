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

namespace CnBlogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeBlogPage : Page
    {
        internal BlogFactory BlogFactory;
        public HomeBlogPage()
        {
            this.InitializeComponent();
            BlogFactory = new BlogFactory();
            BlogFactory.OnLoadMoreStarted += DataLoading;
            BlogFactory.OnLoadMoreCompleted += DataLoaded;
            BlogFactory.Refresh();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        private void BlogsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        /// <summary>
        /// 博客列表开始加载
        /// </summary>
        private void DataLoading(uint count)
        {
            LoadingProgressRing.IsActive = true;
        }
        /// <summary>
        /// 博客列表加载完毕
        /// </summary>
        private void DataLoaded(uint count)
        {
            LoadingProgressRing.IsActive = false;
        }
    }
}
