using System.Collections.Generic;
using System.Linq;
namespace SscCommon.Models
{
    public class AllDanTuoCombinedModel
    {
        public List<int> DanNums { get; set; }
        public List<int> TuoNums { get; set; }
        public List<DanTuoModel> DanTuoModel { get; set; }
    }
}