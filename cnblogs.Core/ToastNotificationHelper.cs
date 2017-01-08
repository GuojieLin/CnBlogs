using CnBlogs.Core.Constants;
using CnBlogs.Core.Extentsions;
using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace CnBlogs.Core
{
    //https://msdn.microsoft.com/zh-cn/windows/uwp/controls-and-patterns/tiles-and-notifications-adaptive-interactive-toasts
    public class ToastNotificationHelper
    {
        public const string TEXT = "text";
        public const string IMAGE = "image";
        public static void PushToastNotification(string command,string id, string title,string content, string logoUri,string imagePath)
        {
            ToastContent toastContent = new ToastContent()
            {
                Launch = command,
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = title
                            },

                            new AdaptiveText()
                            {
                                Text = content,
                            },
                            new AdaptiveImage()
                            {
                                Source = imagePath
                            }
                        },

                        AppLogoOverride = new ToastGenericAppLogo()
                        {
                            Source = logoUri,
                            HintCrop = ToastGenericAppLogoCrop.Circle
                        }
                    }
                }
            };
            ToastNotification toast = new ToastNotification(toastContent.GetXml());
            //一天没处理则超时
            toast.ExpirationTime = DateTime.Now.AddDays(1);
            toast.Group = "HotNews";
            ToastNotificationManager.CreateToastNotifier().Show(toast);

        }
    }
}
