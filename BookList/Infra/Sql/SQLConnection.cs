using System;
using System.Data.SqlClient;

namespace BookList.Infra.Sql
{
    public class SQLConnection
    {
        public static void SQLConnector(string commandString)
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStrings.BasicConnection);
            SqlCommand command = myConnection.CreateCommand();
            myConnection.Open();
            command.CommandText = commandString;
            try { var test = command.ExecuteReader(); }
            catch (Exception e)
            {
                string astring = e.Message;
            }
            myConnection.Close();
        }
    }
}
