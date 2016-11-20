using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace CnBlogs.Core
{
    public enum NetWorkType
    {
        [Display(Name = "无网络")]
        None,
        [Display(Name = "2G")]
        _2G,
        [Display(Name = "3G")]
        _3G,
        [Display(Name = "4G")]
        _4G,
        [Display(Name = "Wifi")]
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
        
        private NetWorkType _network;

        public NetWorkType Network
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
            NetworkStatusChanged?.Invoke(this);
        }

        private NetWorkType GetNewWorkType()
        {
            try
            {
                ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();

                if (profile.IsWlanConnectionProfile)
                {
                    return NetWorkType.WIFI;
                }
                if (profile.IsWwanConnectionProfile)
                {
                    WwanDataClass connectionClass = profile.WwanConnectionProfileDetails.GetCurrentDataClass();
                    switch (connectionClass)
                    {
                        //2G-equivalent
                        case WwanDataClass.Edge:
                        case WwanDataClass.Gprs:
                            return NetWorkType._2G;
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
                            return NetWorkType._3G;
                        //4G-equivalent
                        case WwanDataClass.LteAdvanced:
                            return NetWorkType._4G;
                        //not connected
                        case WwanDataClass.None:
                            return NetWorkType.None;
                        //unknown
                        case WwanDataClass.Custom:
                        default:
                            return NetWorkType.WIFI;
                    }
                }
                return NetWorkType.None;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
                return NetWorkType.None; //as default
            }

        }
    }
}
