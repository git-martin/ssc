using System.Collections.Generic;
using CqsscAnalyse.Model;

namespace CqsscAnalyse
{
    public class ReadyDataModel
    {
        public DataBeCompareModel BeCmpModel { get; set; }
        public List<DataCompareModel> CmpModels { get; set; }
    }


    public class GetDataAnalyse
    {
        public static ReadyDataModel GetModelsReadyGe(List<IssueModel> data, int countNo)
        {
            ReadyDataModel result = new ReadyDataModel();

            List<IssueModel> baseResult = new List<IssueModel>();
            string daxiaoBase = "";
            string danshuangBase = "";
            string zhiheBase = "";
            string lu3Base = "";
            List<int> zhishuList = new List<int> { 1, 2, 3, 5, 7 };
            for (int i = 0; i < countNo; i++)
            {
                IssueModel m = data[countNo - 1 - i];
                baseResult.Add(m);
                daxiaoBase += m.Ge > 4 ? "1" : "0";
                danshuangBase += m.Ge % 2 == 1 ? "1" : "0";
                zhiheBase += zhishuList.Contains(m.Ge) ? "1" : "0";
                lu3Base += (m.Ge % 3).ToString();
            }
            DataBeCompareModel becmp = new DataBeCompareModel();
            becmp.beginCompareIssue = data[countNo - 1].Issue;
            becmp.endCompareIssue = data[0].Issue;
            becmp.daxiaoBase = daxiaoBase;
            becmp.danshuangBase = danshuangBase;
            becmp.zhiheBase = zhiheBase;
            becmp.lu3Base = lu3Base;
            result.BeCmpModel = becmp;

            int beginIndex = data.Count - 1;
            string daxiaoCmp = "";
            string danshuangCmp = "";
            string zhiheCmp = "";
            string lu3Cmp = "";
            result.CmpModels = new List<DataCompareModel>();
            while (beginIndex - countNo > 0)
            {
                daxiaoCmp = "";
                danshuangCmp = "";
                zhiheCmp = "";
                lu3Cmp = "";

                List<IssueModel> compareData = new List<IssueModel>();
                for (int i = 0; i < countNo; i++)
                {
                    IssueModel m = data[beginIndex - i];
                    compareData.Add(data[beginIndex - i]);
                    daxiaoCmp += m.Ge > 4 ? "1" : "0";
                    danshuangCmp += m.Ge % 2 == 1 ? "1" : "0";
                    zhiheCmp += zhishuList.Contains(m.Ge) ? "1" : "0";
                    lu3Cmp += (m.Ge % 3).ToString();
                }
                DataCompareModel cmp = new DataCompareModel();
                cmp.beginCompareIssue = data[beginIndex].Issue;
                cmp.daxiaoCmp = daxiaoCmp;
                cmp.danshuangCmp = danshuangCmp;
                cmp.zhiheCmp = zhiheCmp;
                cmp.lu3Cmp = lu3Cmp;
                cmp.nextIssueData = data[beginIndex - countNo];
                result.CmpModels.Add(cmp);          


                beginIndex--;
            }
            //comparing
            return result;
            //end 
        }

