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

namespace CnBlogs.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ListBoxItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListBoxItem tappedItem = sender as ListBoxItem;
            if (tappedItem != null && tappedItem.Name.Equals("HamburgerButton")) //汉堡按钮
            {
                mainSplitView.IsPaneOpen = !mainSplitView.IsPaneOpen;
            }
        }

        private void mainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void mainNavigationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
