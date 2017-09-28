using AutoMapper;
using AutoMapper.QueryableExtensions;
using PCstore.Services.Contracts;
using PCstore.Web.ViewModels.Device;
using System.Linq;
using System.Web.Mvc;

namespace PCstore.Web.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IMapper mapper;
        private readonly IComputersService computersService;
        private readonly ILaptopsService laptopsService;
        private readonly IDisplaysService displaysService;

        public DeviceController(IMapper mapper, IComputersService computersService, ILaptopsService laptopsService, IDisplaysService displaysService)
        {
            this.mapper = mapper;
            this.computersService = computersService;
            this.laptopsService = laptopsService;
            this.displaysService = displaysService;
        }

        [HttpGet]
        public ActionResult Computers()
        {
            ViewData["Title"] = "Computers";

            var computers = this.computersService
                .GetAll()
                .ProjectTo<ComputerViewModel>()
                .ToList();

            var viewModel = new DeviceViewModel()
            {
                Computers = computers
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Laptops()
        {
            ViewData["Title"] = "Laptops";

            var laptops = this.laptopsService
               .GetAll()
               .ProjectTo<LaptopViewModel>()
               .ToList();

            var viewModel = new DeviceViewModel()
            {
                Laptops = laptops
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Displays()
        {
            ViewData["Title"] = "Displays";

            var displays = this.displaysService
               .GetAll()
               .ProjectTo<DisplayViewModel>()
               .ToList();

            var viewModel = new DeviceViewModel()
            {
                Displays = displays
            };

            return View(viewModel);
        }
    }
}