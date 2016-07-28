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
        public bool IsNarrow { get; private set;}
        public Frame MasterFrame;
        public Frame DetailFrame;
        /// <summary>
        /// 上一次导航类型
        /// </summary>
        private Frame _lastNavigateFrame;

        //private Stack<Type> _mainPageStack;
        //private Stack<Type> _detailPageStack;
        public bool ShowBackUpButton { get { return this.DetailFrame.CanGoBack; } }
        public NavigationService(Frame masterFrame, Frame detailFrame)
        {
            MasterFrame = masterFrame;
            DetailFrame = detailFrame;

            //_mainPageStack = new Stack<Type>();
            //_detailPageStack = new Stack<Type>();

        }

        public void FirstLevelNavigate(Type type, object parameter = null)
        {
            if (parameter == null)
            {
                MasterFrame.Navigate(type);
            }
            else
            {
                MasterFrame.Navigate(type, parameter);
            }
            _lastNavigateFrame = MasterFrame;
            //_detailPageStack.Clear();
        }
        public void SecondLevelNavigate(Type type, object parameter = null)
        {
            if (parameter == null)
            {
                DetailFrame.Navigate(type);
            }
            else
            {
                DetailFrame.Navigate(type, parameter);
            }
        }
        /// <summary>
        /// 从大变小，优先显示详情页面
        /// </summary>
        public void MediumToNarrow()
        {
            IsNarrow = true;
            //表示存在
            DetailFrame.Visibility = DetailFrame.BackStackDepth >= 1 ? Visibility.Visible : Visibility.Collapsed;
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
            if (DetailFrame.CanGoBack)
            {
                DetailFrame.GoBack();
                e.Handled = true;
            }
            else if (MasterFrame.CanGoBack)
            {
                MasterFrame.GoBack();
                e.Handled = true;
            }
        }
    }
}
