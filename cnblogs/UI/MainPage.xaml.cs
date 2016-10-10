using CnBlogs.Common;
using CnBlogs.Core.Enums;
using CnBlogs.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
        public SettingManager SettingManager;
        public MainPage()
        {
            this.InitializeComponent();
            SettingManager = SettingManager.Current;
            //设置Dispatcher,使得更新操作可以异步进行
            SettingManager.SetDispatcher(this.Dispatcher);
            InitFrame();
            InitNavigationService();
            //导航及界面主次Frame切换等都由NavigationService进行控制

            SystemNavigationManager.GetForCurrentView().BackRequested += BackRequest;
            //若要全屏则隐藏顶部状态栏
            if (App.NavigationService.IsMobile && SettingManager.IsFullWindows)
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.HideAsync();
            }
        }


        private bool isExit = false;
        /// <summary>
        /// 后退逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackRequest(object sender, BackRequestedEventArgs e)
        {
            //有可以后退则后退，不可以后退则双击退出程序
            if (App.NavigationService.CanGoBack)
            {
                e.Handled = true;
                App.NavigationService.GoBack(e);
                isExit = false;
            }
            else if (!e.Handled && !isExit)
            {
                DoubleBackExit(e);
            }
        }
        /// <summary>
        /// 手机双击退出到手机主页
        /// </summary>
        /// <param name="e"></param>
        private void DoubleBackExit(BackRequestedEventArgs e)
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            //statusBar.ShowAsync();
            //statusBar.ForegroundColor = Colors.White; // 前景色  
            statusBar.BackgroundOpacity = 0.5; // 透明度  
            statusBar.ProgressIndicator.Text = "再按一次返回键退出程序"; // 文本  
            statusBar.ProgressIndicator.ShowAsync();
            isExit = true;
            Task.Run(async () =>
            {
                //Windows.Data.Xml.Dom. XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);  
                //Windows.Data.Xml.Dom.XmlNodeList elements = toastXml.GetElementsByTagName("text");  
                //elements[0].AppendChild(toastXml.CreateTextNode("再按一次返回键退出程序。"));  
                //ToastNotification toast = new ToastNotification(toastXml);  
                //ToastNotificationManager.CreateToastNotifier().Show(toast);       
                await Task.Delay(1500);
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    await statusBar.ProgressIndicator.HideAsync();
                    //statusBar.HideAsync();
                });
                isExit = false;
            });
            e.Handled = true;
        }

        private void InitNavigationService()
        {
            bool isNarrow = AdaptiveStates?.CurrentState == NarrowState;
            App.InitNavigationService(MasterFrame, DetailFrame, TertiaryFrame, isNarrow);

            //是手机设备或宽度为Narrow则显示CommandBar，否则显示SplitView
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
            //打开程序时跳转到博客列表
            App.NavigationService.MasterFrameNavigate(typeof(BlogListPage));
        }

        private void InitFrame()
        {
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

        private void SettingButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.NavigationService.DetailFrameNavigate(typeof(SettingPage));
        }
    }
}
