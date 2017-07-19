using BallyTech.UI.Web.Platform;
using SG.NightlyAudit.DTO;
using System.Collections.Generic;
using System.Web.Mvc;


namespace Audit.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveProductConfiguration()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult GetAllConfiguredProducts()
        {

            List<ProductDTO> configuredProducts=PlatformAPIProxy.Asset.GetAllProducts();
            return Json(new { productList= configuredProducts },JsonRequestBehavior.AllowGet);
        }



    }
}