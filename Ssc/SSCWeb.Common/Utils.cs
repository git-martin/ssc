using SSCWeb.Common.Model;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SSCWeb.App_Code
{
    public class Utils
    {
        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            myStreamWriter.Write(postDataStr);
            //myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            //myResponseStream.Close();

            return retString;
        }
    }

    public class IssueComparer : IComparer<IssueModel>
    {
        //实现姓名升序
        public int Compare(IssueModel x, IssueModel y)
        {
            return (x.Issue.CompareTo(y.Issue));
        }
    }

    public class IssueComparerDesc : IComparer<IssueModel>
    {
        //实现姓名升序
        public int Compare(IssueModel x, IssueModel y)
        {
            return (y.Issue.CompareTo(x.Issue));
        }
    }
    //
}
