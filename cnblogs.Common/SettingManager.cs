using CnBlogs.Core;
using CnBlogs.Core.Constants;
using CnBlogs.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace CnBlogs.Common
{
    /// <summary>
    /// 配置保留在云端,以便在不同设备上同步
    /// </summary>
    public sealed class SettingManager : NotifyPropertyChanged, IManager
    {
        private ElementTheme _theme;
        public ElementTheme Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                OnPropertyChanged();
            }
        }
        private FontSize _fontSize = FontSize.Small;
        public FontSize FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                OnPropertyChanged();
            }
        }
        private bool _isNoImagesMode;
        public bool IsNoImagesMode
        {
            get { return _isNoImagesMode; }
            set
            {
                _isNoImagesMode = value;
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
        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                OnPropertyChanged();
            }
        }

        public readonly static SettingManager Current = new SettingManager();
        private SettingManager()
        {
            _configurationManager = new RoamingSetting();
            Load();
        }
        public event SettingChangedEventHandler SettingChanged;
        private ISetting _configurationManager;
        public void Load()
        {
            int theme;
            if (_configurationManager.GetSetting(Configuration.Theme, out theme))
            {
                Theme = (ElementTheme)theme;
            }
            else
            {
                Theme = ElementTheme.Light;
            }

            int fontSize;
            if (_configurationManager.GetSetting(Configuration.FontSize, out fontSize))
            {
                FontSize = (FontSize)fontSize;
            }
            else
            {
                FontSize = FontSize.Narmal;
            }

            bool isNoImageMode;
            if (_configurationManager.GetSetting(Configuration.IsNoImagesMode, out isNoImageMode))
            {
                IsNoImagesMode = isNoImageMode;
            }
            else
            {
                IsNoImagesMode = false;
            }
            bool isFullWindows;
            if (_configurationManager.GetSetting(Configuration.IsFullWindows, out isFullWindows))
            {
                IsFullWindows = isFullWindows;
            }
            else
            {
                IsFullWindows = false;
            }

            int pageSize;
            if (_configurationManager.GetSetting(Configuration.PageSize, out pageSize))
            {
                PageSize = pageSize;
            }
            else
            {
                PageSize = Configuration.DefaultPageSize;
            }
        }
        private void OnShareDataChanged()
        {
            SettingChanged?.Invoke();
        }
        public void UpdateTheme(ElementTheme theme)
        {
            Theme = theme;
            _configurationManager.SetSetting(Configuration.Theme, (int)Theme);
            OnShareDataChanged();
        }
        public void UpdateFontSize(FontSize fontSize)
        {
            FontSize = fontSize;
            _configurationManager.SetSetting(Configuration.FontSize, (int)FontSize);
            OnShareDataChanged();
        }
        public void UpdateNoImagesMode(bool isNoImages)
        {
            IsNoImagesMode = isNoImages;
            _configurationManager.SetSetting(Configuration.IsNoImagesMode, IsNoImagesMode);
            OnShareDataChanged();
        }

        public void UpdateFullWindows(bool isFullWindows)
        {
            IsFullWindows = isFullWindows;
            _configurationManager.SetSetting(Configuration.IsFullWindows, IsNoImagesMode);
            OnShareDataChanged();
        }
    }
    public delegate void SettingChangedEventHandler();
}
