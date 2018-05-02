namespace SSC
{
    public class DataCompareModel
    {
        public long beginCompareIssue { get; set; }
        public string daxiaoCmp { get; set; }
        public string danshuangCmp { get; set; }
        public string zhiheCmp { get; set; }
        public string lu3Cmp { get; set; }
        public IssueModel nextIssueData { get; set; }
    }
}