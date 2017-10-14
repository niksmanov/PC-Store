using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using PCstore.Data.Model;
using PCstore.Services.Contracts;
using PCstore.Web.Models;
using PCstore.Web.Providers.Contracts;
using PCstore.Web.ViewModels.Device;
using PCstore.Web.ViewModels.Manage;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PCstore.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly IHttpContextProvider httpContext;
        private readonly IVerificationProvider verification;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;
        private readonly IComputersService computersService;
        private readonly ILaptopsService laptopsService;
        private readonly IDisplaysService displaysService;

        public AdminPanelController()
        {
        }

        public AdminPanelController(IHttpContextProvider httpContext, IVerificationProvider verification, IMapper mapper,
            IUsersService usersService, IComputersService computersService, ILaptopsService laptopsService, IDisplaysService displaysService)
        {
            Guard.WhenArgument(httpContext, nameof(httpContext)).IsNull().Throw();
            Guard.WhenArgument(verification, nameof(verification)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(usersService, nameof(usersService)).IsNull().Throw();
            Guard.WhenArgument(computersService, nameof(computersService)).IsNull().Throw();
            Guard.WhenArgument(laptopsService, nameof(laptopsService)).IsNull().Throw();
            Guard.WhenArgument(displaysService, nameof(displaysService)).IsNull().Throw();

            this.httpContext = httpContext;
            this.verification = verification;
            this.mapper = mapper;
            this.usersService = usersService;
            this.computersService = computersService;
            this.laptopsService = laptopsService;
            this.displaysService = displaysService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Title"] = "Admin Panel";
            return View();
        }


        [HttpDelete]
        public ActionResult ClearCache()
        {
            var enumerator = this.httpContext.CurrentHttpContext.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                this.httpContext.CurrentHttpContext.Cache.Remove((string)enumerator.Key);
            }

            //HttpRuntime.UnloadAppDomain();

            return Json(null);
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
        public ActionResult AddUser(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };

                var result = this.verification.Register(user, model.Password);
            }
            return RedirectToAction("Index", "AdminPanel");
        }

        [HttpGet]
        public ActionResult Users()
        {
            ViewData["Title"] = "Users";


            var users = this.usersService
                .GetAll()
                .OrderByDescending(x => x.ModifiedOn)
                .ProjectTo<UserViewModel>()
                .ToList();

            users = users.Select(x =>
            {
                x.Role = this.verification
                .IsInRole(x.Id, "Admin") ? "Admin" : "User";
                return x;
            }).ToList();

            return PartialView(users);
        }

        // Update User \\
        [HttpGet]
        public ActionResult UpdateUser(string id)
        {
            ViewData["Title"] = "Update user profile";

            var user = this.usersService
                .GetAll()
                .ProjectTo<UserViewModel>()
                .Single(x => x.Id == id);

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(UserViewModel viewModel)
        {
            var role = viewModel.Role;
            var user = this.mapper.Map<User>(viewModel);

            user.UserName = user.Email;
            user.CreatedOn = user.CreatedOn;
            user.PasswordHash = user.PasswordHash;
            user.SecurityStamp = user.SecurityStamp;


            if (role == "User")
            {
                this.verification.AddToRole(user.Id, "User");
                this.verification.RemoveFromRole(user.Id, "Admin");
            }
            else if (role == "Admin")
            {
                this.verification.AddToRole(user.Id, "Admin");
                this.verification.RemoveFromRole(user.Id, "User");
            }

            this.usersService.Update(user);
            return RedirectToAction("Index", "AdminPanel");
        }

        // Delete Records \\
        [HttpPost]
        public ActionResult Index(string type, Guid id)
        {
            switch (type)
            {
                case "Computer":
                    var computer = this.computersService.GetAll().Single(x => x.Id == id);
                    this.computersService.Delete(computer); break;
                case "Laptop":
                    var laptop = this.laptopsService.GetAll().Single(x => x.Id == id);
                    this.laptopsService.Delete(laptop); break;
                case "Display":
                    var display = this.displaysService.GetAll().Single(x => x.Id == id);
                    this.displaysService.Delete(display); break;
                case "User":
                    var user = this.usersService.GetAll().Single(x => x.Id == id.ToString());
                    this.usersService.Delete(user); break;
            }
            return Json(null);
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
            var userId = this.verification.CurrentUserId;

            var allUsers = this.usersService.GetAll();
            var currentUser = this.usersService.GetAll()
                           .Single(x => x.Id == userId);

            model.CreatedOn = DateTime.Now;
            model.Seller = currentUser;
            this.computersService.Add(model);

            return RedirectToAction("Index", "AdminPanel");
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

        // Add Laptop \\
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
            var userId = this.verification.CurrentUserId;
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
            var userId = this.verification.CurrentUserId;
            var currentUser = this.usersService.GetAll()
                           .Single(x => x.Id == userId);

            model.CreatedOn = DateTime.Now;
            model.Seller = currentUser;
            this.displaysService.Add(model);

            return RedirectToAction("Index", "AdminPanel");
        }
    }
}