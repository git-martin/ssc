using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using ErpMongoLog.Models;
using System.Web.Mvc;
using System.Text;

namespace ErpMongoLog.Controllers
{
    public class LogController : ApiController
    {
        public string Get(int id)
        {
            return Environment.MachineName + "()" + id;
        }

        // GET api/Log
        public string GetDetails(string action,int n,int lv)
        {
            var data = new ErpMongoLog.App_Code.MongoLogUtil().GetTopNActionDetails(action.ToString(), n,lv);
            return data;
        }
    }
}