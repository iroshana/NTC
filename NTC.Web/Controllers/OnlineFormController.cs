using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTC.Web.Controllers
{
    public class OnlineFormController : Controller
    {
        // GET: OnlineForm
        public ActionResult Complain()
        {
            return View();
        }
    }
}