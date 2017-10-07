using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using PCstore.Data.Model;
using PCstore.Services.Contracts;
using PCstore.Web.Models;
using PCstore.Web.ViewModels.Device;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PCstore.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;
        private readonly IComputersService computersService;
        private readonly ILaptopsService laptopsService;
        private readonly IDisplaysService displaysService;


        public AdminPanelController(ApplicationUserManager userManager)
        {
            this.UserManager = userManager;
        }

        public AdminPanelController(IMapper mapper, IUsersService usersService,
            IComputersService computersService, ILaptopsService laptopsService, IDisplaysService displaysService)
        {
            this.mapper = mapper;
            this.usersService = usersService;
            this.computersService = computersService;
            this.laptopsService = laptopsService;
            this.displaysService = displaysService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Title"] = "Admin Panel";

            return View();
        }


        // USERS \\
        // Add User \\
        [HttpGet]
        public ActionResult AddUser()
        {
            ViewData["Title"] = "Register new user";
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    CreatedOn = DateTime.Now
                };

                await UserManager.CreateAsync(user, model.Password);
            }
            return RedirectToAction("Index", "AdminPanel");
        }

        // Edit User \\
        [HttpPost]
        public ActionResult EditUser()
        {
            return PartialView();
        }

        // Delete User \\
        [HttpPost]
        public ActionResult DeleteUser(string id)
        {
            var user = this.usersService.GetAll().Single(x => x.Id == id);
            this.usersService.Delete(user);

            return RedirectToAction("Index", "AdminPanel");
        }



        // COMPUTERS \\
        [HttpGet]
        public ActionResult Computers()
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

            return PartialView(viewModel);
        }
        

        // Add Computer \\
        [HttpGet]
        public ActionResult AddComputer()
        {
            ViewData["Title"] = "Create Computer Advertisement";
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComputer(Computer model)
        {
            var userId = User.Identity.GetUserId();

            var allUsers = this.usersService.GetAll();
            var currentUser = this.usersService.GetAll()
                           .Single(x => x.Id == userId);

            model.CreatedOn = DateTime.Now;
            model.Seller = currentUser;
            this.computersService.Add(model);

            return RedirectToAction("Index", "AdminPanel");
        }


        // Delete Computer \\
        [HttpPost]
        public ActionResult Index(Guid id)
        {
            var computer = this.computersService.GetAll().Single(x => x.Id == id);
            this.computersService.Delete(computer);
            return Json(null);
        }
        


        // LAPTOPS \\
        [HttpGet]
        public ActionResult Laptops()
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

            return PartialView(viewModel);
        }

        [HttpGet]
        public ActionResult AddLaptop()
        {
            ViewData["Title"] = "Create Laptop Advertisement";
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLaptop(Laptop model)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.usersService.GetAll()
                           .Single(x => x.Id == userId);

            model.CreatedOn = DateTime.Now;
            model.Seller = currentUser;
            this.laptopsService.Add(model);

            return RedirectToAction("Index", "AdminPanel");
        }



        // DISPLAYS \\
        [HttpGet]
        public ActionResult Displays()
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

            return PartialView(viewModel);
        }

        // Add Display \\
        [HttpGet]
        public ActionResult AddDisplay()
        {
            ViewData["Title"] = "Create Display Advertisement";
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDisplay(Display model)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.usersService.GetAll()
                           .Single(x => x.Id == userId);

            model.CreatedOn = DateTime.Now;
            model.Seller = currentUser;
            this.displaysService.Add(model);

            return RedirectToAction("Index", "AdminPanel");
        }
    }
}