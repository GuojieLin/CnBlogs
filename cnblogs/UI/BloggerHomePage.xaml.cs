using CnBlogs.Common;
using CnBlogs.Core.Extentsions;
using CnBlogs.Entities;
using CnBlogs.Service;
using CnBlogs.ViewModels;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CnBlogs.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BloggerHomePage : Page
    {
        internal BloggerHomeViewModel BloggerHomeViewModel { get; set; }
        public BloggerHomePage()
        {
            this.InitializeComponent();
            //this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }
    
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Blogger recommentBlogger = e.Parameter as Blogger;
            if (e.NavigationMode == NavigationMode.New)
            {
                //为空时说明加载的是自己的信息，获取当前登陆用户信息
                if (recommentBlogger == null)
                {
                    recommentBlogger = CacheManager.LoginUserInfo.Blogger;
                    if (!AuthenticationService.IsLogin ||
                        recommentBlogger == null ||
                        recommentBlogger.BlogApp.IsNullOrEmpty())
                    {
                        //粉丝数量控件隐藏
                        FollowAmountRow.Height = new GridLength(0);
                        //显示登陆控件
                        LoginRow.Height = new GridLength(1, GridUnitType.Auto);
                        return;
                    }
                }
            }
            //加载用户信息
            BloggerHomeViewModel = new BloggerHomeViewModel(recommentBlogger);
            BloggerHomeViewModel.OnLoadMoreStarted += count => LoadingProgressRing.IsActive = true;
            BloggerHomeViewModel.OnLoadMoreCompleted += count => LoadingProgressRing.IsActive = false;
            PhotoImage.Source = new BitmapImage(new Uri(BloggerHomeViewModel.Photo));
            base.OnNavigatedTo(e);
        }

        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            BloggerHomeViewModel.Refresh();
        }

        private void rootPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BlogGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            App.NavigationService.DetailFrameNavigate(typeof(BlogBodyPage), e.ClickedItem);
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AuthenticationService.RedictLoginPage();
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

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var appBarButton = sender as AppBarButton;
            switch (appBarButton.Name)
            {
                case Contants.HomeAppBarButton:
                    App.NavigationService.MasterFrameNavigate(typeof(BlogHomePage));
                    break;
                case Contants.NewAppBarButton:
                    App.NavigationService.MasterFrameNavigate(typeof(NewsHomePage));
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
        

        private void AccountAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            App.NavigationService.DetailFrameNavigate(typeof(BloggerHomePage));
        }
    }
}
