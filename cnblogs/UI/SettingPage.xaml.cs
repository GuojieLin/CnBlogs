using CnBlogs.Common;
using CnBlogs.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
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
    public sealed partial class SettingPage : Page
    {
        SettingViewModel SettingViewModel;
        public SettingPage()
        {
            this.InitializeComponent();
            SettingViewModel = new SettingViewModel();
            FullWindowsToggleSwitch.IsEnabled = App.NavigationService.IsMobile;
        }

        private void DarkModeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            bool isDark = ((ToggleSwitch)sender).IsOn;
            SettingViewModel.UpdateTheme(isDark);
        }

        private void NoImagesModeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            bool isNoImages = ((ToggleSwitch)sender).IsOn;
            SettingViewModel.UpdateNoImagesMode(isNoImages);
        }
        

        private void ClearChacheButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void VoteButton_Click(object sender, RoutedEventArgs e)
        {


        }

        private void FontSizeSlider_Tapped(object sender, TappedRoutedEventArgs e)
        {
            double fontSize = ((Slider)sender).Value;
            SettingViewModel.UpdateFontSize((int)fontSize);
        }

        private void FullWindowsToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            bool isFullWindows = ((ToggleSwitch)sender).IsOn;
            SettingViewModel.UpdateFullWindows(isFullWindows);
            if (App.NavigationService.IsMobile)
            {
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent(typeof(StatusBar).ToString()))
                {
                    StatusBar statusBar = StatusBar.GetForCurrentView();
                    statusBar.BackgroundColor = Colors.OrangeRed;
                    statusBar.BackgroundOpacity = 1;
                    if (SettingManager.Current.IsFullWindows)
                    {
                        statusBar.HideAsync();
                    }
                    else
                    {
                        statusBar.ShowAsync();
                    }
                }
            }
        }
    }
}
