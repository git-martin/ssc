using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SscCommon.Models
{
    public class ElevenX5Model
    {
        public int IssueNo { get; set; }
        public List<int> BetNo { get; set; }
        public int Index { get; set; }

        public List<string> BetNoStr
        {
            get
            {
                var result = new List<string>();
                if (BetNo.Any())
                {
                    foreach (var i in BetNo)
                    {
                        if (i < 10)
                        {
                            result.Add("0" + i);
                        }
                        else
                        {
                            result.Add("" + i);
                        }
                    }
                }
                return result;
            }
        } 
    }
}
