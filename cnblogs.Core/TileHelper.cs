using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace CnBlogs.Core
{
    public class TileHelper
    {
        public const string TEXT = "text";
        public static void UpdateBlogTile(string title, string content)
        {
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueueForWide310x150(true);
            updater.EnableNotificationQueueForSquare150x150(true);
            updater.EnableNotificationQueue(true);
            updater.Clear();
            // Create a tile notification for each feed item.
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text03);
            string titleText = title ?? string.Empty;
            tileXml.GetElementsByTagName(TEXT)[0].InnerText = titleText;
            // Create a new tile notification.
            updater.Update(new TileNotification(tileXml));
            tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text03);
            titleText = title ?? string.Empty;
            tileXml.GetElementsByTagName(TEXT)[0].InnerText = titleText;
            // Create a new tile notification.
            updater.Update(new TileNotification(tileXml));

        }
        public static void UpdateNewsTile(string title, string content,string imageSrc)
        {
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();
            // Create a tile notification for each feed item.
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310ImageAndTextOverlay01);

            string titleText = title ?? string.Empty;
            tileXml.GetElementsByTagName(TEXT)[0].InnerText = titleText;
            tileXml.GetElementsByTagName(TEXT)[0].InnerText = titleText;
            // Create a new tile notification.
            updater.Update(new TileNotification(tileXml));

        }
    }
}
