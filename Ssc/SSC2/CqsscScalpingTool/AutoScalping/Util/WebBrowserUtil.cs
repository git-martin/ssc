using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoScalping.Util
{
    public static class WebBrowserUtil
    {
        public static void DoAction(this WebBrowser wb, string eleId, string eleEvent)
        {
           var ele = wb.Document.GetElementById(eleId);
            if (ele != null)
            {
                ele.InvokeMember(eleEvent);
            }
        }

        public static void SetInputValue(this WebBrowser wb, string eleId, string eleValue)
        {
            var ele = wb.Document.GetElementById(eleId);
            if (ele != null)
            {
                ele.InnerText = eleValue;
            }
        }
        public static void SetInputValue(this WebBrowser wb,IList<KeyValuePair<string,string>> kvArratList )
        {
            foreach (var kv in kvArratList)
            {
                SetInputValue(wb,kv.Key,kv.Value);
            }
        }
    }
}
