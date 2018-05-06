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
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            var data = Biz.MoniBet(1, Biz.CurrentCalculateIssues);
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }
        public List<MoniModel> Today(int id)
        {
            var data = Biz.MoniBet(1, Biz.CurrentCalculateIssues);
            return data;
        }
        public NumAppearModel NoAppear(int id)
        {
            var data = Biz.NoAppear(10, 1, Biz.CurrentCalculateIssues);
            return data;
        }


        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
