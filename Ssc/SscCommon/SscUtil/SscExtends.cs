using System.Collections.Generic;

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
