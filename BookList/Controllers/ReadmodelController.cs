using BookList.DataConnection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Controllers
{
    public class ReadmodelController : Controller
    {
        [HttpGet]
        [Route("/api/r")]
        public ActionResult FetchReadModelData([FromQuery]string readmodel, string id)
        {
            try
            {
                if (id == "all") return Json(Connection.Book.From(readmodel).ToDynamicCollection().Execute());
               else return Json(Helpers.GetReadmodelData(readmodel, Guid.Parse(id)));
            }
            catch(Exception e)
            {
                return Json(new { readmodel, message = e.Message });
            }
        }
    }
}
