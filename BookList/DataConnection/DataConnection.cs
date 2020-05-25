using BookList.Infra.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortuga.Chain;

namespace BookList.DataConnection
{

    public sealed class Connection
    {
        private static volatile SqlServerDataSource _book;
      

        private static readonly object SyncAuth = new object();
        public static SqlServerDataSource Book
        {
            get
            {
                if (_book != null) return _book;
                lock (SyncAuth)
                {
                    if (_book == null)
                        _book = new SqlServerDataSource(
                            ConnectionStrings.BasicConnection);
                }
                return _book;
            }
        }
    }
    
}
