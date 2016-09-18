using CnBlogs.Common;
using CnBlogs.Core.Enums;
using CnBlogs.UI;
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
        public int MasterFrameDepth;
        public int DetailFrameDepth;
        public int TertiaryFrameDepth;
        /// <summary>
        /// 是否可以后退
        /// </summary>
        public bool CanGoBack
        {
            get
            {
                return DetailFrameCanGoBack || TertiaryFrameCanGoBack;//|| MasterFrameCanGoBack;
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
                    DetailFrameDepth > 1 );
            }
        }

        private bool TertiaryFrameCanGoBack
        {
            get
            {
                return (TertiaryFrame != null &&
                    TertiaryFrameDepth > 0 );
            }
        }
        public Frame MasterFrame;
        public Frame DetailFrame;
        public Frame TertiaryFrame;
        public SplitView CnBlogSplitView;
        //private Stack<Type> _mainPageStack;
        //private Stack<Type> _detailPageStack;
        public NavigationService(Frame masterFrame, Frame detailFrame, Frame tertiaryFrame,SplitView splitView,bool isNarrow)
        {
            MasterFrame = masterFrame;
            DetailFrame = detailFrame;
            CnBlogSplitView = splitView;
            TertiaryFrame = tertiaryFrame;
            IsNarrow = isNarrow;
        }

        public void MasterFrameNavigate(Type type, object parameter = null)
        {
            ClearMasterFrameStack();
            ClearDetailFrameStack();
            ClearTertiaryFrameStack();
            if (parameter == null)
            {
                MasterFrame.Navigate(type);
            }
            else
            {
                MasterFrame.Navigate(type, parameter);
            }
            MasterFrameDepth++;
            //当主导航跳转时，显示左侧菜单，
            //CnBlogSplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
            UpdateBackButton();
            UpdateFrame();
        }


        public void DetailFrameNavigate(Type type, object parameter = null)
        {
            //ClearDetailFrameStack();
            ClearTertiaryFrameStack();
            //ClearDetailFrameStack();
            if (parameter == null)
            {
                DetailFrame.Navigate(type);
            }
            else
            {
                DetailFrame.Navigate(type, parameter);

            }

            DetailFrameDepth++;
            //DetailFrame.Visibility = Visibility.Visible;
            //TertiaryFrame.Visibility = Visibility.Collapsed;
            UpdateBackButton();
            UpdateFrame();
            //if (IsNarrow && DetailFrameCanGoBack)
            //{
            //    CnBlogSplitView.DisplayMode = SplitViewDisplayMode.Overlay;
            //}
        }

        public void TertiaryFrameNavigate(Type type, object parameter = null)
        {
            //ClearDetailFrameStack();
            if (parameter == null)
            {
                TertiaryFrame.Navigate(type);
            }
            else
            {
                TertiaryFrame.Navigate(type, parameter);
            }
            TertiaryFrameDepth++;
            //TertiaryFrame.Visibility = Visibility.Visible;
            UpdateBackButton();
            UpdateFrame();
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
            UpdateBackButton();
            UpdateFrame();
        }
        public void UpdateBackButton()
        {
            if (CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }
        /// <summary>
        /// 从小变大，从主导航窗口分离详情页面至右侧
        /// </summary>
        public void NarrowToMedium()
        {
            IsNarrow = false;
            //if (TertiaryFrameCanGoBack)
            //{

            //}
            //else
            //{
            //    DetailFrame.Visibility = Visibility.Visible;
            //}
            UpdateBackButton();
            UpdateFrame();
            //CnBlogSplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
        }
        public void ClearMasterFrameStack()
        {
            MasterFrameDepth = 0;
            if (MasterFrame.BackStackDepth > 0) MasterFrame.BackStack.Clear();
            //需要留下空白页
        }
        private void InitDetailFrame()
        {
            DetailFrame.Navigate(typeof(BlankPage));
            DetailFrameDepth = 1;
            //DetailFrame.SetAttachedPropertyValue(typeof(Canvas),"ZIndex", Contants.DetailFrameDefaultZIndex);
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

        private void ClearTertiaryFrameStack()
        {
            if (TertiaryFrame.BackStackDepth > 0)
            {
                TertiaryFrame.BackStack.Clear();
                TertiaryFrameDepth = 0;
                //TertiaryFrame.Navigate(typeof(BlankPage));
            }
        }
        public void GoBack(BackRequestedEventArgs e)
        {
            if (TertiaryFrameCanGoBack)
            {
                if (TertiaryFrame.CanGoBack) TertiaryFrame.GoBack();
                TertiaryFrameDepth--;
                //若还可以继续后退，则显示，否则隐藏
                //TertiaryFrame.Visibility = TertiaryFrameCanGoBack ? Visibility.Visible : Visibility.Collapsed;
            }
            else if (DetailFrameCanGoBack )
            {
                if(DetailFrame.CanGoBack)  DetailFrame.GoBack();
                DetailFrameDepth--;
                //DetailFrame.Visibility = DetailFrameCanGoBack ? Visibility.Visible : Visibility.Collapsed;
            }
            //else if (MasterFrameCanGoBack)
            //{
            //    MasterFrame.GoBack();
            //    e.Handled = true;
            //}
            UpdateBackButton();
            UpdateFrame();
        }

        private void UpdateFrame()
        {
            //可以后退
            if (CanGoBack)
            {
                //电脑
                if (CnBlogSplitView != null)
                {
                    CnBlogSplitView.DisplayMode = IsNarrow ?
                        SplitViewDisplayMode.Overlay :
                        SplitViewDisplayMode.CompactOverlay;
                }

                //三级Frame能后退
                if (TertiaryFrameCanGoBack)
                {
                    TertiaryFrame.Visibility = Visibility.Visible;
                    DetailFrame.Visibility = Visibility.Collapsed;
                }
                //二级Frame能后退
                else if (DetailFrameCanGoBack)
                {
                    DetailFrame.Visibility = Visibility.Visible;
                    TertiaryFrame.Visibility = Visibility.Collapsed;
                }
                //int zIndex = DetailFrame.GetAttachedPropertyValue<int>(typeof(Canvas), "ZIndex");
                //DetailFrame.SetAttachedPropertyValue(typeof(Canvas),"ZIndex", 2 * zIndex);
            }
            else
            {
                if (CnBlogSplitView != null)
                {
                    CnBlogSplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                }
                if (IsNarrow) DetailFrame.Visibility = Visibility.Collapsed;
                else DetailFrame.Visibility = Visibility.Visible;
                TertiaryFrame.Visibility = Visibility.Collapsed;

                //int zIndex = DetailFrame.GetAttachedPropertyValue<int>(typeof(Canvas),"ZIndex");
                //DetailFrame.SetAttachedPropertyValue(typeof(Canvas),"ZIndex", zIndex / 2);
            }
        }
    }
}
