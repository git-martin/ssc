using System.Collections.Generic;
using System.Linq;
using SscCommon.SscUtil;

namespace SscCommon.Models
{
    public class AllDanTuoCombinedModel
    {
        public List<int> DanNums { get; set; }
        public List<int> TuoNums { get; set; }
        public List<DanTuoModel> DanTuoModel { get; set; }

        public string DanTuoString
        {
            get
            {
                var s = string.Format("µ¨{0}ÍÏ{1}", DanNums.ToSpliteString(), TuoNums.ToSpliteString());
                return s;
            }
        }

        public int MaxMissing
        {
            get { return (int) DanTuoModel.Max(x => x.MissingCount); }
        }
        public int MinMissing
        {
            get { return (int)DanTuoModel.Min(x => x.MissingCount); }
        }
    }
}