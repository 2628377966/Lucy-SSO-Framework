using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lucy.Tools
{
    public class ResultEx
    {
        /// <summary>
        /// 是否操作成功
        /// </summary>
        public bool flag { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>  
        public object data { get; set; }  
        public ResultEx()
        {
            this.flag = true;
            this.msg = string.Empty;
        }
        /// <summary>
        /// 创建一个ResultEx 返回正确
        /// </summary>
        /// <returns></returns>
        public static ResultEx Init()
        {
            return Init(true, string.Empty);
        }
        public static ResultEx Init(object data)
        {
            return Init(true, string.Empty, data);
        }
        /// <summary>
        /// 创建一个ResultEx 返回false
        /// </summary>
        /// <param name="msg">提示的消息</param>
        /// <returns></returns>
        public static ResultEx Init(string msg)
        {
            return Init(false, msg);
        }
        /// <summary>
        /// 创建一个ResultEx 返回false
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ResultEx Init(Exception ex)
        {
            return Init(false, ex.Message);
        }
        /// <summary>
        /// 创建一个ResultEx
        /// </summary>
        /// <param name="flag">是否正确</param>
        /// <param name="msg">提示的消息</param>
        /// <returns></returns>
        public static ResultEx Init(bool flag, string msg)
        {
            return new ResultEx() { flag = flag, msg = msg };
        }
        /// <summary>
        /// 创建一个ResultEx
        /// </summary>
        /// <param name="flag">是否正确</param>
        /// <param name="msg">提示的消息</param>
        /// <returns></returns>
        public static ResultEx Init(bool flag)
        {
            return new ResultEx() { flag = flag };
        }
        /// <summary>
        /// 创建一个ResultEx
        /// </summary>
        /// <param name="flag">是否正确</param>
        /// <param name="msg">提示的消息</param>
        /// <returns></returns>
        public static ResultEx Init(bool flag, string msg, object data)
        {
            return new ResultEx() { flag = flag, msg = msg, data = data };
        }

        #region 返回json
        /// <summary>
        /// 返回Json
        /// </summary>
        /// <returns></returns>
        public JsonResult ToJson()
        {
            return this.ToJson(null, null, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 返回Json
        /// </summary>
        /// <param name="contentType">The content type (MIME type).</param>
        /// <returns></returns>
        public JsonResult ToJson(string contentType)
        {
            return this.ToJson(contentType, null, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 返回Json
        /// </summary>
        /// <param name="behavior"> The JSON request behavior</param>
        /// <returns></returns>
        public JsonResult ToJson(JsonRequestBehavior behavior)
        {
            return this.ToJson(null, null, behavior);
        }
        /// <summary>
        /// 返回Json
        /// </summary>
        /// <param name="contentType">The content type (MIME type).</param>
        /// <param name="contentEncoding">The content encoding.</param>
        /// <returns></returns>
        public JsonResult ToJson(string contentType, Encoding contentEncoding)
        {
            return this.ToJson(contentType, contentEncoding, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 返回Json
        /// </summary>
        /// <param name="contentType">The content type (MIME type).</param>
        /// <param name="behavior"> The JSON request behavior</param>
        /// <returns></returns>
        public JsonResult ToJson(string contentType, JsonRequestBehavior behavior)
        {
            return this.ToJson(contentType, null, behavior);
        }
        /// <summary>
        /// 返回Json
        /// </summary>
        /// <param name="contentType">The content type (MIME type).</param>
        /// <param name="contentEncoding">The content encoding.</param>
        /// <param name="behavior"> The JSON request behavior</param>
        /// <returns></returns>
        public JsonResult ToJson(string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            JsonResult result = new JsonResult();
            result.Data = this;
            result.ContentType = contentType;
            result.ContentEncoding = contentEncoding;
            result.JsonRequestBehavior = behavior;
            return result;
        }
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}
