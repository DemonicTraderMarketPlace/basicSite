using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Infra.Sql
{
    public static class ConnectionStrings
    {
        private static string source = "DESKTOP-8QE2C3U";
        
        public static string BasicConnection =
             "Data Source=" + source + ";Initial Catalog=BookListDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    }
}
