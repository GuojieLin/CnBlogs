using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace CnBlogs.Core
{
    public class AppDomain:NotifyPropertyChanged
    {
        public readonly static AppDomain Current = new AppDomain();
        private NetWorkType _netWorkType;
        public NetWorkType NetWorkType
        {
            get { return _netWorkType; }
            set
            {
                _netWorkType = value;
                OnPropertyChanged(); 
            }
        }
        private AppDomain()
        {
            NetWorkType = NetworkManager.Current.Network;
            NetworkManager.Current.NetworkStatusChanged += Current_NetworkStatusChanged;
        }


        private async void Current_NetworkStatusChanged(object sender)
        {
            if (CoreDispatcher == null)
            {
                NetWorkType = NetworkManager.Current.Network;
            }
            else
            {
                if (CoreDispatcher.HasThreadAccess)
                {
                    NetWorkType = NetworkManager.Current.Network;
                }
                else
                {
                    await CoreDispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () => NetWorkType = NetworkManager.Current.Network);
                }
            }
        }

    }
}
