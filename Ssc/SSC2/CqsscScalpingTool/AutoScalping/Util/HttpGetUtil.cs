using System;
using System.IO;
using System.Net;
using System.Text;

namespace AutoScalping.Util
{
    public class HttpGetUtil: HttpBase
    {
        public static string GetResponse(string url)
        {
            return SendRequest(url, string.Empty);
        }

        public static string SendRequest(string url,string token)
        {
            if (String.IsNullOrEmpty(url))
                throw new Exception("服务端地址未配置，请通知管理员！");
            try
            {
                var objUri = new Uri(url);
                WebRequest httpRequest = HttpWebRequest.Create(objUri);
                httpRequest.Method = "GET";
         
                httpRequest.ContentType = "application/json";
                if (!string.IsNullOrEmpty(token))
                {
                    httpRequest.Headers.Add("token", token);
                }
                var httpResponse = httpRequest.GetResponse();
                var responseStream = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8);
                var responseJson = responseStream.ReadToEnd();
                responseStream.Close();

                return responseJson;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
