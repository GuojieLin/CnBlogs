using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CnBlogs
{
    public class NavigationService
    {
        public bool IsNarrow { get; private set; }
        public bool CanGoBack { get { return DetailFrame != null && DetailFrame.BackStackDepth >= 1 && DetailFrame.CanGoBack; } }
        public Frame MasterFrame;
        public Frame DetailFrame;
        //private Stack<Type> _mainPageStack;
        //private Stack<Type> _detailPageStack;
        public NavigationService(Frame masterFrame, Frame detailFrame)
        {
            MasterFrame = masterFrame;
            DetailFrame = detailFrame;

            //_mainPageStack = new Stack<Type>();
            //_detailPageStack = new Stack<Type>();
            this.DetailFrameNavigate(typeof(BlankPage));
        }

        public void MasterFrameNavigate(Type type, object parameter = null)
        {
            if (parameter == null)
            {
                MasterFrame.Navigate(type);
            }
            else
            {
                MasterFrame.Navigate(type, parameter);
            }
            MasterFrame.Visibility = Visibility.Visible;
        }
        public void DetailFrameNavigate(Type type, object parameter = null)
        {
            if (parameter == null)
            {
                DetailFrame.Navigate(type);
            }
            else
            {
                DetailFrame.Navigate(type, parameter);
            }
            DetailFrame.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 从大变小，优先显示详情页面
        /// </summary>
        public void MediumToNarrow()
        {
            IsNarrow = true;
            //表示存在
            DetailFrame.Visibility = CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }
        /// <summary>
        /// 从小变大，从主导航窗口分离详情页面至右侧
        /// </summary>
        public void NarrowToMedium()
        {
            IsNarrow = false;
            DetailFrame.Visibility = Visibility.Visible;
        }
        public void GoBack(BackRequestedEventArgs e)
        {
            if (CanGoBack)
            {
                DetailFrame.GoBack();
                e.Handled = true;
            }
            else if (MasterFrame.CanGoBack)
            {
                MasterFrame.Visibility = Visibility.Visible;
                MasterFrame.GoBack();
                e.Handled = true;
            }
        }
    }
}
