using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using 贴图库接入.Models;

namespace 贴图库接入.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取相册列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAlbumList()
        {
            return View();
        }

        /// <summary>
        /// 随机30张推荐的图片
        /// </summary>
        /// <returns></returns>
        public ActionResult Get30RandomPic()
        {
            return View();
        }

        /// <summary>
        /// 查询所有的分类
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllCatalog()
        {
            WebRequest req = WebRequest.Create("http://api.tietuku.com/v2/api/getcatalog");
            req.Method = "post";
            string para = "key=" + ConfigHelper.GetOpenKey() + "&returntype=json";
            byte[] buffer = Encoding.UTF8.GetBytes(para);
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = buffer.Length;
            using (var stream = req.GetRequestStream())
            {
                stream.Write(buffer, 0, buffer.Length);
            }
            string resStr = string.Empty;
            using (var res = req.GetResponse())
            {
                using (var stream = res.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        resStr = reader.ReadToEnd();
                    }
                }
            }
            ViewBag.Data = resStr;
            return View();
        }

        /// <summary>
        /// 全部图片列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPicByCId()
        {
            return View();
        }

        /// <summary>
        /// 查询单张图片详细信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult GetOnePic(int pid)
        {
            WebRequest req = WebRequest.Create("http://api.tietuku.com/v2/api/getonepic");
            string paras = "key=" + ConfigHelper.GetOpenKey() + "&returntype=json&pid=" + pid;
            byte[] buffer = Encoding.UTF8.GetBytes(paras);
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = buffer.Length;
            using (var stream = req.GetRequestStream())
            {
                stream.Write(buffer, 0, buffer.Length);
            }
            string resStr = "";
            using (var res = req.GetResponse())
            {
                using (var stream = res.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        resStr = reader.ReadToEnd();
                    }
                }
            }
            ViewBag.Data = resStr;
            return View();
        }

        [HttpPost]
        public string GetUpToken(int albumId)
        {
            var obj = new { deadline = CommonHelper.DateTimeToUnixTimestamp(DateTime.Now) + 60L, aid = albumId, from = "file" };
            string encodedParam;
            var encodedSign = CommonHelper.GetSha1String(CommonHelper.ToJson(obj), out encodedParam);
            return CommonHelper.GetToken(encodedSign, encodedParam);
        }

        [HttpPost]
        public string GetAlbumToken(int pageIndex)
        {
            var obj = new { key = ConfigHelper.GetOpenKey(), returntype = "JSON", p = pageIndex };
            string encodedParam;
            var encodedSign = CommonHelper.GetSha1String(CommonHelper.ToJson(obj), out encodedParam);
            return CommonHelper.GetToken(encodedSign, encodedParam);
        }

    }
}