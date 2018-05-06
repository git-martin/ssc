using System;
using System.Collections.Generic;
using System.Linq;
using SSCWeb.Common.Utils;

namespace SSCWeb.Common.Models
{
    public class MoniModel
    {
        public int Start { get; set; }
        public long Issue { get; set; }
        public string OpenCode { get; set; }
        public List<int> BetShi { get; set; }
        public List<int> BetGe { get; set; }
        public bool IsZhong { get; set; }
        public bool IsBeted { get; set; }
        public int BetZhuCount { get; set; }

        public string BetZhuMoney => BetZhuCount == 0 ? "" : BetZhuCount*2 + "";
        public string IsZhongMoney => IsZhong ? "195" : "";
        public string BetNums => IsBeted ? BetShi.IntListToString() + "," + BetGe.IntListToString() : "";
        public string IssueShort => Issue.ToString().Substring(8, 3);
    }

    public class HisotyMoniModel
    {

        public HisotyMoniModel()
        {
            MoniModels = new List<MoniModel>();
        }
        public string DateStr => Date.ToString("yyyy-MM-dd");
        public string DateShortStr => Date.ToString("M.d");
        public DateTime Date { get; set; }
        public List<MoniModel> MoniModels { get; set; }

        public int TotalBetZhu => MoniModels.Sum(x => x.BetZhuCount);
        public int TotalBetMoney => TotalBetZhu*2;
        public int TotalBetTimes => MoniModels.Count(x => x.IsBeted);
        public int TotalBetZhongTimes => MoniModels.Count(x => x.IsBeted && x.IsZhong);
        public int TotalBetZhongMoney=> TotalBetZhongTimes*195;
    }

    public class NumAppearModel
    {
        public NumAppearModel()
        {
            Ge = new List<int>();
            Shi = new List<int>();
        }
        public List<int> Shi { get; set; }
        public List<int> Ge { get; set; }
    }
}