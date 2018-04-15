using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoScalping.Models;

namespace AutoScalping.Controller
{
    public class BettingSolutionController1: BettingSolutionControllerBase
    {
        public static List<BettingSolutinModel> BuildingBetModels(ref List<AccountResponse> jAccountList, ref List<AccountResponse> qAccountList)
        {
            int jcount = jAccountList.Count;
            int qcount  = qAccountList.Count;
            var betSeperatedNums = RandomSpliteNumbers(jcount, qcount);
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
                m.BettingNums = betSeperatedNums[j];
                m.Beishu = GloableParams.DefaultJiangMultBet;
                result.Add(m);
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
                m.BettingNums = betSeperatedNums[jcount+q];
                m.Beishu = GloableParams.DefaultQcwMultBet;
                result.Add(m);
            }
            return result;

        }

        private static List<List<int>> RandomSpliteNumbers(int seperateJiangCount, int seperateQcwCount)
        {
            var result = new List<List<int>>();
            var baseNums = RandomCreateNumberList();
            List<int> groupJ = baseNums.GetRange(0, 5);
            List<int> groupQ = baseNums.GetRange(5, 5);
            Seperated(ref result,ref groupJ,seperateJiangCount);
            Seperated(ref result, ref groupQ, seperateQcwCount);
            return result;
        }

        private static void Seperated(ref List<List<int>> result, ref List<int> numbers, int dividedCount)
        {
            if (dividedCount == 1)
            {
                result.Add(numbers);
            }
            else if (dividedCount == 2)
            {
                int count = new Random().Next(1, 5);
                var tLst1 = numbers.GetRange(0, count);
                int count2 = 5 - count;
                var tLst2 = numbers.GetRange(count, count2);
                result.Add(tLst1);
                result.Add(tLst2);
            }
            else if (dividedCount == 3)
            {
                int count = new Random().Next(1, 4);
                var tLst1 = numbers.GetRange(0, count);
                int left = 5 - count;
                int count2 = new Random().Next(1, left);
                var tLst2 = numbers.GetRange(count, count2);
                int count3 = 5 - count - count2;
                var tLst3 = numbers.GetRange(count + count2, count3);
                result.Add(tLst1);
                result.Add(tLst2);
                result.Add(tLst3);
            }
        }
    }
}
