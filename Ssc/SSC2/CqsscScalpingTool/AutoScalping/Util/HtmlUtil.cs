using System.Web;

namespace AutoScalping.Util
{
    public class HtmlUtil
    {
        /// <summary>
        /// HTML解码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string HtmlDecode(string source)
        {
            return HttpUtility.HtmlDecode(source);
        }

        /// <summary>
        /// HTML编码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string HtmlEncode(string source)
        {
            return HttpUtility.HtmlEncode(source);
        }

        /// <summary>
        /// URL解码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }
    }
}
