using CnBlogs.Core;
using CnBlogs.Entities;
using CnBlogs.Service;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace CnBlogs.BackgroundTask
{
    public sealed class DisplayLastBlogBackgroundTask : IBackgroundTask
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
            List<Blog> blog = await BlogService.GetSiteHomeArticlesAsync(1, 1);
            var lastBlog = blog.FirstOrDefault();
            if (lastBlog != null)
            {
                //更新磁铁
                TileHelper.UpdateBlogTile(lastBlog.Title, lastBlog.Summary);
            }
            //
            _deferral.Complete();
        }
        
        public async static void Register()
        {
            //这里就是磁贴更新周期的一些逻辑处理
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                BackgroundTaskHelper.Register(typeof(DisplayLastBlogBackgroundTask), new TimeTrigger(15, false),
                    false, true,
                    new SystemCondition(SystemConditionType.InternetAvailable), //网络可用
                    new SystemCondition(SystemConditionType.UserPresent));//用户正在使用时
            }
        }
    }
}
