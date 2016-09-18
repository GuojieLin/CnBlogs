using CnBlogs.Core.Enums;
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
                //根据当前状态更新界面，手机无需根据大小进行改变，因此AdaptiveStates为null
                UpdateForVisualState(AdaptiveStates?.CurrentState);
            };
            //导航及界面主次Frame切换等都由NavigationService进行控制
            bool isNarrow = AdaptiveStates?.CurrentState == NarrowState;
            App.InitNavigationService(MasterFrame, DetailFrame, TertiaryFrame, CnBlogSplitView, isNarrow);

            DetailFrame.Navigated += (sender, e) =>
            {
                UpdateForVisualState(AdaptiveStates?.CurrentState);
            };
            //打开程序是跳转到博客列表
            App.NavigationService.MasterFrameNavigate(typeof(BlogListPage));
            //打开缓存。
        }

        #region 桌面需要用
        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }
        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            if (newState == null && oldState == null) return;
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
