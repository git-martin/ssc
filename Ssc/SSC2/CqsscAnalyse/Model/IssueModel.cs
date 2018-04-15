namespace CqsscAnalyse.Model
{
    public class IssueModel
    {
        public long Issue
        {
            get
            {
                return long.Parse(StrIssue);
            }
        }

        public int Wan { get; set; }
        public int Qian { get; set; }
        public int Bai { get; set; }
        public int Shi { get; set; }
        public int Ge { get; set; }
        public string StrIssue { get; set; }
        public string OpenCode { get; set; }
        public string OpenTime { get; set; }
        public int sync { get; set; }

        public bool IsSync
        {
            get { return sync == 1; }
        }

    }
}