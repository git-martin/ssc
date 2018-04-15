namespace AutoScalping
{
    public static class GloableConstants
    {
        public static class Jiang
        {
            public static string Phone_CurrentIssueServiceUrl = "http://app.jiangapps.com/app/PhoneTermInfo";
            public static string Phone_CqsscBetServiceUrl = "http://app.jiangapps.com/LotteryService/Service";
            public static string Phone_LoginServiceUrl = "http://app.jiangapps.com/app/PhoneLogin";
        }
        public static class Qcw
        {
            public static string Phone_CurrentIssueServiceUrl = "http://app.qcwapps.com/app/PhoneTermInfo";
            public static string Phone_CqsscBetServiceUrl = "http://app.qcwapps.com/LotteryService/Service";
            public static string Phone_LoginServiceUrl = "http://app.qcwapps.com:841/app/PhoneLogin";
        }

        public  enum AccountBelongs
        {
            Jiang = 1,
            Qcw,
        }
        public static class CqsscGameType
        {
            public const string YiXingDX = "1XDX";
            public const string DXDS = "DXDS";
        }

        public static class CqsscDxdsNumber
        {
            public const int Da = 2;
            public const int Xiao = 1;
            public const int Dan = 5;
            public const int Shuang = 4;
        }



    }
}
