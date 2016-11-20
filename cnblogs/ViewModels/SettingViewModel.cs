using CnBlogs.Common;
using CnBlogs.Core;
using CnBlogs.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace CnBlogs.ViewModels
{
    class SettingViewModel: NotifyPropertyChanged
    {
        public SettingManager SettingManager;
        private bool _isDarkModel;
        public bool IsDarkModel
        {
            get { return _isDarkModel; }
            set
            {
                _isDarkModel = value;
                OnPropertyChanged();
            }
        }
        public bool _isNoImagesModel;
        public bool IsNoImagesModel
        {
            get { return _isNoImagesModel; }
            set
            {
                _isNoImagesModel = value;
                OnPropertyChanged();
            }
        }
        public int _fontSize;
        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                OnPropertyChanged();
            }
        }
        private bool _isFullWindows;
        public bool IsFullWindows
        {
            get { return _isFullWindows; }
            set
            {
                _isFullWindows = value;
                OnPropertyChanged();
            }
        }

        public SettingViewModel()
        {
            SettingManager = SettingManager.Current;
            this.IsDarkModel = SettingManager.Theme == ElementTheme.Dark;
            this.IsNoImagesModel = SettingManager.IsNoImagesMode;
            this.IsFullWindows = SettingManager.IsFullWindows;
            this.FontSize = (int)SettingManager.FontSize;
        }

        public void UpdateTheme(bool dark)
        {
            ElementTheme theme = dark ? ElementTheme.Dark : ElementTheme.Light;
            SettingManager.UpdateTheme(theme);
        }

        internal void UpdateNoImagesMode(bool isNoImages)
        {
            SettingManager.UpdateNoImagesMode(isNoImages);
        }
        internal void UpdateFullWindows(bool isFullWindows)
        {
            SettingManager.UpdateFullWindows(isFullWindows);
        }
        internal void UpdateFontSize(int size)
        {
            FontSize fontSize = (FontSize)size;
            SettingManager.UpdateFontSize(fontSize);
        }
    }
}
