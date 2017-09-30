using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using PCstore.Data.Model;
using PCstore.Data.UnitOfWork;
using PCstore.Services.Contracts;
using PCstore.Web.ViewModels.Device;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PCstore.Web.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUsersService usersService;
        private readonly IComputersService computersService;
        private readonly ILaptopsService laptopsService;
        private readonly IDisplaysService displaysService;

        public DeviceController(IMapper mapper, IUnitOfWork unitOfWork, IUsersService usersService, 
            IComputersService computersService, ILaptopsService laptopsService, IDisplaysService displaysService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.usersService = usersService;
            this.computersService = computersService;
            this.laptopsService = laptopsService;
            this.displaysService = displaysService;
        }


        // Computers \\
        [HttpGet]
        public ActionResult Computers()
        {
            ViewData["Title"] = "Computers";

            var computers = this.computersService
                .GetAll()
                .ProjectTo<ComputerViewModel>()
                .ToList();

            var viewModel = new DevicesViewModel()
            {
                Computers = computers
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Computer(Guid Id)
        {
            var computer = this.computersService
                .GetAll()
                .ProjectTo<ComputerViewModel>()
                .SingleOrDefault(x => x.Id == Id);

            return View(computer);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddComputer()
        {
            ViewData["Title"] = "Create Computer Advertisement";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComputer(Computer model)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.usersService.GetAll()
                .SingleOrDefault(x => x.Id == userId);

            model.CreatedOn = DateTime.Now;
            model.Seller = currentUser;
            this.computersService.Add(model);
            this.unitOfWork.Commit();

            return RedirectToAction("Index", "Manage");
        }

        // Laptops \\
        [HttpGet]
        public ActionResult Laptops()
        {
            ViewData["Title"] = "Laptops";

            var laptops = this.laptopsService
               .GetAll()
               .ProjectTo<LaptopViewModel>()
               .ToList();

            var viewModel = new DevicesViewModel()
            {
                Laptops = laptops
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Laptop(Guid Id)
        {
            var laptop = this.laptopsService
                .GetAll()
                .ProjectTo<ComputerViewModel>()
                .SingleOrDefault(x => x.Id == Id);

            return View(laptop);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddLaptop()
        {
            ViewData["Title"] = "Create Laptop Advertisement";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLaptop(Laptop model)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.usersService.GetAll()
                .SingleOrDefault(x => x.Id == userId);

            model.CreatedOn = DateTime.Now;
            model.Seller = currentUser;
            this.laptopsService.Add(model);
            this.unitOfWork.Commit();

            return RedirectToAction("Index", "Manage");
        }

        // Displays \\
        [HttpGet]
        public ActionResult Displays()
        {
            ViewData["Title"] = "Displays";

            var displays = this.displaysService
               .GetAll()
               .ProjectTo<DisplayViewModel>()
               .ToList();

            var viewModel = new DevicesViewModel()
            {
                Displays = displays
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Display(Guid Id)
        {
            var display = this.computersService
                .GetAll()
                .ProjectTo<ComputerViewModel>()
                .SingleOrDefault(x => x.Id == Id);

            return View(display);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddDisplay()
        {
            ViewData["Title"] = "Create Display Advertisement";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDisplay(Display model)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.usersService.GetAll()
                .SingleOrDefault(x => x.Id == userId);

            model.CreatedOn = DateTime.Now;
            model.Seller = currentUser;
            this.displaysService.Add(model);
            this.unitOfWork.Commit();

            return RedirectToAction("Index", "Manage");
        }
    }
}