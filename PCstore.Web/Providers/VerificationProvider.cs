using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PCstore.Data.Model;
using PCstore.Web.Providers.Contracts;
using System.Linq;

namespace PCstore.Web.Providers
{
    public class VerificationProvider : IVerificationProvider
    {
        private readonly IHttpContextProvider httpContextProvider;
        public VerificationProvider(IHttpContextProvider httpContextProvider)
        {
            Guard.WhenArgument(httpContextProvider, nameof(httpContextProvider)).IsNull().Throw();
            this.httpContextProvider = httpContextProvider;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.httpContextProvider.GetUserManager<ApplicationUserManager>();
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.httpContextProvider.GetUserManager<ApplicationSignInManager>();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.IsAuthenticated;
            }
        }

        public string CurrentUserId
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.GetUserId();
            }
        }

        public string CurrentUserUsername
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.GetUserName();
            }
        }

        public IdentityResult Register(User user, string password)
        {
            var result = this.UserManager.Create(user, password);

            if (result.Succeeded)
            {
                this.UserManager.AddToRole(user.Id, "User");
            }

            return result;
        }

        public IdentityResult RegisterAndLoginUser(User user, string password, bool isPersistent, bool rememberBrowser)
        {
            var result = this.UserManager.Create(user, password);

            if (result.Succeeded)
            {
                this.UserManager.AddToRole(user.Id, "User");
                this.SignInManager.SignIn(user, isPersistent, rememberBrowser);
            }

            return result;
        }

        public SignInStatus SignInWithPassword(string userName, string password, bool rememberMe, bool shouldLockout)
        {
            return this.SignInManager.PasswordSignIn(userName, password, rememberMe, shouldLockout);
        }

        public IdentityResult ChangePassword (string userId, string currentPassword, string newPassword)
        {
            var result = this.UserManager.ChangePassword(userId, currentPassword, newPassword);
           
            return result;
        }

        public User GetUserByEmail(string email)
        {
           var user = this.UserManager.Users.SingleOrDefault(x => x.Email == email);

            return user;
        }

        public User GetUserById(string id)
        {
            var user = this.UserManager.Users.SingleOrDefault(x => x.Id == id);

            return user;
        }

        public bool IsInRole(string userId, string roleName)
        {
            return userId != null && this.UserManager.IsInRole(userId, roleName);
        }

        public IdentityResult AddToRole(string userId, string roleName)
        {
            return this.UserManager.AddToRole(userId, roleName);
        }

        public IdentityResult RemoveFromRole(string userId, string roleName)
        {
            return this.UserManager.RemoveFromRole(userId, roleName);
        }

        public void SignOut()
        {
            this.httpContextProvider.CurrentOwinContext.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}