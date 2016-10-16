using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Core
{
    public interface ISetting
    {
        bool GetSetting<T>(string settingName, out T value);
        void SetSetting<T>(string setting, T value);
    }
}
