using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Audit.Controllers
{
    public class LoginController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Validate(string userName, string password)
        {
            return Json(new { redirecturl = "DashBoard" }, JsonRequestBehavior.AllowGet);
        }
    }
}
