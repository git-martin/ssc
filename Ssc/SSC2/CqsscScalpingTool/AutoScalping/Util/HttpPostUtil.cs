using System;
using System.IO;
using System.Net;
using System.Text;

namespace AutoScalping.Util
{
    public class HttpPostUtil: HttpBase
    {
        public static string SendRequest(string postData,string url)
        {
            return SendRequest(postData, url, string.Empty);
        }

        public static string SendRequest(string postData, string url,string token)
        {
            if (String.IsNullOrEmpty(url))
                throw new Exception("服务端地址未配置，请通知管理员！");
            try
            {
                var objUri = new Uri(url);
                WebRequest httpRequest = HttpWebRequest.Create(objUri);
                httpRequest.Method = "POST";
         
                httpRequest.ContentType = "application/json";
                if (!string.IsNullOrEmpty(token))
                {
                    httpRequest.Headers.Add("token", token);
                }
                var arrRequest = Encoding.UTF8.GetBytes(postData);
                httpRequest.ContentLength = arrRequest.Length;
                var requestStream = httpRequest.GetRequestStream();
                requestStream.Write(arrRequest, 0, arrRequest.Length);
                requestStream.Close();
                var httpResponse = httpRequest.GetResponse();
                var responseStream = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8);
                var responseJson = responseStream.ReadToEnd();
                responseStream.Close();

                //GlobalParams.TimeOutTimes = 0;
                return responseJson;
            }
            catch (Exception ex)
            {
                //GlobalParams.TimeOutTimes++;
                throw ex;
            }
        }
    }
}
