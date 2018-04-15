namespace AutoScalping.Models
{
    public class CqsscBettingContent
    {

        /// <summary>
        /// 红包
        /// </summary>
        public float RedBagMoney { get; set; }

        public string UserToken
        { get; set; }
        public bool StopAfterBonus
        {
            get { return true; }
        }
        /// <summary>
        /// 投注号码
        /// </summary>
        public string CodeList { get; set; }

        /// <summary>
        /// 玩法 一星直选 ，大小单双
        /// </summary>
        public string GameType { get; set; } //DXDS 1XDX
        public int Security
        {
            get { return 4; }
        }
        public string BalancePassword
        {
            get { return ""; }
        }

        /// <summary>
        /// 总注数
        /// </summary>
        public int TotalMatchCount { get; set; }

        public bool IsRepeat
        {
            get { return false; }
        }

        public string StrCode
        {
            get { return "37"; }
        }

        public string PlayType
        {
            get { return ""; }
        } //
        /// <summary>
        /// 期号
        /// </summary>
        public string IssuseList { get; set; }
        public string GameCode
        {
            get { return "CQSSC"; }
        } //CQSSC
        /// <summary>
        /// 总额度
        /// </summary>
        public int TotalMoney { get; set; }
    }


    public class IssueContent
    {
        public int Amount { get; set; }
        public string IssuseNumber { get; set; }
        public int IssuseTotalMoney { get; set; }

    }

    public class CodeContent
    {
        public bool IsDan { get { return false; } }

        public string GameType { get; set; }

        public string AnteCode { get; set; }

        public string MatchId { get { return ""; } }
    }
}
