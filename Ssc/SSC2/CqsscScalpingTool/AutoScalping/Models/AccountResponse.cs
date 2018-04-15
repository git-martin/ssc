namespace AutoScalping.Models
{
    public class AccountResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string UserToken { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string LoginName { get; set; }
        public int VipLevel { get; set; }
        public float CommissionBalance { get; set; }
        public float ExpertsBalance { get; set; }
        public float BonusBalance { get; set; }
        public float FreezeBalance { get; set; }
        public float FillMoneyBalance { get; set; }
        public long Mobile { get; set; }
        public string RealName { get; set; }
        public string IdCardNumber { get; set; }
        public bool IsSetBalancePwd { get; set; }
        public string NeedBalancePwdPlace { get; set; }
        public bool IsBingBankCard { get; set; }
        public string BankCardNumber { get; set; }
        public int UserGrowth { get; set; }
        public float RedBagBalance { get; set; }
        public int NeedGrowth { get; set; }
        public bool IsBetHM { get; set; }
        public int UnReadMailCount { get; set; }
        public int HideDisplayNameCount { get; set; }

        //自定义属性
        public string Password { get; set; }

        public GloableConstants.AccountBelongs ComeFrom { get; set; }
        public bool HasBeted { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

        public float MyTotoalMoeny
        {
            get { return FillMoneyBalance + BonusBalance + CommissionBalance; }
        }

        public string ComeFromStr
        {
            get
            {
                string a = "未知";
                switch (ComeFrom)
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
    }

}
