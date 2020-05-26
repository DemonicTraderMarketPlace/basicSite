using BookList.DataConnection;
using BookList.Infra.BaseClasses;
using BookList.Models.EventModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookList.Readmodels
{
    public class BookListReadModel : ReadModel
    {
        public override dynamic EventPublish(EventFromES anEvent, List<dynamic> calendar)
        {
            var data = anEvent.Data;
            switch (anEvent.EventType)
            {
                case "NewBookAdded":
                    NewBookAdded create = data.ToObject<NewBookAdded>();
                    BookListData table = new BookListData
                    {
                       Id = Guid.Parse(create.Id),
                       PublicationDate = create.PublicationDate,
                       PublishingHouse = create.PublishingHouse,
                       Status = "Active",
                       Title = create.Title
                    };
                    return table;

                case "BookRemoved":
                    BookRemoved removed = data.ToObject<BookRemoved>();
                    table = GetRemodelData(Guid.Parse(removed.Id));
                    table.Status = "Closed";
                    return table;

            }return null;
        }

        public static BookListData GetRemodelData(Guid id)
        {
            return Connection.Book.From("BookList", new { id }).ToObject<BookListData>().Execute();
        }
    }

    public class BookListData : ReadModelData
    {
        public string Status { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string PublishingHouse { get; set; }
    }
}
