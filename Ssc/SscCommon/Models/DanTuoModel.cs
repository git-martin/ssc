using System.Collections.Generic;
using System.Linq;

namespace SscCommon.Models
{
    public class DanTuoModel
    {
        public long MissingCount { get; set; }
        public List<int> DanTuoNo { get; set; }

        public List<string> DanTuoNoStr
        {
            get
            {
                var result = new List<string>();
                if (DanTuoNo.Any())
                {
                    foreach (var i in DanTuoNo)
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