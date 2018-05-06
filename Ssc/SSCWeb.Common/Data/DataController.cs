using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using SSCWeb.Common.Models;
using SSCWeb.Common.Utils;

namespace SSCWeb.Common.Data
{
    public class DataController
    {
        public static List<IssueModel> ReaderDaysIssue(int readDataDays)
        {
            List<IssueModel> result = new List<IssueModel>();
            try
            {
                string beginIssue = DateTime.Now.AddDays(0 - readDataDays).ToString("yyyyMMdd") + "001";
                string sql = "select * from cqssc where issue>=" + beginIssue+ "  order by issue desc";
                DataTable dt = SqLiteHelper.getBLLInstance().ExecuteTable(sql, null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        IssueModel dm = new IssueModel();
                        dm.StrIssue = row["issue"].ToString();
                        dm.Wan = int.Parse(row["wan"].ToString());
                        dm.Qian = int.Parse(row["qian"].ToString());
                        dm.Bai = int.Parse(row["bai"].ToString());
                        dm.Shi = int.Parse(row["shi"].ToString());
                        dm.Ge = int.Parse(row["ge"].ToString());
                        dm.OpenCode = row["opencode"].ToString();
                        dm.OpenTime = row["opentime"].ToString();
                        dm.sync = int.Parse(row["sync"].ToString());
                        result.Add(dm);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool InsertOrUpdateIssueModel2Db(IssueModel model)
        {
            string sql = "select count(*) from cqssc where issue=" + model.Issue;
            if (SqLiteHelper.getBLLInstance().Exists(sql))
            {
                return UpdateIssueModel2Db(model);
            }
            else
            {
                return InsertIssueModel2Db(model);
            }
        }

        public static bool InsertIssueModel2Db(IssueModel model)
        {
           try
            {
                var param = new SQLiteParameter[]{
                new SQLiteParameter("@issue",model.Issue),
                new SQLiteParameter("@opencode",model.OpenCode),
                new SQLiteParameter("@opentime",model.OpenTime),
                new SQLiteParameter("@wan",model.Wan),
                new SQLiteParameter("@qian",model.Qian),
                new SQLiteParameter("@bai",model.Bai),
                new SQLiteParameter("@shi",model.Shi),
                new SQLiteParameter("@ge",model.Ge),
                new SQLiteParameter("@sync",model.sync),
            };
                string sql = "insert into cqssc(issue,opencode,opentime,wan,qian,bai,shi,ge,sync) values(@issue,@opencode,@opentime,@wan,@qian,@bai,@shi,@ge,@sync)";
                return SqLiteHelper.getBLLInstance().ExecuteNonQuery(sql, param) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool UpdateIssueModel2Db(IssueModel model)
        {
            try
            {
                var param = new SQLiteParameter[]{
                new SQLiteParameter("@issue",model.Issue),
                new SQLiteParameter("@opencode",model.OpenCode),
                new SQLiteParameter("@opentime",model.OpenTime),
                new SQLiteParameter("@wan",model.Wan),
                new SQLiteParameter("@qian",model.Qian),
                new SQLiteParameter("@bai",model.Bai),
                new SQLiteParameter("@shi",model.Shi),
                new SQLiteParameter("@ge",model.Ge),
                new SQLiteParameter("@sync",model.sync),
            };
                string sql = "update cqssc set opencode=@opencode, opentime=@opentime, wan=@wan,qian=@qian,bai=@bai,shi=@shi,ge=@ge,sync=@sync where issue=@issue";
                return SqLiteHelper.getBLLInstance().ExecuteNonQuery(sql, param) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
