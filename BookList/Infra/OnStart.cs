using BookList.Infra.EventStore;
using BookList.Infra.ReadModels;
using BookList.Infra.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Infra
{
    public class OnStart
    {
        public static void Start()
        {                
            ReadModelRegistration.Register();
            TableReadmodelInterface.CheckForTables();
            EventStoreInterface.StartConnection();
            EventStoreInterface.ReadSavedEvents();
        }
    }
}
