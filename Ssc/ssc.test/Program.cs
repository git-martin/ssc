using System;
using System.Collections.Generic;
using System.Text;
using SscCommon;

namespace ssc.test
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = CombineBet();
            foreach(var a in r){
                   var j = new List<string>();
                   a.ForEach(t => j.Add(t.ToString()));
                   Console.WriteLine(string.Join(",", j.ToArray()));
            }
            Console.WriteLine(r.Count);
            Console.ReadLine();
        }

        private static List<List<int>> CombineBet()
        {
            var combinedList = CombineNumbers();
            var combinedBet = new List<List<int>>();

            var leftList = new List<int> { 3, 8, 9, 10, 11 };
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
                if (tempCombined.Count == 4)
                {
                    foreach (var add in left1)
                    {
                        var bet = new List<int>();
                        bet.AddRange(tempCombined);
                        bet.AddRange(add);
                        combinedBet.Add(bet);
                    }
                }
            }


            return combinedBet;
        }


        private static List<List<int>> CombineNumbers()
        {
            var danList = new List<int> { 1, 2 };
            var tuoList = new List<int> { 4, 5, 6, 7 };
            var combinedList = new List<List<int>>();
            var a2 = PermutationAndCombination<int>.GetCombination(tuoList.ToArray(), 2);
            var a3 = PermutationAndCombination<int>.GetCombination(tuoList.ToArray(), 3);
            var a4  = PermutationAndCombination<int>.GetCombination(tuoList.ToArray(), 4);

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
                foreach (var tuo in a4)
                {
                    var t = new List<int>();
                    t.AddRange(dan);
                    t.AddRange(tuo);
                    combinedList.Add(t);
                }
            }
            return combinedList;
        }
    }
}
