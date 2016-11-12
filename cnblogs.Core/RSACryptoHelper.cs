using System;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace CnBlogs.Core
{
    public class RSACryptoHelper
    {
        public const string PublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCp0wHYbg/NOPO3nzMD3dndwS0MccuMeXCHgVlGOoYyFwLdS24Im2e7YyhB0wrUsyYf0/nhzCzBK8ZC9eCWqd0aHbdgOQT6CuFQBMjbyGYvlVYU2ZP7kG9Ft6YV6oc9ambuO7nPZh+bvXH0zDKfi02prknrScAKC0XhadTHT3Al0QIDAQAB";
        public static string Encrypt(string data)
        {
            byte[] bytes = Encrypt(PublicKey, data);
            return Convert.ToBase64String(bytes);
        }
        public static byte[] Encrypt(string publicKey, string data)
        {
            //从base64获取keybuffer
            IBuffer keyBuffer = CryptographicBuffer.DecodeFromBase64String(publicKey);
            //1024位rsa加密
            AsymmetricKeyAlgorithmProvider asym = AsymmetricKeyAlgorithmProvider.OpenAlgorithm(AsymmetricAlgorithmNames.RsaPkcs1);
            //导入公钥
            CryptographicKey key = asym.ImportPublicKey(keyBuffer);
            //加密数据转化
            IBuffer plainBuffer = CryptographicBuffer.ConvertStringToBinary(data, BinaryStringEncoding.Utf8);
            //加密
            IBuffer encryptedBuffer = CryptographicEngine.Encrypt(key, plainBuffer, null);

            byte[] encryptedBytes;
            CryptographicBuffer.CopyToByteArray(encryptedBuffer, out encryptedBytes);
            return encryptedBytes;
        }
    }
}
