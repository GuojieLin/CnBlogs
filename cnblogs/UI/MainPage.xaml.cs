using CnBlogs.Core.Constants;
using CnBlogs.Core.Enums;
using CnBlogs.Entities;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CnBlogs.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public readonly string deviceFamily = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;
        private Frame NavigateDetailFrame;
        public MainPage()
        {
            this.InitializeComponent();
            MainFrame.Navigating += (sender, e) =>
            {
                LoadingProgressRing.IsIndeterminate = true;
            };
            MainFrame.Navigated += (sender, e) =>
            {
                LoadingProgressRing.IsIndeterminate = false;
            };

            //this.SizeChanged += (o, s) =>
            //{
            //    if (s.NewSize.Width <= DeviceSize.Narrow)
            //    {
            //        //窄
            //    }
            //    else if (s.NewSize.Width <= DeviceSize.Medium)
            //    {
            //        //中等
            //    }
            //    else
            //    {
            //        //宽
            //    }
            //    //bool result = VisualStateManager.GoToState(this, state, true);

            //};

            if (deviceFamily == DeviceFamily.WindowsMobile)
            {
                //windows10手机只限制一列
                NavigateDetailFrame = MainFrame;
            }
            else if (deviceFamily == DeviceFamily.WindowsDesktop)
            {
                //windows10桌面显示2列
                NavigateDetailFrame = DetailFrame;
            }
            MainFrame.Navigate(typeof(BlogListPage), NavigateDetailFrame);

            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        
        private void FirstMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            if (HamburgerButton.IsSelected)
            {
                CnBlogSplitView.IsPaneOpen = !CnBlogSplitView.IsPaneOpen;
            }
            if (HomeListItem.IsSelected)
            {
                MainFrame.Navigate(typeof(BlogListPage), NavigateDetailFrame);
                //MainFrame.Navigate(typeof(BlogListPage), DetailFrame);
            }
            else if (NewsListItem.IsSelected)
            {
                MainFrame.Navigate(typeof(NewsPage), NavigateDetailFrame);
            }
        }

        private void SecondMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
