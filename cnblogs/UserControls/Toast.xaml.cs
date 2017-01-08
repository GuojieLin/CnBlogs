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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CnBlogs.UserControls
{
    public sealed partial class Toast : UserControl
    {
        private string content;
        private TimeSpan showTime;
        private Popup popup;
        private Toast()
        {
            this.InitializeComponent();
            this.popup = new Popup();
            this.Width = Window.Current.Bounds.Width;
            this.Height = Window.Current.Bounds.Height;
            popup.Child = this;
            this.Loaded += ToastStoryboard_Loaded;
            this.Unloaded += ToastStoryboard_Unloaded;
        }
        public Toast(string content, TimeSpan showTime) : this()
        {
            this.content = content;
            this.showTime = showTime;
        }
        public Toast(string content) : this(content, TimeSpan.FromSeconds(1))
        {

        }
        public void show()
        {
            this.popup.IsOpen = true;
        }
        private void ToastStoryboard_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Current_SizeChanged;
        }

        private void ToastStoryboard_Loaded(object sender, RoutedEventArgs e)
        {
            ToastContent.Text = this.content;
            this.ToastStoryboard.BeginTime = this.showTime;
            this.ToastStoryboard.Begin();
            this.ToastStoryboard.Completed += ToastStoryboard_Completed;
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.Width = e.Size.Width;
            this.Height = e.Size.Height;
        }

        private void ToastStoryboard_Completed(object sender, object e)
        {
            this.popup.IsOpen = false;
        }
    }
}
