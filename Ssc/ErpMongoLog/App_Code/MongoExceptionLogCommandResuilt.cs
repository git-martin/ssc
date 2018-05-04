using System;
using System.Collections.Generic;
using IDH.MongoDB.MongoDBHelper;
using MongoDB.Bson.Serialization.Attributes;

namespace ErpMongoLog.App_Code
{

    public class MongoExceptionLogCommandResuilt
    {
     

        public List<MongoExceptionLogCommandElement> result { get; set; }
        public long Count { get; set; }


    }

    public class MongoExceptionLogCommandElement
    {


        public GroupKey _id { get; set; }
        public long Count { get; set; }

    }

    public class GroupKey
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public System.DateTime Date { get; set; }

        public string Action { get; set; }

        public int Severity { get; set; }

    }

}
