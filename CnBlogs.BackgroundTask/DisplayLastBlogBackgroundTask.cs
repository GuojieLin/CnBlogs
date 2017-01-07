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
            if (BackgroundTaskHelper.IsBackgroundTaskRegistered(nameof(DisplayLastBlogBackgroundTask)))
            {
                // Background task already registered.
                return;
            }
            //这里就是磁贴更新周期的一些逻辑处理
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                BackgroundTaskHelper.Register(nameof(DisplayLastBlogBackgroundTask), new TimeTrigger(15, false));
                //下面这句注册会失败，然后会闪退。原因未知。
                //BackgroundTaskHelper.Register(nameof(DisplayLastBlogBackgroundTask), 
                //    typeof(DisplayLastBlogBackgroundTask).FullName,
                //    new TimeTrigger(15, false), false, true, new SystemCondition(SystemConditionType.InternetAvailable));


                //UnRegister();

                //BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder();
                //taskBuilder.Name = nameof(DisplayLastBlogBackgroundTask);
                //taskBuilder.TaskEntryPoint = typeof(DisplayLastBlogBackgroundTask).FullName;
                //taskBuilder.SetTrigger(new TimeTrigger(15, false));
                //var registration = taskBuilder.Register();
            }
        }
        private static bool UnRegister()
        {
            try
            {

                BackgroundTaskHelper.Unregister(nameof(DisplayLastBlogBackgroundTask));
                //foreach (var task in BackgroundTaskRegistration.AllTasks)
                //{
                //    if (task.Value.Name == nameof(DisplayLastBlogBackgroundTask))
                //    {
                //        task.Value.Unregister(true);
                //    }
                //}
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
