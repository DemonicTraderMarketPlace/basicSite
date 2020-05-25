using BookList.DataConnection;
using BookList.Infra.BaseClasses;
using BookList.Infra.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BookList.Infra.EventStore
{
    public class EventDistributor
    {
        public static void Publish(EventFromES anEvent)
        {
            try
            {
                DateTime LastPublishedTime =
                Connection.Book.From("EventLog", new { Id = 1 }).ToObject<EventLog>().Execute().LastEvent;
                DateTime eventTime = Convert.ToDateTime(anEvent.TimeStamp);
                int timeing = DateTime.Compare(eventTime, LastPublishedTime);
                dynamic readmodelData;
                //if(false)
                if (timeing != -1)
                //if(true)
                {
                    string nspace = typeof(EventDistributor).Namespace.Split('.')[0] + ".Readmodels";
                    var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.IsClass && t.Namespace == nspace
                            select t;
                    foreach (var readModel in q)
                    {
                        string[] rMName = readModel.Name.ToString().Split("Data");
                        string[] rMName2 = readModel.Name.ToString().Split("Read");
                        if (rMName.Length == 1 && rMName2.Length > 1)
                        {
                            try
                            {
                                MethodInfo theMethod = typeof(ReadModel).GetMethod("EventPublish", new[] { typeof(EventFromES), typeof(List<dynamic>) });
                                string methodName = readModel.Name.ToString();
                                string nameSpace = readModel.Namespace.ToString();
                                string key = methodName.Split("Read")[0];

                                string fullClassName = nameSpace + "." + methodName;
                                object readModelToInvoke = Activator.CreateInstance(Type.GetType(fullClassName));
                                List<dynamic> book = new List<dynamic>();
                                readmodelData = theMethod.Invoke(readModelToInvoke, new object[] { anEvent, book });
                                if (readmodelData != null)
                                {
                                    TableReadmodelInterface.UpdateTable(readmodelData, key);
                                }
                            }
                            catch (Exception e)
                            {

                                Console.Write(e);
                            }
                        }
                    }
                    EventLog log = Connection.Book.From("EventLog", new { Id = 1 }).ToObject<EventLog>().Execute();
                    if (anEvent.TimeStamp != null)
                    {
                        log.EventCount++;
                        log.LastEventRan = anEvent.StreamId;
                        log.LastEvent = Convert.ToDateTime(anEvent.TimeStamp);
                        try
                        {
                            Connection.Book.Upsert("EventLog", new
                            { log.EventCount, log.LastEventRan, log.LastEvent, Id = log.Id }).Execute();
                        }
                        catch (Exception e)
                        {
                            string msg = e.Message;
                        }
                    }
                }
                else
                {
                    string help;
                }
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
    }
    public class EventLog
    {
        public int Id { get; set; }
        public DateTime LastEvent { get; set; }
        public int EventCount { get; set; }
        public string LastEventRan { get; set; }
    }
}
