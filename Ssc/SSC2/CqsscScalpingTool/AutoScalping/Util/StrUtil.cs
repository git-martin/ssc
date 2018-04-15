using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoScalping.Util
{
    public class StrUtil
    {
        public static string ConvertListToCommerString<T>(List<T> lst)
        {
            if (lst == null || lst.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (T t in lst)
            {
                sb.Append(t.ToString() + ",");
            }
            return sb.ToString().TrimEnd(',');
        }
        public static string ConvertListToString<T>(List<T> lst)
        {
            if (lst == null || lst.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (T t in lst)
            {
                sb.Append(t.ToString());
            }
            return sb.ToString();
        }

        //public static string ConvertBeglongsString(GloableConstants.AccountBelongs accountBelongs)
        //{
        //    if (lst == null || lst.Count == 0)
        //        return string.Empty;
        //    StringBuilder sb = new StringBuilder();
        //    foreach (T t in lst)
        //    {
        //        sb.Append(t.ToString() + ",");
        //    }
        //    return sb.ToString().TrimEnd(',');
        //}
    }
}
