using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SscCommon;
using SscCommon.Buz;
using SscCommon.Models;
using SscCommon.SscUtil;

namespace ssc.test
{
    class Program
    {
        static void Main(string[] args)
        {
            var r1 = SscCombineUtil.CombineBet(new List<int> { 1, 2 }, new List<int> { 3, 4, 5, 6 });
            var r = r1.Distinct().ToList();
            var txt = "";
            foreach(var a in r){
                   var j = new List<string>();
                   a.ForEach(t => j.Add(t.ToString()));
                var s = string.Join(",", j.ToArray());
                   Console.WriteLine(s);
                txt += s + "\r\n";
            }

            var models = new List<SscModel>();
            for (int i = 2; i < 84; i++)
            {
                var model = new SscModel()
                {
                    IssueNo = 18040800 + i,
                    BetNo = new List<int>() {i + 1, i + 2, i + 3, i + 4, i + 5}
                };
                models.Add(model);
            }
            //ElevenX5Buz.SaveModelToFile(models);

            var mo = ElevenX5Buz.GetModelFromFile();

            Console.WriteLine(r.Count);
            Console.ReadLine();
        }      
    }
}
