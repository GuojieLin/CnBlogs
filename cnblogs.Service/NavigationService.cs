using CnBlogs.Common;
using CnBlogs.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CnBlogs.Service
{
    public class NavigationService
    {
        public static NavigationService Instance;
        /// <summary>
        /// 当前设备是否是手机设备
        /// </summary>
        public bool IsMobile { get { return AnalyticsInfo.VersionInfo.DeviceFamily == DeviceFamily.WindowsMobile; } }
        public Action NavigateToDetailAction { get; set; }
        public Action NavigateToMasterAction { get; set; }
        public bool IsNarrow { get; private set; }
        public int MasterFrameDepth;
        public int DetailFrameDepth;
        public int TertiaryFrameDepth;
        public Type DefaultBlankPage { get; private set;}
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
        public Frame MasterFrame { get; private set; }
        public Frame DetailFrame { get; private set; }
        public Frame TertiaryFrame { get; private set; }
        public Frame LastFrame { get; private set; }
        //public SplitView CnBlogSplitView { get; private set; }
        //public CommandBar CommandBar { get; private set; }
        //private Stack<Type> _mainPageStack;
        //private Stack<Type> _detailPageStack;
        public NavigationService(Frame masterFrame, Frame detailFrame, Frame tertiaryFrame, Type defaultBlankPage, bool isNarrow)
        {
            DefaultBlankPage = defaultBlankPage;
            MasterFrame = masterFrame;
            DetailFrame = detailFrame;
            //CnBlogSplitView = splitView;
            TertiaryFrame = tertiaryFrame;
            IsNarrow = isNarrow;
            Instance = this;
            //CommandBar = commandBar;
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
            LastFrame = MasterFrame;
            MasterFrameDepth++;
            //当主导航跳转时，显示左侧菜单，
            //CnBlogSplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
            UpdateBackButton();
            UpdateFrame();
            //NavigateToMasterAction?.Invoke();
        }

        public void LastFrameNavigate(Type type, object parameter = null)
        {
            //一级frame不做其他跳转操作。若最后一个跳转的不是三级frame则让二级frame进行跳转
            if (LastFrame == TertiaryFrame)
            {
                TertiaryFrameNavigate(type, parameter);
            }
            else
            {
                DetailFrameNavigate(type, parameter);
            }
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

            LastFrame = DetailFrame;
            DetailFrameDepth++;
            UpdateBackButton();
            UpdateFrame();
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
            LastFrame = TertiaryFrame;
            TertiaryFrameDepth++;
            UpdateBackButton();
            UpdateFrame();
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
                NavigateToDetailAction?.Invoke();
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                NavigateToMasterAction?.Invoke();
            }
        }
        /// <summary>
        /// 从小变大，从主导航窗口分离详情页面至右侧
        /// </summary>
        public void NarrowToMedium()
        {
            IsNarrow = false;
            UpdateBackButton();
            UpdateFrame();
        }
        public void ClearMasterFrameStack()
        {
            MasterFrameDepth = 0;
            if (MasterFrame.BackStackDepth > 0) MasterFrame.BackStack.Clear();
            //需要留下空白页
        }
        private void InitDetailFrame()
        {
            DetailFrame.Navigate(DefaultBlankPage);
            DetailFrameDepth = 1;
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
            }
            else if (DetailFrameCanGoBack )
            {
                if(DetailFrame.CanGoBack)  DetailFrame.GoBack();
                DetailFrameDepth--;
                //DetailFrame.Visibility = DetailFrameCanGoBack ? Visibility.Visible : Visibility.Collapsed;
            }
            UpdateBackButton();
            UpdateFrame();
        }

        private void UpdateFrame()
        {
            //可以后退
            if (CanGoBack)
            {
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
            }
            else
            {
                if (IsNarrow) DetailFrame.Visibility = Visibility.Collapsed;
                else DetailFrame.Visibility = Visibility.Visible;
                TertiaryFrame.Visibility = Visibility.Collapsed;
            }
        }
    }
}
