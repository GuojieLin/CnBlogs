using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Core
{
    public class ConfigurationManager : IRoamingSetting
    {
        public bool FindConfiguration<T>(string settingName, out T value)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (roamingSettings.Values.ContainsKey(settingName))
            {
                value = (T)Convert.ChangeType(roamingSettings.Values[settingName], typeof(T));
                return true;
            }
            value = default(T);
            return false;
        }

        public void SetConfiguration<T>(string settingName, T value)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            roamingSettings.Values[settingName] = value;
        }
    }
}
