using CnBlogs.Core.Extentsions;
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
        public static void PushToastNotification(string id, string title, string imagePath)
        {
            ToastTemplateType toastTemplate;

            toastTemplate = imagePath.IsNullOrEmpty() ?
                ToastTemplateType.ToastText01:
                ToastTemplateType.ToastImageAndText01 ;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName(TEXT);
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(title));

            if (!imagePath.IsNullOrEmpty())
            {
                XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName(IMAGE);
                ((XmlElement)toastImageAttributes[0]).SetAttribute("src", $"Ms-appdata:///temp/{Constants.Configuration.ImageTempDirectory}/{imagePath}");
                ((XmlElement)toastImageAttributes[0]).SetAttribute("alt", "image");
            }
            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toastNode).SetAttribute("duration", "long");
            ((XmlElement)toastNode).SetAttribute("launch", $"{{\"type\":\"toast\",\"id\":\"{id}\"}}");

            ToastNotification toast = new ToastNotification(toastXml);

            ToastNotificationManager.CreateToastNotifier().Show(toast);


        }
    }
}
