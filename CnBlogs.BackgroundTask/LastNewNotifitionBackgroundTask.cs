using CnBlogs.Core;
using CnBlogs.Core.Extentsions;
using CnBlogs.Entities;
using CnBlogs.Service;
using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace CnBlogs.BackgroundTask
{
    public sealed class LastNewNotifitionBackgroundTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral; // Note: defined at class scope so we can mark it complete inside the OnCancel() callback if we choose to support cancellation
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            //
            // TODO: Insert code to start one or more asynchronous methods using the
            //       await keyword, for example:
            //
            // await ExampleMethodAsync();
            //
            //获取第一条博客
            List<News> news = await NewsService.GetHotNewsDataArticlesAsync(1);
            var lastNews = news.LastOrDefault();
            bool needPostNotifition = NeedPostNotifition(lastNews);
            if (needPostNotifition) //推送过，且未包含感兴趣的新闻,则通知
            {
                //更新磁铁
                string url = (await NewsService.GetNewsBodyAsync(lastNews.Id)).ImageUrl;
                //可能有多个图片用;分割，且以//开头，去掉//
                //images2015.cnblogs.com/news/66372/201701/66372-20170105220153644-24320897.jpg;
                if (!url.IsNullOrEmpty()) url = "http://" + url.Split(';')[0].TrimStart('/');
                //string locaFileName = await ImageStorageHelper.GetLocalImageName(url);
                //if (locaFileName.IsNullOrEmpty())
                //{
                //    //不存在缓存时才通知
                //    url = await Core.HttpHelper.DownloadImage(url);
                //}
                string command = new QueryString()
                {
                    { "action", "HotNews" },
                    { "queryString", JsonSerializeHelper.Serialize(lastNews)}
                }.ToString();
                ToastNotificationHelper.PushToastNotification(command,lastNews.Id, lastNews.Title, lastNews.Summary, lastNews.TopicIcon, url);
                //加入最后推送缓存
                RoamingSetting.Current.SetSetting("LastNotifitionNewsId", lastNews.Id);
            }
            //
            _deferral.Complete();
        }

        private bool NeedPostNotifition(News lastNews)
        {
            if (lastNews == null ||
                (!lastNews.Title.Contains("微软") &&
                lastNews.Views < 1000))  //浏览量小于1000 而且不感兴趣 则不推送
                return false;
            string lastNotifitionNewsId;
            if (RoamingSetting.Current.GetSetting("LastNotifitionNewsId", out lastNotifitionNewsId))
            {
                return lastNotifitionNewsId.IsNullOrEmpty() || lastNotifitionNewsId != lastNews.Id;
            }
            return true;
        }

        public async static void Register()
        {
            //这里就是磁贴更新周期的一些逻辑处理
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                BackgroundTaskHelper.Register(typeof(LastNewNotifitionBackgroundTask), new TimeTrigger(60, false),
                    false, true, new SystemCondition(SystemConditionType.InternetAvailable), new SystemCondition(SystemConditionType.UserPresent));
                
            }
        }
    }
}
