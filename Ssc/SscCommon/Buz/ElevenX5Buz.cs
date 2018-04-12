using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SscCommon.CommonUti;
using SscCommon.Models;
using SscCommon.SscUtil;

namespace SscCommon.Buz
{
    public class ElevenX5Buz
    {
        protected static string RootPath = System.Environment.CurrentDirectory;
        protected static string BetFileName = "sys.no.dat";
        protected static string BetFilePath = System.IO.Path.Combine(RootPath, BetFileName);
        public static List<ElevenX5Model> GetModelFromFile()
        {
            var result = new List<ElevenX5Model>();
            if (!FileUtil.IsExistFile(BetFilePath))
            {
                FileUtil.CreateFile(BetFilePath);
            }
            var fileContent = FileUtil.FileToString(BetFilePath);
            if (string.IsNullOrWhiteSpace(fileContent))
                return result;
            var lst = fileContent.Split('@');
            if (lst.Any())
            {
                foreach (var item in lst)
                {
                    var bet = item.Split(',');
                    var model = new ElevenX5Model()
                    {
                        IssueNo = bet[0].ToInt(),
                        BetNo = new List<int>
                        {
                            bet[1].ToInt(),
                            bet[2].ToInt(),
                            bet[3].ToInt(),
                            bet[4].ToInt(),
                            bet[5].ToInt(),
                        },
                       
                    };
                    if (bet.Length > 6)
                    {
                        model.Index = bet[6].ToInt();
                    }
                    result.Add(model);
                }
            }
            return result;
        }
        public static bool SaveModelToFile(List<ElevenX5Model> models)
        {
            try
            {
                var sb = new StringBuilder();
                foreach (var model in models)
                {
                    sb.AppendFormat("{0},{1}@", model.IssueNo, string.Join(",", model.BetNo));
                }
                var result = sb.ToString();
                if (!string.IsNullOrWhiteSpace(result) && result.EndsWith("@"))
                {
                    result = result.TrimEnd('@');
                }
                FileUtil.WriteText(BetFilePath, result);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static List<List<int>> GetCombinedBetNos(List<int> danList, List<int> tuoList)
        {
           return SscCombineUtil.CombineBetNo(danList,tuoList);
        }
        public static List<List<int>> GetCombinedDanTuoNos(List<int> danList, List<int> tuoList)
        {
            return SscCombineUtil.CombineDanTuoNo(danList, tuoList);
        }


        public static List<AllDanTuoCombinedModel> CalculateAllDanTuoCombinationModels()
        {
            var result = new List<AllDanTuoCombinedModel>();
            var dan2List = PermutationAndCombination<int>.GetCombination(SscConst.BetNumbers.ToArray(), 2);
            foreach (var dan2 in dan2List)
            {
                var rest = SscConst.BetNumbers.ToList();
                rest.RemoveAll(x => dan2.Contains(x));
                var tuo4List = PermutationAndCombination<int>.GetCombination(rest.ToArray(), 4);
                foreach (var tuo4 in tuo4List)
                {
                    var danNums = dan2.ToList();
                    var tuoNums = tuo4.ToList();
                    var tmp = GetCombinedDanTuoNos(danNums, tuoNums);
                    var dantuoModels = tmp.Select(model => new DanTuoModel()
                    {
                        DanTuoNums = model, MissingCount = 0
                    }).ToList();

                    result.Add(new AllDanTuoCombinedModel()
                                 {
                                     DanNums = dan2.ToList(),
                                     TuoNums = tuo4.ToList(),
                                     DanTuoModel = dantuoModels
                                     
                                 });
                }
            }
            return result;
        } 
    }
}
