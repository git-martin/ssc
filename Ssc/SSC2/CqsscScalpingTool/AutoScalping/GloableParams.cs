using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoScalping.Http;
using AutoScalping.Models;

namespace AutoScalping
{
    public static class GloableParams
    {
        public static AccountResponse CurrentAccount { get; set; }

        public static  List<AccountResponse> JiangAccountFromFile { get; set; }

        public static List<AccountResponse> QcwAccountFromFile { get; set; }

        public static List<AccountResponse> JiangErrorAccountFromFile = new List<AccountResponse>();

        public static List<AccountResponse> QcwErrorAccountFromFile = new List<AccountResponse>();

        public static int DefaultJiangMultBet = 1;
        public static int DefaultQcwMultBet = 1;

        public static int DefaultJiangPersent = 2;
        public static int DefaultQcwPersent= 3;


        public static Version AppCurrentVersion { get; set; }
    }
}
