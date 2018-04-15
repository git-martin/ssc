using System.Collections.Generic;
using System.Configuration;

namespace CqsscAnalyse
{
    public static class GlobalConstants
    {
        public static string SqLiteConnectString = ConfigurationManager.AppSettings["SQLiteConnectionString"];
        public static int ReadDataDays = int.Parse(ConfigurationManager.AppSettings["getDataDaysBefore"]);
        public static int MinAnalyseIssues = int.Parse(ConfigurationManager.AppSettings["minIssueCount"]);
        public static int MaxAnalyseIssues = int.Parse(ConfigurationManager.AppSettings["maxIssueCount"]);

        public static List<string> DayIssueList()
        {
            List<string> result = new List<string>();
            for (int i = 1; i < 121; i++)
            {
                result.Add(i.ToString().PadLeft(3,'0'));
            }
            return result;
        }
    }
}
