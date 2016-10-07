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
        public MainPage()
        {
            this.InitializeComponent();
            MasterFrame.Navigating += (sender, e) =>
            {
                LoadingProgressRing.Visibility = Visibility.Visible;
                LoadingProgressRing.IsIndeterminate = true;
            };
            MasterFrame.Navigated += (sender, e) =>
            {
                LoadingProgressRing.IsIndeterminate = false;
                LoadingProgressRing.Visibility = Visibility.Collapsed;
                UpdateForVisualState(AdaptiveStates?.CurrentState);
            };
            //导航及界面主次Frame切换等都由NavigationService进行控制
            bool isNarrow = AdaptiveStates?.CurrentState == NarrowState;
            App.InitNavigationService(MasterFrame, DetailFrame, TertiaryFrame, isNarrow);

            //是手机设备则显示CommandBar，否则显示SplitView

            App.NavigationService.NavigateToDetailAction = () =>
            {
                if (App.NavigationService.IsMobile || App.NavigationService.IsNarrow)
                {
                    CnBlogSplitView.DisplayMode = SplitViewDisplayMode.Overlay;
                    CnBlogSplitView.IsPaneOpen = false;
                    CommandBar.Visibility = Visibility.Collapsed;
                }
                else
                {
                    CnBlogSplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                    CommandBar.Visibility = Visibility.Collapsed;
                }
            };

            App.NavigationService.NavigateToMasterAction = () =>
            {
                if (App.NavigationService.IsMobile || App.NavigationService.IsNarrow)
                {
                    CnBlogSplitView.DisplayMode = SplitViewDisplayMode.Overlay;
                    CnBlogSplitView.IsPaneOpen = false;
                    CommandBar.Visibility = Visibility.Visible;
                }
                else
                {
                    CnBlogSplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                    CommandBar.Visibility = Visibility.Collapsed;
                }
            };
            if (!App.NavigationService.IsMobile)
            {
                CommandBar.Visibility = Visibility.Visible;
                AdaptiveStates.CurrentStateChanged += AdaptiveStates_CurrentStateChanged;
            }

            //打开程序是跳转到博客列表
            App.NavigationService.MasterFrameNavigate(typeof(BlogListPage));
            //打开缓存。
        }

        #region 桌面需要用
        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }
        private void ApdateUI()
        {
            if (App.NavigationService.IsMobile || App.NavigationService.IsNarrow)
            {
                CnBlogSplitView.DisplayMode = SplitViewDisplayMode.Overlay;
                CnBlogSplitView.IsPaneOpen = false;
                CommandBar.Visibility = Visibility.Visible;
            }
            else
            {
                CnBlogSplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                CommandBar.Visibility = Visibility.Collapsed;
            }
        }
        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            if (newState == null && oldState == null) return;
            //若当前设备是手机则无需根据宽度变化
            if (App.NavigationService.IsMobile) return;
            if (newState == NarrowState && oldState == MediumState)
            {
                //从大变小
                App.NavigationService.MediumToNarrow();
            }
            else if (newState == MediumState && oldState == NarrowState)
            {
                //从小变大  
                App.NavigationService.NarrowToMedium();
            }
        }
        private void FirstMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            if (HomeListItem.IsSelected)
            {
                App.NavigationService.MasterFrameNavigate(typeof(BlogListPage));
            }
            else if (NewsListItem.IsSelected)
            {
                App.NavigationService.MasterFrameNavigate(typeof(NewsListPage));
            }
        }
        private void SecondMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void HamburgerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CnBlogSplitView.IsPaneOpen = !CnBlogSplitView.IsPaneOpen;
        }
        #endregion
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var appBarButton = sender as AppBarButton;
            switch (appBarButton.Name)
            {
                case Contants.HomeAppBarButton:
                    App.NavigationService.MasterFrameNavigate(typeof(BlogListPage));
                    break;
                case Contants.NewAppBarButton:
                    App.NavigationService.MasterFrameNavigate(typeof(NewsListPage));
                    break;
                case Contants.MessagesAppBarButton:
                    break;
                default:
                    break;
            }

        }
    }
}
