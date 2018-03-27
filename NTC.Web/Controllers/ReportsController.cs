using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTC.Web.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Advisingpanel()
        {
            return View();
        }

        public ActionResult FinePayments()
        {
            return View();
        }

        public ActionResult PunishmentTraining()
        {
            return View();
        }

        public ActionResult CancelationLicense()
        {
            return View();
        }

        public ActionResult ComplaintManagement()
        {
            return View();
        }
    }
}