using System.Web.Mvc;

namespace PCstore.Controllers
{
    public class DeviceController : Controller
    {
        public ActionResult Computers()
        {
			ViewBag.Title = "Computers";
			
            return View();
        }

        public ActionResult Laptops()
        {
			ViewBag.Title = "Laptops";
			
            return View();
        }

        public ActionResult Displays()
        {
			ViewBag.Title = "Displays";
			
            return View();
        }
    }
}