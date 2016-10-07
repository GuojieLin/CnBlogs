using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Common.Share
{
    public class ShareHelper
    {
        public const string AppId= "987654321";
        public const string StoreLogoUrl = "ms-appx:///Assets/StoreLogo.png";
        private static WeChatRequest CreateWeChatRequest()
        {
            WeChatRequest req = new WeChatRequest(AppId);
            return req;
        }
        public static async void Share2Timeline(string shareUrl,string title)
        {
            byte[] pic = await LoadStoreLogo();
            WeChatRequest req = CreateWeChatRequest();
            req.WebPageShare2TimelineRequest(shareUrl, title, pic);
        }
        public static async void Share2UserChoose(string shareUrl, string title)
        {
            byte[] pic = await LoadStoreLogo();
            
            WeChatRequest req = CreateWeChatRequest();
            req.WebPageShareByUserChooseRequest(shareUrl, title, pic);
        }
        public static async void Share2Session(string shareUrl, string title)
        {
            byte[] pic = await LoadStoreLogo();
            WeChatRequest req = CreateWeChatRequest();
            req.WebPageShare2SessionRequest(shareUrl, title, pic);
        }
        private static async Task<byte[]> LoadStoreLogo()
        {
            var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(StoreLogoUrl));
            byte[] pic;
            using (var stream = await file.OpenReadAsync())
            {
                pic = new byte[stream.Size];
                await stream.AsStream().ReadAsync(pic, 0, pic.Length);
            }
            return pic;
        }

    }
}
