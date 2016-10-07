using CnBlogs.Common.Share;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CnBlogs.UserControls
{
    public sealed partial class ShareDialog : ContentDialog
    {
        public static readonly ShareDialog Default = new ShareDialog();
        private string _url;
        private string _title;
        private ShareDialog()
        {
            this.InitializeComponent();
        }
        public void Init(string url, string title)
        {
            this._url = url;
            this._title = title;
        }

        private void ShareByWeChatAppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ShareHelper.Share2UserChoose(this._url, this._title);
            this.Hide();
        }
        /// <summary>
        /// 复制链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ShareByCopyUriAppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(this._url);
            Clipboard.SetContent(dataPackage);
            MessageDialog messageDialog = new MessageDialog("复制成功");
            IUICommand uiCommand = await messageDialog.ShowAsync();
            this.Hide();
        }
        /// <summary>
        /// 其他app处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShareByMoreWayAppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareLinkHandler);
            DataTransferManager.ShowShareUI();
            this.Hide();
        }
        

        private void ShareLinkHandler(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.Properties.Title = "分享博客[来自 博客园UWP版]";
            request.Data.Properties.Description = "向好友分享这篇博客";
            request.Data.SetWebLink(new Uri(_url));
        }
    }
}
