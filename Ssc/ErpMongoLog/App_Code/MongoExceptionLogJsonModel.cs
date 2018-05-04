using System;
using System.Collections.Generic;
using IDH.MongoDB.MongoDBHelper;
using MongoDB.Bson.Serialization.Attributes;

namespace ErpMongoLog.App_Code
{

    public class MongoExceptionLogJsonModel
    {
     
        public long Count { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public System.DateTime Date { get; set; }

        public string Action { get; set; }

        public int Severity { get; set; }

    }

    public class MongoExceptionLogJson
    {
        public string LastQueryTime{ get; set; }
        public List<MongoExceptionLogJsonModel> Data { get; set; }
    }



}
