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
using Windows.UI.ViewManagement;
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
        public MainPage()
        {
            this.InitializeComponent();
            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            MasterFrame.Navigating += (sender, e) =>
            {
                LoadingProgressRing.IsIndeterminate = true;
            };
            MasterFrame.Navigated += (sender, e) =>
            {
                LoadingProgressRing.IsIndeterminate = false;
                UpdateForVisualState(AdaptiveStates.CurrentState);
            };
            App.InitNavigationService(MasterFrame, DetailFrame);
            DetailFrame.Navigated += (sender,e)=> 
            {
                UpdateForVisualState(AdaptiveStates.CurrentState);
            };
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
            App.NavigationService.FirstLevelNavigate(typeof(BlogListPage));
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            App.NavigationService.GoBack(e);
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }

        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            if (newState == NarrowState && 
                oldState == MediumState)
            {
                //从大变小
                App.NavigationService.MediumToNarrow();
            }
            else if(newState == MediumState &&
                oldState == NarrowState)
            {
                //从小变大
                App.NavigationService.NarrowToMedium();
            }
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = DetailFrame.CanGoBack || MasterFrame.CanGoBack 
                ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        private void FirstMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            if (HamburgerButton.IsSelected)
            {
                CnBlogSplitView.IsPaneOpen = !CnBlogSplitView.IsPaneOpen;
            }
            if (HomeListItem.IsSelected)
            {
                MasterFrame.Navigate(typeof(BlogListPage));
                //MasterFrame.Navigate(typeof(BlogListPage), DetailFrame);
            }
            else if (NewsListItem.IsSelected)
            {
                MasterFrame.Navigate(typeof(NewsPage));
            }
        }
        private void SecondMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
