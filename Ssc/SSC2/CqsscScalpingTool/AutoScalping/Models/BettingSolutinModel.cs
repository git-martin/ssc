using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using AutoScalping.Util;

namespace AutoScalping.Models
{
    public class BettingSolutinModel
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public GloableConstants.AccountBelongs Belongs { get; set; }

        public string BelongsStr
        {
            get
            {
                string a = "未知";
                switch (Belongs)
                {
                    case GloableConstants.AccountBelongs.Jiang:
                        a = "大奖网";
                        break;
                    case GloableConstants.AccountBelongs.Qcw:
                        a = "趣彩网";
                        break;
                    default:
                        break;
                }
                return a;
            }
        }

        public string BettingNumStr
        {
            get
            {
                BettingNums.Sort();
                return "-,-,-,-,"+ StrUtil.ConvertListToString<int>(BettingNums);
            }
        }

        public List<int> BettingNums { get; set; }

        public int Zhushu
        {
            get { return this.BettingNums.Count; }
        }
        public int Beishu { get; set; }

        public int TotalMoney
        {
            get { return this.Beishu*this.Zhushu*2; }
        }

        public float TotalReallyPayMoney
        {
            get { return this.TotalMoney - this.MaxRedUse; }
        }

        public float MaxRedUse
        {
            get
            {
                float present = 0;
                if (this.Belongs == GloableConstants.AccountBelongs.Jiang)
                {
                    present = GloableParams.DefaultJiangPersent;
                }
                else if (this.Belongs == GloableConstants.AccountBelongs.Qcw)
                {
                    present = GloableParams.DefaultQcwPersent;
                }
                float canUse = (float)(TotalMoney*present*1.0/100.0);
                if (canUse <= AccountRed)
                    return canUse;
               return AccountRed;
            }
        }

        public float MaxRedTheory
        {
            get
            {
                float present = 0;
                if (this.Belongs == GloableConstants.AccountBelongs.Jiang)
                {
                    present = GloableParams.DefaultJiangPersent;
                }
                else if (this.Belongs == GloableConstants.AccountBelongs.Qcw)
                {
                    present = GloableParams.DefaultQcwPersent;
                }
                float canUse = (float)(TotalMoney * present * 1.0 / 100.0);
                return canUse;
            }
        }

        public float AccountMoney { get; set; }
        public float AccountRed { get; set; }

        public string CurrentIssueStr { get; set; }

        public bool IsBettingSucc { get; set; }

        public string IsBettingSuccMsg
        {
            get { return this.IsBettingSucc ? "投注成功" : "投注失败"; }
        }

        public int  ResponseCode { get; set; }

        public string MyMessage { get; set; }

        public string CurrentToken { get; set; }
    }
}
