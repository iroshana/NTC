using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTC.Web.Controllers
{
    public class ChartsController : Controller
    {
        // GET: Charts
        public ActionResult ChartDiaplay()
        {
            return View();
        }
    }
}