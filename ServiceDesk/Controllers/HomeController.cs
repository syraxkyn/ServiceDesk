using ServiceDesk.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceDesk.Controllers
{
    public class HomeController : Controller
    {
       public ActionResult Index()
        {
            return View();
        }
    }
}