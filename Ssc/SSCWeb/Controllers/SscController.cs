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
    public class SscController : ApiController
    {
        public NumAppearModel GetNoAppear(int id)
        {
            var data = Biz.NoAppear(10, 1, Biz.CurrentCalculateIssues);
            return data;
        }
    }
}
