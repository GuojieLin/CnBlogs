using CnBlogs.Common;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CnBlogs.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BloggerHomePage : Page
    {
        private bool _isLogining;
        BloggerHomeViewModel BloggerHomeViewModel;
        public BloggerHomePage()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Blogger blogger = e.Parameter as Blogger;
            if (blogger == null)
            {
                if (AuthenticationService.IsLogin)
                {
                    Grid.Visibility = Visibility.Visible;
                    NeedLoginGrid.Visibility = Visibility.Collapsed;
                    //登录返回时无需重新获取信息 
                    if (!_isLogining)
                    {
                        blogger = await AuthenticationService.LoadUserInfo();
                        if (blogger == null)
                        {
                            CacheManager.LoginUserInfo.Logout();
                            CacheManager.Current.UpdateLogout();
                        }
                    }
                    else
                    {
                        blogger = CacheManager.LoginUserInfo.Blogger;
                    }
                }
                else
                {
                    CacheManager.LoginUserInfo.Logout();
                    CacheManager.Current.UpdateLogout();
                }
            }
            if (!AuthenticationService.IsLogin)
            {
                Grid.Visibility = Visibility.Collapsed;
                NeedLoginGrid.Visibility = Visibility.Visible;
                _isLogining = true;
                return;
            }
            _isLogining = false;
            BloggerHomeViewModel = new BloggerHomeViewModel(blogger);
            BloggerHomeViewModel.OnLoadMoreStarted += count => LoadingProgressRing.IsActive = true;
            BloggerHomeViewModel.OnLoadMoreCompleted += count => LoadingProgressRing.IsActive = false;
            base.OnNavigatedTo(e);
        }

        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {

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
    }
}
