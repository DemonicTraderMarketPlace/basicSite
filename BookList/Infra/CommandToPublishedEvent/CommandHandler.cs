using BookList.Infra.BaseClasses;
using BookList.Infra.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Infra.CommandToPublishedEvent
{
    public class CommandHandler
    {
        public static void ActivateCommand(Commands cmd, Aggregate agregate)
        {
            string[] agregateType = agregate.GetType().ToString().Split('.');
            string agregateId = agregateType[2] + "." + cmd.Id;
            List<EventFromES> eventStream = new List<EventFromES>();
            try
            {
                eventStream = EventStoreInterface.HydrateFromES(agregateId);
            }
            catch (Exception e) { Console.Write(e); }
            if (eventStream.Count > 0)
            {
                foreach (var evt in eventStream)
                {
                    try
                    {
                        agregate.Hydrate(evt);
                    }
                    catch (Exception e)
                    {
                        Console.Write(e);
                    }
                }

            }
            Events[] toComplete = agregate.Execute(cmd);
            foreach (var evt in toComplete)
            {
                evt.StreamId = agregateId;
                evt.TimeStamp = DateTime.Now;
                EventStoreInterface.PublishToEventStore(evt);

            }
        }
    }
}
