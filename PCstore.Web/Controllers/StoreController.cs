using System.Web.Mvc;

namespace PCstore.Controllers
{
    public class StoreController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "PC store - Home";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Contact";

            return View();
        }


    }
}