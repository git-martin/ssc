using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoScalping.Models;

namespace AutoScalping.Controller
{
    public class BettingSolutionController0Test: BettingSolutionControllerBase
    {
        public static List<BettingSolutinModel> BuildingBetModels(ref List<AccountResponse> jAccountList, ref List<AccountResponse> qAccountList)
        {
            int jcount = jAccountList.Count;
            int qcount  = qAccountList.Count;
            List<BettingSolutinModel> result = new List<BettingSolutinModel>();
            for (int j = 0; j < jcount; j++)
            {
                var account = jAccountList[j];
                BettingSolutinModel m = new BettingSolutinModel();
                m.CurrentToken = account.UserToken;
                m.LoginName = account.LoginName;
                m.AccountMoney = account.MyTotoalMoeny;
                m.AccountRed = account.RedBagBalance;
                m.Belongs = GloableConstants.AccountBelongs.Jiang;
                m.Password = account.Password;
                m.BettingNums = new List<int>() {1};
                m.Beishu = GloableParams.DefaultJiangMultBet;
                result.Add(m);
                break;;
            }
            for (int q= 0; q < qcount; q++)
            {
                var account = qAccountList[q];
                BettingSolutinModel m = new BettingSolutinModel();
                m.CurrentToken = account.UserToken;
                m.LoginName = account.LoginName;
                m.AccountMoney = account.MyTotoalMoeny;
                m.AccountRed = account.RedBagBalance;
                m.Belongs = GloableConstants.AccountBelongs.Qcw;
                m.Password = account.Password;
                m.BettingNums = new List<int>() { 3 };
                m.Beishu = GloableParams.DefaultJiangMultBet;
                result.Add(m);
                break;
            }
            return result;
        }
    }
}
