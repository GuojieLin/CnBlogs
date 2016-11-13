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
        BloggerHomeViewModel BloggerHomeViewModel;
        public BloggerHomePage()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            bool needLogin = false;
            Blogger blogger = e.Parameter as Blogger;
            if (blogger == null)
            {
                if (AuthenticationService.IsLogin)
                {
                    Grid.Visibility = Visibility.Visible;
                    NeedLoginGrid.Visibility = Visibility.Collapsed;
                    blogger = await AuthenticationService.LoadUserInfo();
                    if (blogger == null)
                    {
                        CacheManager.LoginUserInfo.Logout();
                        CacheManager.Current.UpdateLogout();
                        //重新登录
                    }
                }
                else
                {
                    CacheManager.LoginUserInfo.Logout();
                    CacheManager.Current.UpdateLogout();
                    //TODO:登录
                }
            }
            if (!AuthenticationService.IsLogin)
            {
                Grid.Visibility = Visibility.Collapsed;
                NeedLoginGrid.Visibility = Visibility.Visible;
                return;
            }
            if (e.NavigationMode == NavigationMode.New)
            {
                blogger = await BlogService.LoadCurrentUserInfoAsync(blogger.BlogApp);
                BloggerHomeViewModel = new BloggerHomeViewModel(blogger);
            }
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
