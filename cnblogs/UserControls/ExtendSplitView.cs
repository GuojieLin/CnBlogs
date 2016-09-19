using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CnBlogs.UserControls
{
    public sealed partial class ExtendSplitView : SplitView
    {
        public ExtendSplitView()
        {
            this.DefaultStyleKey = typeof(ExtendSplitView);
        }
        public double MinBottomWidth
        {
            get { return (double)GetValue(MinBottomWidthProperty); }
            set { SetValue(MinBottomWidthProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MinBottomWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinBottomWidthProperty =
            DependencyProperty.Register("MinBottomWidth", typeof(double), typeof(ExtendSplitView), new PropertyMetadata(0));
        public Grid BottomGrid
        {
            get { return (Grid)GetValue(BottomGridProperty); }
            set { SetValue(BottomGridProperty, value); }
        }
        // Using a DependencyProperty as the backing store for BottomGrid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomGridProperty =
            DependencyProperty.Register("BottomGrid", typeof(Grid), typeof(ExtendSplitView), new PropertyMetadata(0));
    }
}
