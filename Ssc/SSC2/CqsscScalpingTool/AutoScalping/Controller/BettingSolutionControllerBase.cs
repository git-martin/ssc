using System;
using System.Collections.Generic;

namespace AutoScalping.Controller
{
    public class BettingSolutionControllerBase
    {
        public static List<int> RandomCreateNumberList()
        {
            List<int> result = new List<int>();
            List<int> baseNo = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 10; i++)
            {
                int index = rnd.Next(0, baseNo.Count);
                result.Add(baseNo[index]);
                baseNo.RemoveAt(index);
            }
            return result;
        }
    }
}