//using System.Collections.Generic;

//namespace CqsscAnalyse
//{
//    public class DataAnalyse
//    {
//        public static void DoAnalyse(List<DataModel> data, int countNo)
//        {
//            List<DataModel> baseResult = new List<DataModel>();
//            Dictionary<long,string>

//            List<string> daxiaoArray = new List<string>();
//            List<string> danshuangArray = new List<string>();
//            List<string> zhiheArray = new List<string>();
//            List<string> lu3Array = new List<string>();

//            string daxiaoBase = "";
//            string danshuangBase = "";
//            string zhiheBase = "";
//            string lu3Base = "";

//            List<int> zhishuList = new List<int> { 1, 2, 3, 5, 7 };
//            for (int i = 0; i < countNo; i++)
//            {
//                DataModel m = data[countNo - 1 - i];
//                baseResult.Add(m);
//                daxiaoBase += m.Ge > 4 ? "1" : "0";
//                danshuangBase += m.Ge % 2 == 1 ? "1" : "0";
//                zhiheBase += zhishuList.Contains(m.Ge) ? "1" : "0";
//                lu3Base += (m.Ge % 3).ToString();
//            }

//            int beginIndex = data.Count - 1;
//            string daxiaoCmp = "";
//            string danshuangCmp = "";
//            string zhiheCmp = "";
//            string lu3Cmp = "";
//            while (beginIndex - countNo > 0)
//            {
//                List<DataModel> compareData = new List<DataModel>();
//                for (int i = 0; i < countNo; i++)
//                {
//                    DataModel m = data[beginIndex - i];
//                    compareData.Add(data[beginIndex - i]);
//                    daxiaoCmp += m.Ge > 4 ? "1" : "0";
//                    danshuangCmp += m.Ge % 2 == 1 ? "1" : "0";
//                    zhiheCmp += zhishuList.Contains(m.Ge) ? "1" : "0";
//                    lu3Cmp += (m.Ge % 3).ToString();
//                }
//                daxiaoArray.Add(daxiaoCmp);
//                danshuangArray.Add(danshuangCmp);
//                zhiheArray.Add(zhiheCmp);
//                lu3Array.Add(lu3Cmp);
//                beginIndex--;
//            }
//        }
//    }
//}
