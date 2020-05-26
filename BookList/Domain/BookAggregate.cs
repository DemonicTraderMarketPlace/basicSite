using BookList.Infra.BaseClasses;
using BookList.Models.CommandModels;
using BookList.Models.EventModels;
using System;

namespace BookList.Domain
{
    public class BookAggregate : Aggregate
    {
        internal string Id;
        internal bool Active;
        public override Events[] Execute(Commands cmd)
        {
            if(cmd is AddNewBook) return _addNewBook((AddNewBook)cmd);
            if(cmd is RemoveBook) return _removeBook((RemoveBook)cmd);
            return null;
        }

        public override void Hydrate(EventFromES evt)
        {
            if (evt.EventType == "NewBookAdded") _newBookAdded(evt);
            if (evt.EventType == "BookRemoved") _bookRemoved(evt);
        }

        private Events[] _addNewBook(AddNewBook cmd)
        {
            if (string.IsNullOrEmpty(cmd.Id)) throw new Exception("Id is a required field");
            if (!string.IsNullOrEmpty(Id)) throw new Exception("Book already been created");
            return new Events[]
            {
                new NewBookAdded
                {
                    Id = cmd.Id,
                    Title = cmd.Title,
                    PublicationDate = DateTime.Parse(cmd.PublicationDate),
                    PublishingHouse = cmd.PublishingHouse                    
                }
            };
        }
        private Events[] _removeBook(RemoveBook cmd)
        {
            if (string.IsNullOrEmpty(Id)) throw new Exception("Book Not Found");
            if (string.IsNullOrEmpty(cmd.Id)) throw new Exception("Id is a required field");
            if (!Active) throw new Exception("Book Already Removed");
            return new Events[]
            {
                new BookRemoved
                {
                    Id = cmd.Id
                }
            };
        }

        private void _newBookAdded(EventFromES evt)
        {
            Id = evt.Data.Id;
            Active = true;
        }

        private void _bookRemoved(EventFromES evt)
        {
            Active = false;
        }


    }
}
