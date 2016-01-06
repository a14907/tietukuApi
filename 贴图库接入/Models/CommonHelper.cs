using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 贴图库接入.Models
{
    public class CommonHelper
    {
        #region 将一个字符串转化为HMACSHA1加密的字符串+static string GetSha1String(string jsonStr, out string encodedParam)
        /// <summary>
        /// 将一个字符串转化为HMACSHA1加密的字符串
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <param name="encodedParam"></param>
        /// <returns></returns>
        public static string GetSha1String(string jsonStr, out string encodedParam)
        {
            encodedParam = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonStr));
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            byte[] bytes = encoding.GetBytes(encodedParam);
            HMACSHA1 transform = new HMACSHA1(encoding.GetBytes(ConfigHelper.GetSecretKey()));
            CryptoStream stream = new CryptoStream(Stream.Null, transform, CryptoStreamMode.Write);
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            return Convert.ToBase64String(transform.Hash);

        }
        #endregion

        #region 将本地时间按转换为unix时间戳+static long DateTimeToUnixTimestamp(DateTime time)
        /// <summary>
        /// 将本地时间按转换为unix时间戳（到1970年一月一号的秒数）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long DateTimeToUnixTimestamp(DateTime time)
        {
            DateTime dUnix = new DateTime(1970, 1, 1, 0, 0, 0, time.Kind);
            TimeSpan ts = time - dUnix;
            return Convert.ToInt64(ts.TotalSeconds);
        }

        #endregion

        #region 将对象转换为json字符串+static string ToJson(object obj)
        /// <summary>
        /// 将对象转换为json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(obj);
        }
        #endregion

        #region 拼接Token+static string GetToken(string encodedSign, string encodedParam)
        /// <summary>
        /// 拼接Token
        /// </summary>
        /// <param name="encodedSign"></param>
        /// <param name="encodedParam"></param>
        /// <returns></returns>
        public static string GetToken(string encodedSign, string encodedParam)
        {
            return ConfigHelper.GetAccessKey() + ':' + encodedSign + ':' + encodedParam;
        }
        #endregion
    }
}
