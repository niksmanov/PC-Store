using System.Web.Mvc;

namespace PCstore.Web.Controllers
{
    public class StoreController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Title"] = "PC store - Home";

            return View();
        }

        public ActionResult About()
        {
            ViewData["Title"] = "About";

            return View();
        }

        public ActionResult Contact()
        {
            ViewData["Title"] = "Contact";

            return View();
        }


    }
}