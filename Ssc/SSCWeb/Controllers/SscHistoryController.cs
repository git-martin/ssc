using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SSCWeb.Common;
using SSCWeb.Common.Models;

namespace SSCWeb.Controllers
{
    public class SscHistoryController : ApiController
    {
        // GET api/sschistory/10
        public List<HisotyMoniModel> Get(int id)
        {
            var result = new List<HisotyMoniModel>();
            if (BizHisotry.HisotryCalculateIssues == null || BizHisotry.HisotryCalculateIssues.Count == 0)
                BizHisotry.SyncData(10);
            else
                BizHisotry.SyncTodayData();
            for (int i = 0; i < 10; i++)
            {
                var data = BizHisotry.MoniBet(i, BizHisotry.HisotryCalculateIssues);
                var historyModel = new HisotyMoniModel()
                {
                    Date = DateTime.Now.AddDays(0-i),
                    MoniModels = data
                };
                result.Add(historyModel);
            }
            return result;
        }
    }
}
