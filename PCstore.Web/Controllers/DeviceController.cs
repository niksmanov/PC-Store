using PCstore.Services.Contracts;
using System.Linq;
using System.Web.Mvc;

namespace PCstore.Web.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IComputersService computersService;
        private readonly ILaptopsService laptopsService;
        private readonly IDisplaysService displaysService;

        public DeviceController(IComputersService computersService, ILaptopsService laptopsService, IDisplaysService displaysService)
        {
            this.computersService = computersService;
            this.laptopsService = laptopsService;
            this.displaysService = displaysService;
        }
        public ActionResult Computers()
        {
            ViewData["Title"] = "Computers";

            var computers = this.computersService
                .GetAll()
                .ToList();

            return View();
        }

        public ActionResult Laptops()
        {
            ViewData["Title"] = "Laptops";
            var laptops = this.laptopsService
                .GetAll()
                .ToList();

            return View();
        }

        public ActionResult Displays()
        {
            ViewData["Title"] = "Displays";
            var displays = this.displaysService
                .GetAll()
                .ToList();

            return View();
        }
    }
}