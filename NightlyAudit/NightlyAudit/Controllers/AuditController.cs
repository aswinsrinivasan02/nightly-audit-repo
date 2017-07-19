using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels.Audit;

namespace Audit.Controllers
{
    public class AuditController : Controller
    {
        // GET: Audit
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAuditTypes()
        {
            var resultModel = new AuditViewModel();
            
           
            resultModel.AuditTypes = new List<AuditType>()
            {
               new AuditType { AuditId=1,AuditName="TransactionPVAudit"}
            };

           return Json(resultModel, JsonRequestBehavior.AllowGet);
        }
    }
}