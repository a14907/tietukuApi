using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贴图库接入.Models
{
    public class ConfigHelper
    {
        #region 根据key取得配置值+static string GetAppSettingByKey(string key)
        /// <summary>
        /// 根据key取得配置值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettingByKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        #endregion
        #region 取得AccessKey+static string GetAccessKey()
        /// <summary>
        /// 取得AccessKey
        /// </summary>
        /// <returns></returns>
        public static string GetAccessKey()
        {
            return GetAppSettingByKey("AccessKey");
        }
        #endregion

        #region  取得SecretKey+static string GetSecretKey()
        /// <summary>
        /// 取得SecretKey
        /// </summary>
        /// <returns></returns>
        public static string GetSecretKey()
        {
            return GetAppSettingByKey("SecretKey");
        }
        #endregion

        #region 取得OpenKey+ static string GetOpenKey()
        /// <summary>
        /// 取得OpenKey
        /// </summary>
        /// <returns></returns>
        public static string GetOpenKey()
        {
            return GetAppSettingByKey("OpenKey");
        }
        #endregion
    }
}
