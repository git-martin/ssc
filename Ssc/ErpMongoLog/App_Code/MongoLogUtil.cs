using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json.Converters;//IsoDateTimeConverter 
using IDH.MongoDB.MongoDBHelper;
using MongoDB.Driver;
using System.Configuration;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using log4net;
using Newtonsoft.Json;

namespace ErpMongoLog.App_Code
{
    public class MongoLogUtil
    {
        private string rootPath = AppDomain.CurrentDomain.BaseDirectory;
        private const string DefaultConnectionstringName = "MongoServerSettings";
        private string conString = ConfigurationManager.ConnectionStrings[DefaultConnectionstringName].ConnectionString;

        public MongoExceptionLogCommandResuilt GetTodayLog()
        {
            //var s = new DbHelper<MongoExceptionLog>();
            //var list = s.Where(x => x.ExceptionTime >= DateTime.Now.Date).GroupBy(x => new { x.Action, x.ExceptionTime.Date }).ToList();
            //return list;

            //创建数据连接
            //获取指定数据库
            var url = new MongoUrl(conString);
            var client = new MongoClient(url);
            var server = client.GetServer();
            MongoDatabase db = server.GetDatabase(url.DatabaseName);
            //var cmd = MongoLogSite.CommonUti.FileUtil.FileToString(System.IO.Path.Combine(rootPath, "exception_statistic.js"));
            //IMongoQuery
            //BsonJavaScript.Create(cmd);
            //IMongoQuery query = Query.Text(cmd);
            //var s = query.ToJson();
            //var s = db.Eval(cmd); // 无权限

            var pipeline = new BsonArray
        {
            new BsonDocument
            {
                    {
                        "$match",new BsonDocument
                        {
                            {
                                "ExTime", new BsonDocument
                                {
                                    {
                                        "$gte",DateTime.Now.Date.AddHours(8)// "ISODate(\"2018-04-17T08:00:00.000+0800\")"
                                    }
                                }
                            }
                        }
                    }
            },
            new BsonDocument
            {
                    { "$group",
                        new BsonDocument
                            {
                                { "_id", new BsonDocument
                                    {
                                        {
                                            "Action",new BsonDocument
                                            {
                                                {
                                                    "$ifNull",new BsonArray
                                                    {
                                                        new BsonString("$Action"),
                                                        new BsonString("$ExMsg"),
                                                        //new BsonDocument
                                                        //{
                                                        //    {
                                                        //        "$substr",new BsonArray
                                                        //        {
                                                        //             new BsonString("$ExMsg"),
                                                        //             new BsonInt32(0),
                                                        //             new BsonInt32(40),
                                                        //        }
                                                        //    }
                                                        //}
                                                    }
                                                }
                                            }//"$Action"
                                        },
                                        {
                                            "Date",new BsonDocument
                                            {
                                                {
                                                    "$dateToString",new BsonDocument
                                                    {
                                                        {"format","%Y-%m-%d" },
                                                        {"date","$ExTime" }
                                                    }
                                                }
                                            }
                                        },
                                        {
                                            "Severity","$Severity"
                                        }
                                    }

                                },
                                {
                                    "Count", new BsonDocument
                                                 {
                                                     {
                                                         "$sum", 1
                                                     }
                                                 }
                                }
                            }
                    }
            },
            //new BsonDocument
            //{
            //    { "$project",new BsonDocument
            //        {
                        
            //            {"Action", "$_id.action" },
            //            {"Date","$_id.date" },
            //            {"Count",1 },
            //            {"id",1 },
            //        }
            //    }
            //},
             new BsonDocument
            {
                { "$sort",new BsonDocument
                    {
                        {"Count",-1 },
                    }
                }
            },
        };

            var command = new CommandDocument
        {
            { "aggregate", "ExceptionLog" },
            { "pipeline", pipeline }
        };
            MongoExceptionLogCommandResuilt result = null;
            var cmdResult = db.RunCommand(command);
            if (cmdResult.Ok)
            {
                if (cmdResult.Response != null)
                {
                    result = JsonConvert.DeserializeObject<MongoExceptionLogCommandResuilt>(cmdResult.Response.ToJson());
                    //result = JSON.Instance.}
                }

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="n"></param>
        /// <param name="minLv"> -1： 按Action/ExMsg 和 Severity =1/2/3(chart上只显示1,2,3的Exception), 其他：按Action/ExMsg 和 Severity=minLv找</param>
        /// <returns></returns>
        public string GetTopNActionDetails(string action, int n, int minLv)
        {
            System.Threading.Thread.Sleep(2000);
            if (n > 20)
                n = 20;
            var url = new MongoUrl(conString);
            var client = new MongoClient(url);
            var server = client.GetServer();
            MongoDatabase db = server.GetDatabase(url.DatabaseName);

            var exMsgList = new List<string> { "can not match tracking number while marking not wish post orders.",
                "can not match tracking number while marking orders tracking number repeatly.",
                "IDH.Service.Bus.Email.EmailService::SynchronousAccountMessage",
                "Failed to mark wish track-number, order id:",
                "IDH.Service.Bus.Email.EmailSynCompleteEvent"
            };
            var actionIsExMsg = false;
            if (exMsgList.Any(x => action.StartsWith(x)))
                actionIsExMsg = true;
            else if (action.Split(':').Length > 1 || action.Split(',').Length > 1)
                actionIsExMsg = true;
            var isFromChartRequest = minLv == -1;
            var pipeline = new BsonArray
            {
                new BsonDocument
                {
                        {
                            "$match",new BsonDocument
                            {
                                {
                                    "ExTime", new BsonDocument
                                    {
                                        {
                                            "$gte",DateTime.Now.Date.AddHours(8)// "ISODate(\"2018-04-17T08:00:00.000+0800\")"
                                        }
                                    }
                                },
                                {
                                    actionIsExMsg?"ExMsg":"Action",action
                                },
                                {
                                    "Severity",new BsonDocument
                                    {
                                        { isFromChartRequest?"$lte":"$eq",isFromChartRequest?3:minLv }
                                    }
                                },
                            }
                        }
                },
                 new BsonDocument
                {
                    { "$sort",new BsonDocument
                        {
                            {"ExTime",-1 },
                        }
                    }
                },
                new BsonDocument
                {
                    { "$limit",n }
                },
            };
            

            var command = new CommandDocument
        {
            { "aggregate", "ExceptionLog" },
            { "pipeline", pipeline }
        };
            var result = "";
            var cmdResult = db.RunCommand(command);
            if (cmdResult.Ok)
            {
                if (cmdResult.Response != null)
                {
                    result = cmdResult.Response.ToJson();
                }
            }
            return result;
        }

        public void SaveTodayLog()
        {
            try
            {
                Biz.log.Info("hit SaveTodayLog at" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                var fileName = "today.exceptionlog.json";
                var filePath = System.IO.Path.Combine(rootPath, "Scripts", fileName);
                var jsonEntity = new MongoExceptionLogJson()
                {
                    LastQueryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Data = new List<MongoExceptionLogJsonModel>(),
                };

                var tempLog = GetTodayLog();
                if (tempLog != null)
                {

                    foreach (var t in tempLog.result)
                    {
                        jsonEntity.Data.Add(new MongoExceptionLogJsonModel()
                        {
                            Date = t._id.Date,
                            Action = t._id.Action,
                            Count = t.Count,
                            Severity = t._id.Severity
                        });
                    }
                    var jsonString = JsonConvert.SerializeObject(jsonEntity);
                    Biz.log.Info("get logs count " + tempLog.result.Count + " at" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    FileUtil.WriteText(filePath, jsonString);
                }
                else
                {
                    Biz.log.Info("get logs null at" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }

            }
            catch (Exception ex)
            {
                Biz.log.Error(">>>>>>>>>>SaveTodayLog Error.", ex);
            }


        }
        public string Convert(MongoExceptionLog log)
        {
            return JsonConvert.SerializeObject(log);
        }
        public string Convert(MongoExceptionLogJSON logs)
        {
            return JsonConvert.SerializeObject(logs);
        }

    }
}
