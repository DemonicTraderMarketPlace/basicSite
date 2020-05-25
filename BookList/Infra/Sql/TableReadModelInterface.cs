using BookList.DataConnection;
using BookList.Infra.BaseClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BookList.Infra.Sql
{
    public class TableReadmodelInterface
    {

        static string nspace = typeof(TableReadmodelInterface).Namespace.Split('.')[0] + ".Readmodels";
        public static void CheckForTables()
        {
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == nspace
                    select t;
            foreach (var readModel in q)
            {
                if(readModel.BaseType.Name == "ReadModelData")
                {
                    string asd = "hmmm";
                }
                string[] key = readModel.Name.ToString().Split("Read");
                int json = readModel.Name.ToString().Split("Json").Length;
                if (readModel.BaseType.Name == "ReadModelData")
                {
                    try
                    {
                        string methodName = readModel.Name.ToString();
                        string nameSpace = readModel.Namespace.ToString();
                        string fullClassName = nameSpace + "." + methodName;
                        object classToInvoke = Activator.CreateInstance(Type.GetType(fullClassName));
                        string dboParams = "";
                        string tableName = key[0];
                        foreach (PropertyInfo propertyInfo in classToInvoke.GetType().GetProperties())
                        {
                            if (propertyInfo.Name.ToString() != "Id")
                            {
                                var trying = propertyInfo.PropertyType;
                                string fullname = trying.FullName.ToString();
                                if (fullname == "System.Int32")
                                {
                                    dboParams = dboParams + propertyInfo.Name.ToString() + " INT, ";
                                }
                                else if (fullname == "System.String")
                                {
                                    dboParams = dboParams + propertyInfo.Name.ToString() + " varchar(5000), ";
                                }
                                else if (fullname == "System.Double")
                                {
                                    dboParams = dboParams + propertyInfo.Name.ToString() + " money, ";
                                }
                                else if (fullname == "System.Boolean")
                                {
                                    dboParams = dboParams + propertyInfo.Name.ToString() + " bit, ";
                                }
                                else if (fullname == "System.Guid")
                                {
                                    dboParams = dboParams + propertyInfo.Name.ToString() + " uniqueIdentifier, ";
                                }
                                else
                                {
                                    dboParams = dboParams + propertyInfo.Name.ToString() + " varchar(MAX), ";
                                }
                            }
                        }
                        string commandtext = "IF NOT EXISTS(SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].["
                            + tableName + "]'))" + " CREATE TABLE " + tableName + "(" + " Id varchar(500) PRIMARY KEY, "
                            + dboParams + ");";
                        SQLConnection.SQLConnector(commandtext);
                    }
                    catch (Exception e)
                    {
                        Console.Write(e);
                    }
                }
                if (key.Length == 1 && key[0][0] != '<' && key[0] != "<>o__0" && json > 1)
                {
                    try
                    {
                        string methodName = readModel.Name.ToString();
                        string nameSpace = readModel.Namespace.ToString();
                        string fullClassName = nameSpace + "." + methodName;
                        object classToInvoke = Activator.CreateInstance(Type.GetType(fullClassName));
                        string dboParams = "";
                        string tableName = key[0].Split("Json")[0];
                        foreach (PropertyInfo propertyInfo in classToInvoke.GetType().GetProperties())
                        {
                            if (propertyInfo.Name.ToString() != "Id")
                            {
                                dboParams = dboParams + propertyInfo.Name.ToString() + " nvarchar(MAX), ";
                            }
                        }
                        string dropCommandText = "IF EXISTS(SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[" + tableName + "]'))" + " Drop TABLE " + tableName;
                        //SqlConnector.sQLConnection(dropCommandText);                       
                        string commandtext = "IF NOT EXISTS(SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[" + tableName + "]'))" + " CREATE TABLE " + tableName + "(" + " Id varchar(50) PRIMARY KEY, " + dboParams + ");";
                        string atest = commandtext;
                        SQLConnection.SQLConnector(commandtext);
                    }
                    catch (Exception e)
                    {
                        Console.Write(e);
                    }
                }
            }
        }

        public static void UpdateTable(ReadModelData readModelData, string table)
        {
            try
            {
                Connection.Book.Upsert(table, readModelData).Execute();
            }
            catch (Exception e)
            {
                string catching = e.Message.ToString();
            }
        }





        private static string getJson(object readModelData)
        {

            string json = JsonConvert.SerializeObject(readModelData).ToString();
            return json;
        }

        private static object GetValues(ReadModelData readModelData, string name)
        {
            var returnable = readModelData.GetType().GetProperties()
                .Single(pi => pi.Name == name)
                .GetValue(readModelData, null);
            return returnable;
        }

        public static string makeCSV(List<String> list)
        {
            string returnable = "";
            foreach (string astring in list)
            {
                returnable += astring + ", ";
            }
            return returnable;
        }
    }
}
