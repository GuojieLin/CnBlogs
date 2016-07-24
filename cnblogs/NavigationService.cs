using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace CnBlogs
{
    public class NavigationService
    {
        public bool IsNarrow { get; private set;}
        public Frame MainFrame;
        public Frame DetailFrame;
        /// <summary>
        /// 上一次导航类型
        /// </summary>
        private Frame _lastNavigateFrame;

        private Stack<Type> _mainPageStack;
        private Stack<Type> _detailPageStack;
        public bool ShowBackUpButton { get { return this._detailPageStack.Count > 0; } }
        public NavigationService(Frame mainFrame, Frame detailFrame)
        {
            MainFrame = mainFrame;
            DetailFrame = detailFrame;

            _mainPageStack = new Stack<Type>();
            _detailPageStack = new Stack<Type>();

        }

        public void FirstLevelNavigate(Type type, object parameter = null)
        {
            _mainPageStack.Push(type);
            if (parameter == null)
            {
                MainFrame.Navigate(type);
            }
            else
            {
                MainFrame.Navigate(type, parameter);
            }
            _lastNavigateFrame = MainFrame;
            _detailPageStack.Clear();
        }
        public void SecondLevelNavigate(Type type, object parameter = null)
        {
            if (_lastNavigateFrame == MainFrame)
            {
                //上一次是主导航,清空
                _detailPageStack.Clear();
            }
            _detailPageStack.Push(type);
            if (IsNarrow) FirstLevelNavigate(type, parameter);
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
            if (_detailPageStack.Count >= 1)
                App.NavigationService.FirstLevelNavigate(_detailPageStack.Peek(), null);
        }
        /// <summary>
        /// 从小变大，从主导航窗口分离详情页面至右侧
        /// </summary>
        public void NarrowToMedium()
        {
            IsNarrow = false;
            if (_detailPageStack.Count >= 1)
            {
                App.NavigationService.SecondLevelNavigate(_detailPageStack.Peek(), null);
                while (MainFrame.CurrentSourcePageType != _mainPageStack.Peek())
                {
                    if(FirstLevelFrameCanGoBack()) MainFrame.GoBack();
                }
            }

        }

        public bool FirstLevelFrameCanGoBack()
        {
            return _mainPageStack.Count > 1 && MainFrame.CanGoBack;
        }
        public bool SecondLevelFrameCanGoBack()
        {

            return _detailPageStack.Count > 1 && DetailFrame.CanGoBack;
        }
        public void GoBack()
        {
            if (FirstLevelFrameCanGoBack())
            {
                MainFrame.GoBack();
            }
            else if(SecondLevelFrameCanGoBack())
            {
                DetailFrame.GoBack();
            }
        }
    }
}
