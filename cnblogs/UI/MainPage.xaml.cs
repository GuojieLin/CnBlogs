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
using Windows.UI.Core;
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
        private ListBoxItem _lastClickListItem;
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
            App.InitNavigationService(MainFrame, DetailFrame);
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
            App.NavigationService.FirstLevelNavigate(typeof(BlogListPage));
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            App.NavigationService.GoBack();
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }

        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            var isNarrow = newState == NarrowState;
            if (isNarrow && oldState == MediumState && _lastClickListItem != null)
            {
                //从大变小
                App.NavigationService.MediumToNarrow();
            }
            else if(!isNarrow && oldState == NarrowState)
            {
                //从小变大
                App.NavigationService.NarrowToMedium();
            }
        }
        private void FirstMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            if (HamburgerButton.IsSelected)
            {
                CnBlogSplitView.IsPaneOpen = !CnBlogSplitView.IsPaneOpen;
            }
            if (HomeListItem.IsSelected)
            {
                MainFrame.Navigate(typeof(BlogListPage));
                //MainFrame.Navigate(typeof(BlogListPage), DetailFrame);
            }
            else if (NewsListItem.IsSelected)
            {
                MainFrame.Navigate(typeof(NewsPage));
            }
        }
        private void SecondMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
