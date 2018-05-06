using System.Collections.Generic;

namespace SSCWeb.Common.Utils
{
    public static class UtilExtends
    {

        public static string IntListToString(this List<int> list)
        {
            var s = "";
            foreach (int i in list)
            {
                s += i;
            }
            return s;
        }
        public static int ToInt(this string num)
        {
            return int.Parse(num);
        }

        public static List<string> ToStringList(this List<int> numList)
        {
            var result = new List<string>();
            if (numList != null && numList.Count>0)
            {
                foreach (var i in numList)
                {
                    if (i < 10)
                    {
                        result.Add("0" + i);
                    }
                    else
                    {
                        result.Add("" + i);
                    }
                }
            }
            return result;
        }
        public static string ToSpliteString(this List<int> numList)
        {
            var result = "";
            if (numList != null && numList.Count > 0)
            {
                foreach (var i in numList)
                {
                    if (i < 10)
                    {
                        result += "0" + i;
                    }
                    else
                    {
                        result += i;
                    }
                    result += ",";
                }
            }
            if (result.EndsWith(","))
                result = result.TrimEnd(',');
            return result;
        }

        public static long ToLong(this string num)
        {
            return long.Parse(num);
        }
    }
}
