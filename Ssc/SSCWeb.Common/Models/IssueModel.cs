using System.Collections.Generic;

namespace SSCWeb.Models
{
    public class IssueModel
    {
        public long Issue
        {
            get
            {
                return long.Parse(StrIssue);
            }
        }

        public int Wan { get; set; }
        public int Qian { get; set; }
        public int Bai { get; set; }
        public int Shi { get; set; }
        public int Ge { get; set; }
        public string StrIssue { get; set; }
        public string OpenCode { get; set; }
        public string OpenTime { get; set; }
        public int sync { get; set; }

        public bool IsSync
        {
            get { return sync == 1; }
        }

    }

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
    }
}