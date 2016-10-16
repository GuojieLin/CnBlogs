using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;

namespace CnBlogs.Core
{
    public class RoamingSetting : ISetting
    {
        static RoamingSetting()
        {
            ApplicationData.Current.DataChanged +=
               new TypedEventHandler<ApplicationData, object>(DataChangeHandler);
        }
        /// <summary>
        /// 当漫游数据发送改变时处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void DataChangeHandler(ApplicationData sender, object args)
        {
            //TODO:当漫游数据发送改变时处理
        }

        public bool GetSetting<T>(string settingName, out T value)
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            if (roamingSettings.Values.ContainsKey(settingName))
            {
                value = (T)Convert.ChangeType(roamingSettings.Values[settingName], typeof(T));
                return true;
            }
            value = default(T);
            return false;
        }

        public void SetSetting<T>(string settingName, T value)
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            roamingSettings.Values[settingName] = value;
        }
    }
}
