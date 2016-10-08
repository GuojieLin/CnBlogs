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
    public interface ISettingManager
    {
        
    }
    public sealed class SettingManager : NotifyPropertyChanged, ISettingManager
    {
        public ElementTheme Theme { get; private set; }
        public FontSize FontSize { get; private set; }
        public bool IsNoImagesMode { get; private set; }
        public bool IsFullWindows { get; private set; }
        public int PageSize { get; private set; }

        public readonly static SettingManager Current = new SettingManager();
        private SettingManager()
        {
            _configurationManager = new ConfigurationManager();
            LoadSetting();
        }
        public event SettingChangedEventHandler SettingChanged;
        private IRoamingSetting _configurationManager;
        private void LoadSetting()
        {
            int theme;
            if (_configurationManager.FindConfiguration(Configuration.Theme, out theme))
            {
                Theme = (ElementTheme)theme;
            }
            else
            {
                Theme = ElementTheme.Light;
            }

            FontSize fontSize;
            if (_configurationManager.FindConfiguration(Configuration.FontSize, out fontSize))
            {
                FontSize = fontSize;
            }
            else
            {
                FontSize = FontSize.Narmal;
            }

            bool isNoImageMode;
            if (_configurationManager.FindConfiguration(Configuration.IsNoImagesMode, out isNoImageMode))
            {
                IsNoImagesMode = isNoImageMode;
            }
            else
            {
                IsNoImagesMode = false;
            }
            bool isFullWindows;
            if (_configurationManager.FindConfiguration(Configuration.IsFullWindows, out isFullWindows))
            {
                IsFullWindows = isFullWindows;
            }
            else
            {
                IsFullWindows = false;
            }

            int pageSize;
            if (_configurationManager.FindConfiguration(Configuration.IsFullWindows, out pageSize))
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
            _configurationManager.SetConfiguration(Configuration.Theme, (int)Theme);
            OnShareDataChanged();
        }
        public void UpdateFontSize(FontSize fontSize)
        {
            FontSize = fontSize;
            _configurationManager.SetConfiguration(Configuration.FontSize, FontSize);
            OnShareDataChanged();
        }
        public void UpdateNoImagesMode(bool isNoImages)
        {
            IsNoImagesMode = isNoImages;
            _configurationManager.SetConfiguration(Configuration.IsNoImagesMode, IsNoImagesMode);
            OnShareDataChanged();
        }

        public void UpdateFullWindows(bool isFullWindows)
        {
            IsFullWindows = isFullWindows;
            _configurationManager.SetConfiguration(Configuration.IsFullWindows, IsNoImagesMode);
            OnShareDataChanged();
        }
    }
    public delegate void SettingChangedEventHandler();
}
