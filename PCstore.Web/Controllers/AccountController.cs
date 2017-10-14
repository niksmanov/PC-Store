using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PCstore.Data.Model;
using System;
using PCstore.Web.Models;
using Bytes2you.Validation;
using PCstore.Web.Providers.Contracts;

namespace PCstore.Web.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class AccountController : Controller
    {
        private readonly IVerificationProvider verification;

        public AccountController()
        {
        }

        public AccountController(IVerificationProvider verification)
        {
            Guard.WhenArgument(verification, nameof(verification)).IsNull().Throw();
            this.verification = verification;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (this.verification.IsAuthenticated)
            {
                return RedirectToAction("Index", "Store");
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewData["Title"] = "Log in";
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (this.verification.IsAuthenticated)
            {
                return RedirectToAction("Index", "Store");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = this.verification.GetUserByEmail(model.Email);
            if (user != null && user.DeletedOn != null)
            {
                ModelState.AddModelError("", "Your account is blocked!");
                return View(model);
            }

            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/Store/Index" : returnUrl;

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = this.verification.SignInWithPassword(model.Email, model.Password, model.RememberMe, shouldLockout: false);
 
            switch (result)
            {
                case SignInStatus.Success:
                    return this.Redirect(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (this.verification.IsAuthenticated)
            {
                return RedirectToAction("Index", "Store");
            }

            ViewData["Title"] = "Register";
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (this.verification.IsAuthenticated)
            {
                return RedirectToAction("Index", "Store");
            }

            if (this.ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };

                var result = this.verification.RegisterAndLoginUser(user, model.Password, isPersistent: false, rememberBrowser: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Store");
                }

                AddErrors(result);
            }

            return View(model);
        }


        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            if (!this.verification.IsAuthenticated)
            {
                return RedirectToAction("Index", "Store");
            }

            this.verification.SignOut();
            return this.RedirectToAction("Index", "Store");
        }


        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }       
        #endregion
    }
}