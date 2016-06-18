using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace CnBlogs.UWP.Configurations
{
    public class Configuration
    {
        public readonly static Configuration DefaultConfiguration = new Configuration();
        /// <summary>
        /// 是否总是显示导航栏
        /// </summary>
        internal bool AlwaysShowNavigation
        {
            get; set;
        }
        /// <summary>
        /// 每次加载新闻条数
        /// </summary>
        internal int LoadNewsCount
        {
            get; set;
        }
        /// <summary>
        /// 每次加载博客条数
        /// </summary>
        internal int LoadBlogCount
        {
            get; set;
        }
        /// <summary>
        /// 主题
        /// </summary>
        internal ApplicationTheme Theme
        {
            get; set;
        }

        public Configuration()
        {
            AlwaysShowNavigation = true;
            LoadBlogCount = 20;
            LoadBlogCount = 20;
            Theme = ApplicationTheme.Light;
        }

        public Configuration(ApplicationDataContainer container)
        {
            AlwaysShowNavigation = container.Values[Constants.AppConstant.AlwaysShowNavigation] != null ?
                Convert.ToBoolean(container.Values[Constants.AppConstant.AlwaysShowNavigation]) :
                DefaultConfiguration.AlwaysShowNavigation;
            LoadBlogCount = container.Values[Constants.AppConstant.LoadBlogCount] != null ?
                int.Parse(container.Values[Constants.AppConstant.LoadBlogCount].ToString()) :
                DefaultConfiguration.LoadBlogCount;
            LoadBlogCount = container.Values[Constants.AppConstant.LoadBlogCount] != null ?
                int.Parse(container.Values[Constants.AppConstant.LoadBlogCount].ToString()) :
                DefaultConfiguration.LoadBlogCount;
            if (container.Values[Constants.AppConstant.Theme] != null)
            {
                Theme = (ApplicationTheme)Enum.Parse(typeof(ApplicationTheme), container.Values[Constants.AppConstant.Theme].ToString());
            }
            else
            {
                Theme = DefaultConfiguration.Theme;
            }
        }
    }
}
