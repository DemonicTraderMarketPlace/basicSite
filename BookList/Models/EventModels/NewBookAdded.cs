using BookList.Infra.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Models.EventModels
{
    public class NewBookAdded : Events
    {
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string PublishingHouse { get; set; }
    }
}
