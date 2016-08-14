using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace CnBlogs.Core
{
    public enum NewWorkType
    {
        None,
        _2G,
        _3G,
        _4G,
        WIFI,
    }

    public class NetworkManager
    {

        private static NetworkManager _current;
        public static NetworkManager Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new NetworkManager();
                }
                return _current;
            }
        }
        
        private NewWorkType _network;

        public NewWorkType Network
        {
            get
            {
                return _network;
            }
        }

        public event NetworkStatusChangedEventHandler NetworkStatusChanged;

        public NetworkManager()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
            _network = GetNewWorkType();
        }

        private void NetworkInformation_NetworkStatusChanged(object sender)
        {
            _network = GetNewWorkType();
            if (NetworkStatusChanged != null)
            {
                NetworkStatusChanged(this);
            }
        }

        private NewWorkType GetNewWorkType()
        {
            try
            {
                ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();

                if (profile.IsWlanConnectionProfile)
                {
                    return NewWorkType.WIFI;
                }
                if (profile.IsWwanConnectionProfile)
                {
                    WwanDataClass connectionClass = profile.WwanConnectionProfileDetails.GetCurrentDataClass();
                    switch (connectionClass)
                    {
                        //2G-equivalent
                        case WwanDataClass.Edge:
                        case WwanDataClass.Gprs:
                            return NewWorkType._2G;
                        //3G-equivalent
                        case WwanDataClass.Cdma1xEvdo:
                        case WwanDataClass.Cdma1xEvdoRevA:
                        case WwanDataClass.Cdma1xEvdoRevB:
                        case WwanDataClass.Cdma1xEvdv:
                        case WwanDataClass.Cdma1xRtt:
                        case WwanDataClass.Cdma3xRtt:
                        case WwanDataClass.CdmaUmb:
                        case WwanDataClass.Umts:
                        case WwanDataClass.Hsdpa:
                        case WwanDataClass.Hsupa:
                            return NewWorkType._3G;
                        //4G-equivalent
                        case WwanDataClass.LteAdvanced:
                            return NewWorkType._4G;
                        //not connected
                        case WwanDataClass.None:
                            return NewWorkType.None;
                        //unknown
                        case WwanDataClass.Custom:
                        default:
                            return NewWorkType.WIFI;
                    }
                }
                return NewWorkType.None;
            }
            catch (Exception)
            {
                return NewWorkType.None; //as default
            }

        }
    }
}
