﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SscCommon;
using SscCommon.Buz;
using SscCommon.Models;
using SscCommon.SscUtil;

namespace ssc.test
{

    public enum A
    {
        Wish=1,
        Ali=2,
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(A.Wish.ToString());
            //Console.WriteLine(r.Count);
            Console.ReadLine();
            var date = DateTime.MinValue;
            DateTime.TryParseExact("180409", "yyMMdd",CultureInfo.CurrentCulture, DateTimeStyles.None, out date);

            var r1 = SscCombineUtil.CombineBetNo(new List<int> { 1, 2 }, new List<int> { 3, 4, 5, 6 });
            var r = r1.Distinct().ToList();
            var txt = "";
            foreach(var a in r){
                   var j = new List<string>();
                   a.ForEach(t => j.Add(t.ToString()));
                var s = string.Join(",", j.ToArray());
                   Console.WriteLine(s);
                txt += s + "\r\n";
            }

            var models = new List<ElevenX5Model>();
            for (int i = 2; i < 84; i++)
            {
                var model = new ElevenX5Model()
                {
                    IssueNo = 18040800 + i,
                    BetNo = new List<int>() {i + 1, i + 2, i + 3, i + 4, i + 5}
                };
                models.Add(model);
            }
            //ElevenX5Buz.SaveModelToFile(models);

            var mo = ElevenX5Buz.GetModelFromFile();

            Console.WriteLine(A.Wish.ToString());
            Console.WriteLine(r.Count);
            Console.ReadLine();
        }      
    }
}
