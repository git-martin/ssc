using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SSCWeb.Common.Models;

namespace SSCWeb.Common
{
    public static class BizBase
    {
        public static readonly ILog log = LogManager.GetLogger(typeof (BizBase));

        public static Dictionary<int, List<int>> DicBet = new Dictionary<int, List<int>>
        {
            {0, new List<int>() {1,3,6,9} },
            {1, new List<int>() {2, 3, 4, 7, 9}},
            {2, new List<int>() {1, 3, 4, 7, 9}},
            {3, new List<int>() {6, 9, 2, 1, 8, 7}},
            {4, new List<int>() {5, 6, 1, 7, 8, 9}},
            {5, new List<int>() {6, 4, 7, 8}},
            {6, new List<int>() {3, 9, 4, 5, 8}},
            {7, new List<int>() {5, 8, 9, 4, 1, 0}},
            {8, new List<int>() {5, 7, 3, 4, 1}},
            {9, new List<int>() {3, 6, 1, 4, 7}}
        };

        public static bool NotBet(List<IssueModel> data, int preOpenCodeIndex)
        {
            int preGe = data[preOpenCodeIndex].Ge;
            int preShi = data[preOpenCodeIndex].Shi;
            if (preGe == 0 
                || preShi == 0 
                || preGe == preShi 
                )
            {
                return true;
            }
            return false;
        }

        public static bool NotBetHistory(List<IssueModel> data, int preOpenCodeIndex)
        {
            int preGe = data[preOpenCodeIndex].Ge;
            int preShi = data[preOpenCodeIndex].Shi;
            if (
            //preGe == 0
            //    || preShi == 0
                preGe == preShi
                || Math.Abs(preShi - preGe) > 3
                || preShi == 4
                || preGe == 4)
            {
                return true;
            }
            return false;
        }
    }
}
