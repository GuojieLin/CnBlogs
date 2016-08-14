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
    public sealed class SettingManager : ISettingManager
    {
        public ElementTheme Theme { get; private set; }

        public FontSize FontSize { get; private set; }

        public bool IsNoImagesMode { get; private set; }
        public bool IsFullWindows { get; private set; }
        public int PageSize { get; private set; }

        private static SettingManager _current;

        public static SettingManager Current
        {
            get { return _current ?? (_current = new SettingManager()); }
        }
        public SettingManager()
        {
            _configurationManager = new ConfigurationManager();
            LoadSetting();
        }
        public event SettingChangedEventHandler ShareDataChanged;
        private IRoamingSetting _configurationManager;
        private void LoadSetting()
        {
            ElementTheme theme;
            if (_configurationManager.FindConfiguration(Configuration.Theme, out theme))
            {
                Theme = theme;
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
            ShareDataChanged?.Invoke();
        }
        public void UpdateTheme(bool dark)
        {
            Theme = dark ? ElementTheme.Dark : ElementTheme.Light;
            _configurationManager.SetConfiguration(Configuration.Theme, Theme);
            OnShareDataChanged();
        }
        public void UpdateFontSize(FontSize fontSize)
        {
            FontSize = fontSize;
            _configurationManager.SetConfiguration(Configuration.FontSize, FontSize);
            OnShareDataChanged();
        }
        public void UpdateNoImagesMode(bool no_images)
        {
            IsNoImagesMode = no_images;
            _configurationManager.SetConfiguration(Configuration.IsNoImagesMode, IsNoImagesMode);
            OnShareDataChanged();
        }

        public void UpdateFullWindows(bool no_images)
        {
            IsNoImagesMode = no_images;
            _configurationManager.SetConfiguration(Configuration.IsFullWindows, IsNoImagesMode);
            OnShareDataChanged();
        }
    }
    public delegate void SettingChangedEventHandler();
}
