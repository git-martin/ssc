using System;
using System.Collections.Generic;
using IDH.MongoDB.MongoDBHelper;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace ErpMongoLog.App_Code
{
    public class MongoExceptionLogJSON
    {
        public MongoExceptionLogJSON()
        {
            LogQueryTime = DateTime.Now;
            Logs = new List<MongoExceptionLog>();
        }
        public DateTime LogQueryTime { get; set; }
        public List<MongoExceptionLog> Logs { get; set; }
    }


    public class MongoExceptionLogStatistic
    {
        public int Count { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
    }

    public enum ExceptionSeverity : int
    {
        Critical = 1,
        Error = 2,
        Warning = 3,
        Information = 4,
        Verbose = 5
    }

    [CollectionName("ExceptionLog")]
    public class MongoExceptionLog : Entity
    {

        /// <summary>
        /// 异常发生时间
        /// </summary>
        [BsonElement("ExTime")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public System.DateTime ExceptionTime { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// 事件Id
        /// </summary>
        public Nullable<int> EventId { get; set; }

        /// <summary>
        /// 服务所在IP
        /// </summary>
        [BsonElement("SvrIP")]
        public string ServerIP { get; set; }

        /// <summary>
        /// 终结点地址
        /// </summary>
        [BsonElement("SvrEPoint")]
        public string ServerEndpoint { get; set; }

        /// <summary>
        /// Action对应协议
        /// </summary>
        public string Contract { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 异常严重等级
        /// </summary>
        public ExceptionSeverity Severity { get; set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        [BsonElement("ReqMsg")]
        public string RequestMessage { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [BsonElement("ExMsg")]
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// 异常的追踪明细
        /// </summary>
        [BsonElement("ExDetail")]
        public string ExceptionDetails { get; set; }
    }

}
