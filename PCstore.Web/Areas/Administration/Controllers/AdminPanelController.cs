using AutoMapper;
using PCstore.Services.Contracts;
using System.Linq;
using System.Web.Mvc;

namespace PCstore.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUsersService usersService;
        private readonly IComputersService computersService;
        private readonly ILaptopsService laptopsService;
        private readonly IDisplaysService displaysService;

        public AdminPanelController(IMapper mapper, IUsersService usersService,
            IComputersService computersService, ILaptopsService laptopsService, IDisplaysService displaysService)
        {
            this.mapper = mapper;
            this.usersService = usersService;
            this.computersService = computersService;
            this.laptopsService = laptopsService;
            this.displaysService = displaysService;
        }

        public ActionResult Index()
        {
            ViewData["Title"] = "Admin Panel";

            return View();
        }

        public JsonResult GetUsersData()
        {
            var usersJson = this.usersService.GetAll().ToList();
            return Json(new { rows = usersJson }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetComputersData()
        {
            var computersJson = this.computersService.GetAll().ToList();
            return Json(new { rows = computersJson }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLaptopsData()
        {
            var laptopsJson = this.laptopsService.GetAll().ToList();
            return Json(new { rows = laptopsJson }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDisplaysData()
        {
            var displaysJson = this.displaysService.GetAll().ToList();
            return Json(new { rows = displaysJson }, JsonRequestBehavior.AllowGet);
        }
    }
}