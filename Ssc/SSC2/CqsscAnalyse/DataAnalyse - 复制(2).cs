//using System.Collections.Generic;

//namespace CqsscAnalyse
//{
//    public class DataAnalyse
//    {

//        public class DataCompareModel
//        {
//            public long beginCompareIssue { get; set; }
//            public string daxiaoArray { get; set; }
//            public string danshuangArray { get; set; }
//            public string zhiheArray { get; set; }
//            public string lu3Array { get; set; }
//            public DataModel nextIssueData { get; set; }
//        }

//        public static void DoAnalyse(List<DataModel> data, int countNo)
//        {
//            List<DataModel> baseResult = new List<DataModel>();

//            Dictionary<long, string> daxiaoArray = new Dictionary<long, string>();
//            Dictionary<long, string> danshuangArray = new Dictionary<long, string>();
//            Dictionary<long, string> zhiheArray = new Dictionary<long, string>();
//            Dictionary<long, string> lu3Array = new Dictionary<long, string>();

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
//                long compareBeginIssue = data[beginIndex].Issue;
//                daxiaoArray.Add(compareBeginIssue, daxiaoCmp);
//                danshuangArray.Add(compareBeginIssue, danshuangCmp);
//                zhiheArray.Add(compareBeginIssue, zhiheCmp);
//                lu3Array.Add(compareBeginIssue, lu3Cmp);
//                beginIndex--;
//            }
//            //comparing
//            for

//            //end 
//        }
//    }
//}
