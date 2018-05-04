using System.Net;
using HtmlAgilityPack;
using SSCWeb.Common.Model;

namespace SSCWeb.App_Code
{
    public class DataSaver
    {
        public void SaveData()
        {
            return;
            string content = string.Empty;
            string url = "http://www.lecai.com/lottery/draw/list/200?d=2016-10-28";
            try
            {
                HtmlDocument htmldoc =  new HtmlWeb().Load(url);
                HtmlNode rootNode = htmldoc.DocumentNode;
                HtmlNodeCollection issueNodes = rootNode.SelectNodes("//table[@id='draw_list']//tbody//tr");

                HtmlNode temp = null;
                IssueModel model = null;

                foreach (HtmlNode row in issueNodes)
                {
                    temp = HtmlNode.CreateNode(row.OuterHtml);
                    model = new IssueModel();
                    string a = temp.SelectSingleNode("//tr//td[0]").InnerText;
                    string b = temp.SelectSingleNode("//tr//td[1]").InnerText;

                    HtmlNodeCollection opencodesNodes = temp.SelectNodes("//tr//td[2]//span");

                    if (opencodesNodes.Count > 4)
                    {
                        string wan = opencodesNodes[0].InnerText;
                        string qian = opencodesNodes[1].InnerText;
                        string bai = opencodesNodes[2].InnerText;
                        string shi = opencodesNodes[3].InnerText;
                        string ge = opencodesNodes[4].InnerText;
                    }
                }
              

            }
            catch (WebException)
            {

            }
        }
    }
}
