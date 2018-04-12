using System.Collections.Generic;
using System.Linq;
namespace SscCommon.Models
{
    public class DanTuoModel
    {
        public int MissingCount { get; set; }
        public List<int> DanTuoNums { get; set; }

        public List<string> DanTuoNumsStr
        {
            get
            {
                var result = new List<string>();
                if (DanTuoNums.Any())
                {
                    foreach (var i in DanTuoNums)
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