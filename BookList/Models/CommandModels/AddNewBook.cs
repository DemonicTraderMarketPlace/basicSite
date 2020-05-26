using BookList.Infra.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Models.CommandModels
{
    public class AddNewBook : Commands
    {
        public string Title { get; set; }
        public string PublishingHouse { get; set; }
        public string PublicationDate { get; set; }
    }
}
