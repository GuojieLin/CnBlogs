using CnBlogs.Entities;
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

namespace CnBlogs.UserControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlogItemUserControl : Page
    {
        public Blog Blog => this.DataContext as Blog;
        public BlogItemUserControl()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DiggsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
