using System.Collections.Generic;
using SscCommon.CommonUti;

namespace SscCommon.SscUtil
{
    public class SscCombineUtil
    {
        public static List<List<int>> CombineNumbers(List<int> danList,List<int> tuoList)
        {
            var combinedList = new List<List<int>>();
            var a2 = PermutationAndCombination<int>.GetCombination(tuoList.ToArray(), 2);
            var a3 = PermutationAndCombination<int>.GetCombination(tuoList.ToArray(), 3);
            var a4 = PermutationAndCombination<int>.GetCombination(tuoList.ToArray(), 4);

            var d1 = PermutationAndCombination<int>.GetCombination(danList.ToArray(), 1);
            var d2 = PermutationAndCombination<int>.GetCombination(danList.ToArray(), 2);
            var finalDan = new List<int[]>();
            finalDan.AddRange(d1);
            finalDan.AddRange(d2);
            foreach (var danArray in finalDan)
            {
                var dan = new List<int>();
                foreach (var a in danArray)
                {
                    dan.Add(a);
                }
                foreach (var tuo in a2)
                {
                    var t = new List<int>();
                    t.AddRange(dan);
                    t.AddRange(tuo);
                    combinedList.Add(t);
                }
                foreach (var tuo in a3)
                {
                    var t = new List<int>();
                    t.AddRange(dan);
                    t.AddRange(tuo);
                    combinedList.Add(t);
                }
                if (dan.Count == 1)
                {
                    foreach (var tuo in a4)
                    {
                        var t = new List<int>();
                        t.AddRange(dan);
                        t.AddRange(tuo);
                        combinedList.Add(t);
                    }
                }
               
            }
            return combinedList;
        }

        public static List<List<int>> CombineBet(List<int> danList, List<int> tuoList)
        {
            var combinedList = CombineNumbers(danList,tuoList);
            var combinedBet = new List<List<int>>();

            var leftList = new List<int>();
            leftList.AddRange(SscConst.BetNumbers);
            danList.ForEach(a => leftList.Remove(a));
            tuoList.ForEach(a => leftList.Remove(a));
                                                          
            var left1 = PermutationAndCombination<int>.GetCombination(leftList.ToArray(), 1);
            var left2 = PermutationAndCombination<int>.GetCombination(leftList.ToArray(), 2);
            foreach (var tempCombined in combinedList)
            {
                if (tempCombined.Count == 3)
                {
                    foreach (var add in left2)
                    {
                        var bet = new List<int>();
                        bet.AddRange(tempCombined);
                        bet.AddRange(add);
                        combinedBet.Add(bet);
                    }
                }
                else if (tempCombined.Count == 4)
                {
                    foreach (var add in left1)
                    {
                        var bet = new List<int>();
                        bet.AddRange(tempCombined);
                        bet.AddRange(add);
                        combinedBet.Add(bet);
                    }
                }
                else
                {
                    //var j = new List<string>();
                    //tempCombined.ForEach(t => j.Add(t.ToString()));
                    //var s = string.Join(",", j.ToArray());
                    //Console.WriteLine("---------{0}>>{1}",tempCombined.Count,s);
                    var bet = new List<int>();
                    bet.AddRange(tempCombined);
                    combinedBet.Add(bet);
                }
            }
            return combinedBet;
        }
    }
}
