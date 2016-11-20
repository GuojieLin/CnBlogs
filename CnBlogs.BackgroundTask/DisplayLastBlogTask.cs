using CnBlogs.Core;
using CnBlogs.Entities;
using CnBlogs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace CnBlogs.BackgroundTask
{
    public sealed class DisplayLastBlogTask : IBackgroundTask
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
                UnRegister();
            
                BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder();
                taskBuilder.Name = nameof(DisplayLastBlogTask);
                taskBuilder.TaskEntryPoint = typeof(DisplayLastBlogTask).FullName;
                taskBuilder.SetTrigger(new TimeTrigger(15, false));
                var registration = taskBuilder.Register();
            }
        }
        private static bool UnRegister()
        {
            try
            {
                foreach (var task in BackgroundTaskRegistration.AllTasks)
                {
                    if (task.Value.Name == nameof(DisplayLastBlogTask))
                    {
                        task.Value.Unregister(true);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
