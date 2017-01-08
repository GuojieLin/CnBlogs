using CnBlogs.BackgroundTask;
using CnBlogs.Common;
using CnBlogs.Core;
using CnBlogs.Entities;
using CnBlogs.Service;
using CnBlogs.UI;
using Microsoft.QueryStringDotNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CnBlogs
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static string DeviceFamily = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;
        public static NavigationService NavigationService;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }
        public static void InitNavigationService(Frame masterFrame, Frame detailFrame,Frame tertiaryFrame,Type type, bool isNarrow)
        {
            App.NavigationService = new NavigationService(masterFrame, detailFrame, tertiaryFrame, type, isNarrow);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                //this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            var appView = ApplicationView.GetForCurrentView();
            appView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);
            //注册后台任务
            DisplayLastBlogBackgroundTask.Register();
            LastNewNotifitionBackgroundTask.Register();
            //注册cortana命令
            await RegistCortanaVCDFile();

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        private async Task RegistCortanaVCDFile()
        {
            var storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(Constants.VCDFilePath));
            await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager
                .InstallCommandDefinitionsFromStorageFileAsync(storageFile);
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            switch (args.Kind)
            {
                case ActivationKind.ToastNotification:
                    {
                        ProcessToastNotification(args);
                        break;
                    }
                case ActivationKind.VoiceCommand:
                    {
                        ProcessVoiceCommand(args);
                        break;
                    }
            }
            base.OnActivated(args);
        }
        /// <summary>
        /// 点击通知响应
        /// </summary>
        /// <param name="args"></param>
        private void ProcessToastNotification(IActivatedEventArgs args)
        {
            var toastActivationArgs = args as ToastNotificationActivatedEventArgs;
            if (toastActivationArgs != null)
            {
                bool success = ToastificationHandle(toastActivationArgs);
                if (!success)
                {
                    Frame rootFrame = Window.Current.Content as Frame;
                    // If we're loading the app for the first time, place the main page on
                    // the back stack so that user can go back after they've been
                    // navigated to the specific page
                    if (rootFrame.BackStack.Count == 0)
                        rootFrame.BackStack.Add(new PageStackEntry(typeof(MainPage), null, null));
                }
                Window.Current.Activate();
            }
        }
        private void ProcessVoiceCommand(IActivatedEventArgs args)
        {
            var voiceActivationArgs = args as VoiceCommandActivatedEventArgs;
            if (voiceActivationArgs != null)
            {
                var result = voiceActivationArgs.Result;
                var command = result.Text;

                Window.Current.Activate();
            }
        }

        private bool ToastificationHandle(ToastNotificationActivatedEventArgs toastActivationArgs)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            // Parse the query string
            QueryString args = QueryString.Parse(toastActivationArgs.Argument);
            // See what action is being requested 
            switch (args["action"])
            {
                case "HotNews":
                    // The URL retrieved from the toast args
                    string queryString = args["queryString"];
                    News news = JsonSerializeHelper.Deserialize<News>(queryString);
                    //二级Frame才显示该页
                    if (NavigationService.DetailFrame.Content is NewsBodyPage &&
                        (NavigationService.DetailFrame.Content as NewsBodyPage).NewsBodyViewModel.News.Id.Equals(news.Id))
                        break;
                    // Otherwise navigate to view it
                    NavigationService.DetailFrameNavigate(typeof(NewsBodyPage), news);
                    return true;
            }
            //导航失败
            return false;
        }



        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
