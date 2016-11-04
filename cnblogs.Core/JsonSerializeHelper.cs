using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace CnBlogs.Core
{
    public class JsonSerializeHelper
    {
        /// <summary>
        /// json序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string Serialize<T>(T entity)
        {
            DataContractJsonSerializer dcSerializer = new DataContractJsonSerializer(typeof(T));

            MemoryStream stream = new MemoryStream();

            dcSerializer.WriteObject(stream, entity);

            string data = Encoding.UTF8.GetString(stream.ToArray(), 0, (int)stream.Length);

            return data;
        }

        public static T Deserialize<T>(string json)
        {
            DataContractJsonSerializer dcSerializer = new DataContractJsonSerializer(typeof(T));

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            return (T)dcSerializer.ReadObject(stream);
        }
    }
}
