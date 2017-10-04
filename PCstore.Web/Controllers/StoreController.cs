using System.Web.Mvc;
using System.Web.Caching;

namespace PCstore.Web.Controllers
{
    public class StoreController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Title"] = "PC store - Home";

            return View();
        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        [ChildActionOnly]
        public ActionResult IndexCache()
        {
            return this.PartialView();
        }

        public ActionResult About()
        {
            ViewData["Title"] = "About";

            return View();
        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        [ChildActionOnly]
        public ActionResult AboutCache()
        {
            return this.PartialView();
        }

        public ActionResult Contact()
        {
            ViewData["Title"] = "Contact";

            return View();
        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        [ChildActionOnly]
        public ActionResult ContactCache()
        {
            return this.PartialView();
        }
    }
}