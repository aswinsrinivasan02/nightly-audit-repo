using BallyTech.UI.Web.Platform;
using SG.NightlyAudit.DTO;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Audit.Controllers
{
    public class AuditController : Controller
    {
        // GET: Audit
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAuditTypes(int auditId)
        {
            List<AuditDTO> auditDTOList = PlatformAPIProxy.ApplicationHelper.GetAuditJob(auditId);
            return Json(new { auditDetailList = auditDTOList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadPartialView(string controlType)
        {
            return PartialView("_"+controlType);
        }
       
        
    }
}