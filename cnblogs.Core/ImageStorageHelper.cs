using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;

namespace CnBlogs.Core
{
    public class ImageStorageHelper
    {
        private static string RenameByMD5(string str)
        {
            HashAlgorithmProvider hashAlgorithm =
                 HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            CryptographicHash cryptographic = hashAlgorithm.CreateHash();
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            cryptographic.Append(buffer);
            return CryptographicBuffer.EncodeToHexString(cryptographic.GetValueAndReset());
        }
        public static async Task<string> StorageImageFolder(IRandomAccessStream stream, string uri)
        {
            StorageFolder folder = await GetImageFolder();
            string image = RenameByMD5(uri) + ".png";
            StorageFile file = await folder.CreateFileAsync(image);
            await FileIO.WriteBytesAsync(file, await ConvertIRandomAccessStreamByte(stream));
            return file.Name;
        }

        private static async Task<byte[]> ConvertIRandomAccessStreamByte(IRandomAccessStream stream)
        {
            DataReader read = new DataReader(stream.GetInputStreamAt(0));
            await read.LoadAsync((uint)stream.Size);
            byte[] temp = new byte[stream.Size];
            read.ReadBytes(temp);
            return temp;
        }

        private static async Task<StorageFolder> GetImageFolder()
        {
            //文件夹
            StorageFolder folder = null;
            //从本地获取文件夹
            try
            {
                folder = await ApplicationData.Current.TemporaryFolder.GetFolderAsync(Constants.Configuration.ImageTempDirectory);
            }
            catch (FileNotFoundException)
            {
                //没找到
                folder = await ApplicationData.Current.TemporaryFolder.CreateFolderAsync(Constants.Configuration.ImageTempDirectory);
            }
            return folder;
        }

        public static async Task<string> GetLocalImageName(string url)
        {
            try
            {
                StorageFolder folder = await GetImageFolder();
                string image = RenameByMD5(url) + ".png";
                StorageFile file = await folder.GetFileAsync(image);
                return file.Name;
            }
            catch
            {
                return null;
            }
        }
    }
}
