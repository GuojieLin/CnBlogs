using CnBlogs.Core.Constants;
using CnBlogs.Core.Enums;
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
        /// <summary>
        /// 是否可以后退
        /// </summary>
        public bool CanGoBack
        {
            get
            {
                return DetailFrameCanGoBack;//|| MasterFrameCanGoBack;
            }
        }
        //private bool MasterFrameCanGoBack
        //{
        //    get
        //    {
        //        return (IsNarrow && (MasterFrame != null &&
        //            MasterFrame.BackStackDepth >= 1 &&
        //            MasterFrame.CanGoBack));
        //    }
        //}
        private bool DetailFrameCanGoBack
        {
            get
            {
                return (DetailFrame != null &&
                    DetailFrame.BackStackDepth > 1 &&
                    DetailFrame.CanGoBack);
            }
        }
        public Frame MasterFrame;
        public Frame DetailFrame;
        public SplitView CnBlogSplitView;
        //private Stack<Type> _mainPageStack;
        //private Stack<Type> _detailPageStack;
        public NavigationService(Frame masterFrame, Frame detailFrame,SplitView splitView,bool isNarrow)
        {
            MasterFrame = masterFrame;
            DetailFrame = detailFrame;
            CnBlogSplitView = splitView;
            IsNarrow = isNarrow;
        }

        public void MasterFrameNavigate(Type type, object parameter = null)
        {
            ClearMasterFrameStack();
            ClearDetailFrameStack();
            if (parameter == null)
            {
                MasterFrame.Navigate(type);
            }
            else
            {
                MasterFrame.Navigate(type, parameter);
            }
            //当主导航跳转时，显示左侧菜单，
            //CnBlogSplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
            UpdateBackButton();
        }
        public void DetailFrameNavigate(Type type, object parameter = null)
        {
            ClearDetailFrameStack();
            if (parameter == null)
            {
                DetailFrame.Navigate(type);
            }
            else
            {
                DetailFrame.Navigate(type, parameter);
            }
            DetailFrame.Visibility = Visibility.Visible;
            UpdateBackButton();
            //if (IsNarrow && DetailFrameCanGoBack)
            //{
            //    CnBlogSplitView.DisplayMode = SplitViewDisplayMode.Overlay;
            //}
        }
        /// <summary>
        /// 从大变小，优先显示详情页面
        /// </summary>
        public void MediumToNarrow()
        {
            IsNarrow = true;
            //表示存在
            DetailFrame.Visibility = DetailFrameCanGoBack ? Visibility.Visible : Visibility.Collapsed;
            UpdateBackButton();
            
        }
        public void UpdateBackButton()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = CanGoBack ?
            AppViewBackButtonVisibility.Visible :
            AppViewBackButtonVisibility.Collapsed;
            CnBlogSplitView.DisplayMode = IsNarrow && CanGoBack ? SplitViewDisplayMode.Overlay : SplitViewDisplayMode.CompactOverlay;
        }
        /// <summary>
        /// 从小变大，从主导航窗口分离详情页面至右侧
        /// </summary>
        public void NarrowToMedium()
        {
            IsNarrow = false;
            DetailFrame.Visibility = Visibility.Visible;
            UpdateBackButton();
            //CnBlogSplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
        }
        public void ClearMasterFrameStack()
        {
            if (MasterFrame.BackStackDepth > 1) MasterFrame.BackStack.Clear();
            //需要留下空白页
        }
        private void InitDetailFrame()
        {
            DetailFrame.Navigate(typeof(BlankPage));
        }
        /// <summary>
        /// 当主Frame跳转时，清空明细Frame
        /// </summary>
        public void ClearDetailFrameStack()
        {
            if (DetailFrame.BackStackDepth < 1)
            {
                InitDetailFrame();
            }

            if (DetailFrame.BackStackDepth > 1)
            {
                DetailFrame.BackStack.Clear();
                InitDetailFrame();
            }
            //需要留下空白页
        }
        public void GoBack(BackRequestedEventArgs e)
        {
            if (DetailFrameCanGoBack)
            {
                DetailFrame.GoBack();
                e.Handled = true;
            }
            //else if (MasterFrameCanGoBack)
            //{
            //    MasterFrame.GoBack();
            //    e.Handled = true;
            //}
            UpdateBackButton();
        }
    }
}