        public static ReadyDataModel GetModelsReadyShi(List<IssueModel> data, int countNo)
        {
            ReadyDataModel result = new ReadyDataModel();

            List<IssueModel> baseResult = new List<IssueModel>();
            string daxiaoBase = "";
            string danshuangBase = "";
            string zhiheBase = "";
            string lu3Base = "";
            List<int> zhishuList = new List<int> { 1, 2, 3, 5, 7 };
            for (int i = 0; i < countNo; i++)
            {
                IssueModel m = data[countNo - 1 - i];
                baseResult.Add(m);
                daxiaoBase += m.Shi > 4 ? "1" : "0";
                danshuangBase += m.Shi % 2 == 1 ? "1" : "0";
                zhiheBase += zhishuList.Contains(m.Shi) ? "1" : "0";
                lu3Base += (m.Shi % 3).ToString();
            }
            DataBeCompareModel becmp = new DataBeCompareModel();
            becmp.beginCompareIssue = data[countNo - 1].Issue;
            becmp.endCompareIssue = data[0].Issue;
            becmp.daxiaoBase = daxiaoBase;
            becmp.danshuangBase = danshuangBase;
            becmp.zhiheBase = zhiheBase;
            becmp.lu3Base = lu3Base;
            result.BeCmpModel = becmp;

            int beginIndex = data.Count - 1;
            string daxiaoCmp = "";
            string danshuangCmp = "";
            string zhiheCmp = "";
            string lu3Cmp = "";
            result.CmpModels = new List<DataCompareModel>();
            while (beginIndex - countNo > 0)
            {
                daxiaoCmp = "";
                danshuangCmp = "";
                zhiheCmp = "";
                lu3Cmp = "";

                List<IssueModel> compareData = new List<IssueModel>();
                for (int i = 0; i < countNo; i++)
                {
                    IssueModel m = data[beginIndex - i];
                    compareData.Add(data[beginIndex - i]);
                    daxiaoCmp += m.Shi > 4 ? "1" : "0";
                    danshuangCmp += m.Shi % 2 == 1 ? "1" : "0";
                    zhiheCmp += zhishuList.Contains(m.Shi) ? "1" : "0";
                    lu3Cmp += (m.Shi % 3).ToString();
                }
                DataCompareModel cmp = new DataCompareModel();
                cmp.beginCompareIssue = data[beginIndex].Issue;
                cmp.daxiaoCmp = daxiaoCmp;
                cmp.danshuangCmp = danshuangCmp;
                cmp.zhiheCmp = zhiheCmp;
                cmp.lu3Cmp = lu3Cmp;
                cmp.nextIssueData = data[beginIndex - countNo];
                result.CmpModels.Add(cmp);


                beginIndex--;
            }
            //comparing
            return result;
            //end 
        }

        public static ReadyDataModel GetModelsReadyBai(List<IssueModel> data, int countNo)
        {
            ReadyDataModel result = new ReadyDataModel();

            List<IssueModel> baseResult = new List<IssueModel>();
            string daxiaoBase = "";
            string danshuangBase = "";
            string zhiheBase = "";
            string lu3Base = "";
            List<int> zhishuList = new List<int> { 1, 2, 3, 5, 7 };
            for (int i = 0; i < countNo; i++)
            {
                IssueModel m = data[countNo - 1 - i];
                baseResult.Add(m);
                daxiaoBase += m.Bai > 4 ? "1" : "0";
                danshuangBase += m.Bai % 2 == 1 ? "1" : "0";
                zhiheBase += zhishuList.Contains(m.Bai) ? "1" : "0";
                lu3Base += (m.Bai % 3).ToString();
            }
            DataBeCompareModel becmp = new DataBeCompareModel();
            becmp.beginCompareIssue = data[countNo - 1].Issue;
            becmp.endCompareIssue = data[0].Issue;
            becmp.daxiaoBase = daxiaoBase;
            becmp.danshuangBase = danshuangBase;
            becmp.zhiheBase = zhiheBase;
            becmp.lu3Base = lu3Base;
            result.BeCmpModel = becmp;

            int beginIndex = data.Count - 1;
            string daxiaoCmp = "";
            string danshuangCmp = "";
            string zhiheCmp = "";
            string lu3Cmp = "";
            result.CmpModels = new List<DataCompareModel>();
            while (beginIndex - countNo > 0)
            {
                daxiaoCmp = "";
                danshuangCmp = "";
                zhiheCmp = "";
                lu3Cmp = "";

                List<IssueModel> compareData = new List<IssueModel>();
                for (int i = 0; i < countNo; i++)
                {
                    IssueModel m = data[beginIndex - i];
                    compareData.Add(data[beginIndex - i]);
                    daxiaoCmp += m.Bai > 4 ? "1" : "0";
                    danshuangCmp += m.Bai % 2 == 1 ? "1" : "0";
                    zhiheCmp += zhishuList.Contains(m.Bai) ? "1" : "0";
                    lu3Cmp += (m.Bai % 3).ToString();
                }
                DataCompareModel cmp = new DataCompareModel();
                cmp.beginCompareIssue = data[beginIndex].Issue;
                cmp.daxiaoCmp = daxiaoCmp;
                cmp.danshuangCmp = danshuangCmp;
                cmp.zhiheCmp = zhiheCmp;
                cmp.lu3Cmp = lu3Cmp;
                cmp.nextIssueData = data[beginIndex - countNo];
                result.CmpModels.Add(cmp);


                beginIndex--;
            }
            //comparing
            return result;
            //end 
        }
    }
}
