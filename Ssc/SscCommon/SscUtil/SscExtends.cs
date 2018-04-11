using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SscCommon.SscUtil
{
    public static class ElevenX5Extends
    {
        public static bool IsValidNumbers(this List<int> numList)
        {
            return numList.TrueForAll(IsNumberValid);
        }
        public static bool ContainsAllNo(this List<int> numList, List<int>  subNumList)
        {
            return subNumList.All(i => numList.Contains(i));
        }

        public static bool IsValidNumber(this int num)
        {
            return IsNumberValid(num);
        }
        public static bool IsValidNumber(this string num)
        {
            if (String.IsNullOrWhiteSpace(num))
            {
                return false;
            }
            int no = 0;
            if (!int.TryParse(num, out no))
            {
                return false;
            }
            if (!no.IsValidNumber())
            {
                return false;
            }
            return true;
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
      
        public static bool IsValidIssue(this string issue)
        {
            return IsIssueValie(issue);
        }
        public static bool IsValidIssue(this int issue)
        {
            return IsIssueValie(issue.ToString());
        }

        #region private methond
        private static bool IsNumberValid(int num)
        {
            return SscConst.BetNumbers.Contains(num);
        }
        private static bool IsIssueValie(string issue)
        {
            if (string.IsNullOrWhiteSpace(issue))
                return false;
            //if (issue.Length != 8)
            //    return false;
            //DateTime date = DateTime.MinValue;
            //if (!DateTime.TryParseExact(issue.Substring(0, 6), "yyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
            //{
            //    return false;
            //}
            int issueFlag = 0;
            if (!int.TryParse(issue, out issueFlag))
            {
                return false;
            }
            if (issueFlag < 1 || issueFlag > 84)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
