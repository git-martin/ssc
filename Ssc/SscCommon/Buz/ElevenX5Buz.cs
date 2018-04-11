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
                        Index = bet[6].ToInt(),
                    };
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
    }
}
