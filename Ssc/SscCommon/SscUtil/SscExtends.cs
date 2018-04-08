using System.Collections.Generic;
using System.Linq;

namespace SscCommon.SscUtil
{
    public static class SscExtends
    {
        public static bool IsValidSscNumbers(this List<int> numList)
        {
            return numList.TrueForAll(IsNumberValid);
        }
        public static bool IsValidSscNumber(this int num)
        {
            return IsNumberValid(num);
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
        private static bool IsNumberValid(int num)
        {
            return SscConst.BetNumbers.Contains(num);
        }
    }
}
