using AutoMapper;
using AutoMapper.QueryableExtensions;
using PCstore.Data.Model;
using PCstore.Services.Contracts;
using PCstore.Web.ViewModels.Device;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Caching;
using PagedList;
using Bytes2you.Validation;
using PCstore.Web.Providers.Contracts;

namespace PCstore.Web.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IVerificationProvider verification;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;
        private readonly IComputersService computersService;
        private readonly ILaptopsService laptopsService;
        private readonly IDisplaysService displaysService;

        public DeviceController()
        {
        }

        public DeviceController(IVerificationProvider verification, IMapper mapper, IUsersService usersService,
            IComputersService computersService, ILaptopsService laptopsService, IDisplaysService displaysService)
        {
            Guard.WhenArgument(verification, nameof(verification)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(usersService, nameof(usersService)).IsNull().Throw();
            Guard.WhenArgument(computersService, nameof(computersService)).IsNull().Throw();
            Guard.WhenArgument(laptopsService, nameof(laptopsService)).IsNull().Throw();
            Guard.WhenArgument(displaysService, nameof(displaysService)).IsNull().Throw();

            this.verification = verification;
            this.mapper = mapper;
            this.usersService = usersService;
            this.computersService = computersService;
            this.laptopsService = laptopsService;
            this.displaysService = displaysService;
        }


        // COMPUTERS \\
        [HttpGet]
        [OutputCache(CacheProfile = "ShortLived")]
        public ActionResult Computers(int? page)
        {
            ViewData["Title"] = "Computers";

            var computers = this.computersService
                .GetAll()
                .OrderByDescending(x => x.ModifiedOn)
                .ProjectTo<ComputerViewModel>()
                .ToList();

            var viewModel = new DevicesViewModel()
            {
                Computers = computers
            };

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(viewModel.Computers.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Computer(Guid Id)
        {
            ViewData["Title"] = "Computer";

            var computer = this.computersService
                .GetAll()
                .ProjectTo<ComputerViewModel>()
                .Single(x => x.Id == Id);

            return View(computer);
        }

        // Add Computer \\
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public ActionResult AddComputer()
        {
            ViewData["Title"] = "Create Computer Advertisement";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddComputer(Computer model)
        {
            var userId = this.verification.CurrentUserId;
            var currentUser = this.usersService.GetAll()
                           .Single(x => x.Id == userId);

            model.CreatedOn = DateTime.Now;
            model.Seller = currentUser;
            this.computersService.Add(model);

            return RedirectToAction("Index", "Manage");
        }

        // Update Computer \\
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public ActionResult UpdateComputer(Guid id)
        {
            ViewData["Title"] = "Update Computer Advertisement";

            var computer = this.computersService
                .GetAll()
                .ProjectTo<ComputerViewModel>()
                .Single(x => x.Id == id);

            return View(computer);
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateComputer(Computer model)
        {
            this.computersService.Update(model);

            return RedirectToAction("Index", "Manage");
        }

        // LAPTOPS \\
        [HttpGet]
        [OutputCache(CacheProfile = "ShortLived")]
        public ActionResult Laptops(int? page)
        {
            ViewData["Title"] = "Laptops";

            var laptops = this.laptopsService
               .GetAll()
               .OrderByDescending(x => x.ModifiedOn)
               .ProjectTo<LaptopViewModel>()
               .ToList();

            var viewModel = new DevicesViewModel()
            {
                Laptops = laptops
            };

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(viewModel.Laptops.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Laptop(Guid Id)
        {
            ViewData["Title"] = "Laptop";

            var laptop = this.laptopsService
                 .GetAll()
                 .ProjectTo<LaptopViewModel>()
                 .Single(x => x.Id == Id);

            return View(laptop);
        }

        // Add Laptop \\
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public ActionResult AddLaptop()
        {
            ViewData["Title"] = "Create Laptop Advertisement";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddLaptop(Laptop model)
        {
            var userId = this.verification.CurrentUserId;
            var currentUser = this.usersService.GetAll()
                           .Single(x => x.Id == userId);

            model.CreatedOn = DateTime.Now;
            model.Seller = currentUser;
            this.laptopsService.Add(model);

            return RedirectToAction("Index", "Manage");
        }

        // Update Laptop \\
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public ActionResult UpdateLaptop(Guid id)
        {
            ViewData["Title"] = "Update Laptop Advertisement";

            var laptop = this.laptopsService
                .GetAll()
                .ProjectTo<LaptopViewModel>()
                .Single(x => x.Id == id);

            return View(laptop);
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateLaptop(Laptop model)
        {
            this.laptopsService.Update(model);

            return RedirectToAction("Index", "Manage");
        }

        // DISPLAYS \\
        [HttpGet]
        [OutputCache(CacheProfile = "ShortLived")]
        public ActionResult Displays(int? page)
        {
            ViewData["Title"] = "Displays";

            var displays = this.displaysService
               .GetAll()
               .OrderByDescending(x => x.ModifiedOn)
               .ProjectTo<DisplayViewModel>()
               .ToList();

            var viewModel = new DevicesViewModel()
            {
                Displays = displays
            };

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(viewModel.Displays.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Display(Guid Id)
        {
            ViewData["Title"] = "Display";

            var display = this.displaysService
                   .GetAll()
                   .ProjectTo<DisplayViewModel>()
                   .Single(x => x.Id == Id);

            return View(display);
        }

        // Add Display \\
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public ActionResult AddDisplay()
        {
            ViewData["Title"] = "Create Display Advertisement";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddDisplay(Display model)
        {
            var userId = this.verification.CurrentUserId;
            var currentUser = this.usersService.GetAll()
                           .Single(x => x.Id == userId);

            model.CreatedOn = DateTime.Now;
            model.Seller = currentUser;
            this.displaysService.Add(model);

            return RedirectToAction("Index", "Manage");
        }

        // Update Display \\
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public ActionResult UpdateDisplay(Guid id)
        {
            ViewData["Title"] = "Update Display Advertisement";

            var display = this.displaysService
                .GetAll()
                .ProjectTo<DisplayViewModel>()
                .Single(x => x.Id == id);

            return View(display);
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDisplay(Display model)
        {
            this.displaysService.Update(model);

            return RedirectToAction("Index", "Manage");
        }
    }
}