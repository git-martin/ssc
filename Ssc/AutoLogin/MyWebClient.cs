using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace AutoLogin
{
    /// <summary>
    /// 支持 Session 和 Cookie 的 WebClient。
    /// </summary>
    public class MyWebClient : WebClient
    {
        // Cookie 容器
        private CookieContainer _cookieContainer;
        private NameValueCollection _cookieList;
        private readonly int _timeoutSeconds = 30;
        public string Token { get; set; }
        public int SiteId { get; set; }
        public long KeywordsId { get; set; }
        public string Keyword { get; set; }
        public List<string> ItemIds { get; set; }
        public List<long> SouceIds { get; set; }

        /// <summary>
        /// 创建一个新的 WebClient 实例。
        /// </summary>
        public MyWebClient()
        {
            this._cookieContainer = new CookieContainer();
            this._cookieList = new NameValueCollection();
          
        }

        public MyWebClient(int timeoutSeconds)
        {
            this._cookieContainer = new CookieContainer();
            this._cookieList = new NameValueCollection();
            this._timeoutSeconds = timeoutSeconds;

        }

        /// <summary>
        /// 创建一个新的 WebClient 实例。
        /// </summary>
        /// <param name="cookies">Cookie 容器</param>
        public MyWebClient(CookieContainer cookies)
        {
            this._cookieContainer = cookies;
        }

        /// <summary>
        /// Cookie 容器
        /// </summary>
        public CookieContainer Cookies
        {
            get { return this._cookieContainer; }
            set { this._cookieContainer = value; }
        }

        public NameValueCollection CookieList
        {
            get { return this._cookieList; }
            set { this._cookieList = value; }
        }

        /// <summary>
        /// 返回带有 Cookie 的 HttpWebRequest。
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                var httpRequest = request as HttpWebRequest;
                httpRequest.CookieContainer = _cookieContainer;
                httpRequest.Timeout = this._timeoutSeconds*1000;
                httpRequest.ReadWriteTimeout = this._timeoutSeconds * 1000; 
            }
            return request;
        }

        #region 封装了PostData, PostJsonData，GetSrc 和 GetFile 方法

        /// <summary>
        /// 向指定的 URL POST 数据，并返回页面
        /// </summary>
        /// <param name="uriString">POST URL</param>
        /// <param name="uriParameters">POST的数据</param>
        /// <param name="msg"></param>
        /// <returns>页面的源文件</returns>
        public string PostData(string uriString, Dictionary<string, object> uriParameters, out string msg)
        {
            try
            {
                string postString = BuildingUrlParams(uriParameters);
                string cookieString = BuildCookies();
                if (!string.IsNullOrWhiteSpace(cookieString))
                    this.Headers[HttpRequestHeader.Cookie] = cookieString;
                // 将 Post 字符串转换成字节数组
                byte[] postData = Encoding.GetEncoding("UTF-8").GetBytes(postString);
                this.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                // 上传数据，返回页面的字节数组
                byte[] responseData = this.UploadData(uriString, "POST", postData);
                // 将返回的将字节数组转换成字符串(HTML);
                string srcString = Encoding.GetEncoding("UTF-8").GetString(responseData);
                srcString = srcString.Replace("\t", "");
                srcString = srcString.Replace("\r", "");
                srcString = srcString.Replace("\n", "");
                msg = string.Empty;
                return srcString;
            }
            catch (WebException we)
            {
                msg = we.Message;
                return string.Empty;
            }
        }

        public string PostJsonData(string uriString, string jsonParameters, out string msg)
        {
            try
            {
                string postString = jsonParameters;
                string cookieString = BuildCookies();
                if (!string.IsNullOrWhiteSpace(cookieString))
                    this.Headers[HttpRequestHeader.Cookie] = cookieString;
                // 将 Post 字符串转换成字节数组
                byte[] postData = Encoding.GetEncoding("UTF-8").GetBytes(postString);
                this.Headers.Add("Content-Type", "application/json");
                this.Headers.Add("User-Agent","Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.108 Safari/537.36");
                // 上传数据，返回页面的字节数组
                byte[] responseData = this.UploadData(uriString, "POST", postData);
                // 将返回的将字节数组转换成字符串(HTML);
                string srcString = Encoding.GetEncoding("UTF-8").GetString(responseData);
                srcString = srcString.Replace("\t", "");
                srcString = srcString.Replace("\r", "");
                srcString = srcString.Replace("\n", "");
                msg = string.Empty;
                return srcString;
            }
            catch (WebException we)
            {
                msg = we.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 获得指定 URL 的源文件
        /// </summary>
        /// <param name="uriString">页面 URL</param>
        /// <param name="uriParameters">页面 URL的参数</param>
        /// <param name="msg"></param>
        /// <returns>result</returns>
        public string GetSrc(string uriString, Dictionary<string,object> uriParameters,out string msg)
        {
            try
            {
                string parameters = BuildingUrlParams(uriParameters);
                string cookieString = BuildCookies();
                if (!string.IsNullOrWhiteSpace(parameters))
                    uriString += "?" + parameters;
                if (!string.IsNullOrWhiteSpace(cookieString))
                    this.Headers[HttpRequestHeader.Cookie] = cookieString;
                // 返回页面的字节数组
                this.Headers[HttpRequestHeader.Accept] = "application/json, text/javascript, */*; q=0.01";
                byte[] responseData = this.DownloadData(uriString);
                // 将返回的将字节数组转换成字符串(HTML);
                string srcString = Encoding.GetEncoding("UTF-8").GetString(responseData);
                srcString = srcString.Replace("\t", "");
                srcString = srcString.Replace("\r", "");
                srcString = srcString.Replace("\n", "");
                msg = string.Empty;
                AddCookies(this.ResponseHeaders[HttpResponseHeader.SetCookie]);
                return srcString;
            }
            catch (WebException we)
            {
                msg = we.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 获得指定 URL 的源文件
        /// </summary>
        /// <param name="uriString">页面 URL</param>
        /// <param name="uriParameters">页面 URL的参数</param>
        /// <param name="msg"></param>
        /// <param name="queryUrl"></param>
        /// <returns>result</returns>
        public string GetSrc(string uriString, Dictionary<string, object> uriParameters, out string msg,out string queryUrl)
        {
            queryUrl = "";
            try
            {
                string parameters = BuildingUrlParams(uriParameters);
                string cookieString = BuildCookies();
                if (!string.IsNullOrWhiteSpace(parameters))
                    uriString += "?" + parameters;
                if (!string.IsNullOrWhiteSpace(cookieString))
                    this.Headers[HttpRequestHeader.Cookie] = cookieString;
                // 返回页面的字节数组
                this.Headers[HttpRequestHeader.Accept] = "application/json, text/javascript, */*; q=0.01";
                byte[] responseData = this.DownloadData(uriString);
                // 将返回的将字节数组转换成字符串(HTML);
                string srcString = Encoding.GetEncoding("UTF-8").GetString(responseData);
                srcString = srcString.Replace("\t", "");
                srcString = srcString.Replace("\r", "");
                srcString = srcString.Replace("\n", "");
                msg = string.Empty;
                AddCookies(this.ResponseHeaders[HttpResponseHeader.SetCookie]);
                queryUrl = uriString;
                return srcString;
            }
            catch (WebException we)
            {
                msg = we.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// Http同步Get异步请求  
        /// </summary>
        /// <param name="uriString"></param>
        /// <param name="uriParameters"></param>
        /// <param name="callBackDownDataCompleted"></param>
        /// <param name="msg"></param>
        /// <param name="queryUrl"></param>
        public void GetSrcAsync(string uriString, Dictionary<string, object> uriParameters, DownloadDataCompletedEventHandler callBackDownDataCompleted, out string msg, out string queryUrl)
        {
            queryUrl = "";
            msg = "";
            try
            {
                string parameters = BuildingUrlParams(uriParameters);
                string cookieString = BuildCookies();
                if (!string.IsNullOrWhiteSpace(parameters))
                    uriString += "?" + parameters;
                if (!string.IsNullOrWhiteSpace(cookieString))
                    this.Headers[HttpRequestHeader.Cookie] = cookieString;
                // 返回页面的字节数组
                this.Headers[HttpRequestHeader.Accept] = "application/json, text/javascript, */*; q=0.01";
                if (callBackDownDataCompleted != null)
                    this.DownloadDataCompleted += callBackDownDataCompleted;
                this.DownloadDataAsync(new Uri(uriString));
            }
            catch (WebException we)
            {
                msg = we.Message;
            }
            
        }

        

        private string BuildingUrlParams(Dictionary<string, object> urlParameters)
        {
            string result = "";
            if (urlParameters == null || !urlParameters.Any())
                return result;
            foreach (var parameter in urlParameters)
            {
                result += string.Format("{0}={1}&", parameter.Key, parameter.Value);
            }
            result = result.TrimEnd('&');
            return result;
        }
        private void AddCookies(string cookiesString)
        {
            if (string.IsNullOrWhiteSpace(cookiesString))
                return;
            var cookies = cookiesString.Split(';');
            if (!cookies.Any())
                return;
            foreach (var v in cookies)
            {
                var t = v.Split('=');
                if (t.Count() != 2)
                    continue;
                if (!_cookieList.AllKeys.Contains(t[0]))
                    _cookieList.Add(t[0], t[1]);
            }
        }

        private string BuildCookies()
        {
            string result = string.Empty;
            if (_cookieList.Count == 0)
                return result;
            foreach (var cookie in _cookieList.AllKeys)
            {
                result += string.Format("{0}={1};", cookie, _cookieList[cookie]);
            }
            if (!string.IsNullOrWhiteSpace(result))
                result = result.TrimEnd(';');
            return result;
        }
        /// <summary>
        /// 从指定的 URL 下载文件到本地
        /// </summary>
        /// <param name="uriString">文件 URL</param>
        /// <param name="fileName">本地文件的完成路径</param>
        /// <returns></returns>
        public bool GetFile(string urlString, string fileName, out string msg)
        {
            try
            {
                this.DownloadFile(urlString, fileName);
                msg = string.Empty;
                return true;
            }
            catch (WebException we)
            {
                msg = we.Message;
                return false;
            }
        }
        #endregion

        public MyWebClient Copy()
        {
            var client = new MyWebClient(_timeoutSeconds)
            {
                CookieList = this.CookieList,
                Cookies = this.Cookies,
                KeywordsId = this.KeywordsId,
                Token = this.Token??"",
                Keyword = this.Keyword??"",
                SiteId = this.SiteId,
                ItemIds = this.ItemIds??new List<string>(),
                SouceIds = this.SouceIds??new List<long>()
            };
            return client;
        }
    }
}
