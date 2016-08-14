using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Core
{
    public interface IRoamingSetting
    {
        bool FindConfiguration<T>(string settingName, out T value);
        void SetConfiguration<T>(string setting, T value);
    }
}
