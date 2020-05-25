using BookList.DataConnection;
using BookList.Infra.BaseClasses;
using System;

namespace BookList
{
    public static class Helpers
    {
        public static ReadModelData GetReadmodelData(string readModelData, Guid id)
        {
            try
            {
                ReadModelData table =
                    Connection.Book.From(readModelData, new { id })
                    .ToObjectOrNull<ReadModelData>().Execute();
                return table;

            }
            catch(Exception e)
            {
                return new ReadModelData { Id = Guid.Empty };
            }
        }
    }
}
