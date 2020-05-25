using BookList.DataConnection;
using BookList.Domain;
using BookList.Infra.CommandToPublishedEvent;
using BookList.Models.CommandModels;
using BookList.Readmodels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookList.Controllers
{
    public class BookController:Controller
    {
        [HttpPost]
        [Route("api/[Controller]/add")]
        public ActionResult AcceptBasicAgregate([FromBody]AddNewBook addNewBook)
        {
            addNewBook.Id = Guid.NewGuid();
            BookAggregate aggregate = new BookAggregate();
            CommandHandler.ActivateCommand(addNewBook, aggregate);
            return Ok();
        }

        [HttpPost]
        [Route("api/[Controller]/remove")]
        public ActionResult CloseBasicAgregate([FromBody]RemoveBook removeBook)
        {
            try
            {
                BookAggregate aggregate = new BookAggregate();
                CommandHandler.ActivateCommand(removeBook, aggregate);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
