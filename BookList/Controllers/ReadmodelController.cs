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
        [Route("/api/[controller]/fetch")]
        public ActionResult FetchReadModelData(string readmodelDataType, Guid id)
        {
            try
            {
               return Json(Helpers.GetReadmodelData(readmodelDataType, id));
            }
            catch(Exception e)
            {
                return Json(new { readmodelDataType, message = e.Message });
            }
        }
    }
}
