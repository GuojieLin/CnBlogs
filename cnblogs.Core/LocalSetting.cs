using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;

namespace CnBlogs.Core
{
    public class LocalSetting : ISetting
    {
        public bool GetSetting<T>(string settingName, out T value)
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            if (localSetting.Values.ContainsKey(settingName))
            {
                value = (T)Convert.ChangeType(localSetting.Values[settingName], typeof(T));
                return true;
            }
            value = default(T);
            return false;
        }

        public void SetSetting<T>(string settingName, T value)
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            localSetting.Values[settingName] = value;
        }
    }
}
