namespace SSCWeb.Common.Models
{
    public class IssueModel
    {
        public long Issue => long.Parse(StrIssue);
        public int ShortIssue => int.Parse(StrIssue.Substring(8, 3));
        public int Wan { get; set; }
        public int Qian { get; set; }
        public int Bai { get; set; }
        public int Shi { get; set; }
        public int Ge { get; set; }
        public string StrIssue { get; set; }
        public string OpenCode { get; set; }
        public string OpenTime { get; set; }
        public int sync { get; set; }

        public bool IsSync => sync == 1;
    }
}