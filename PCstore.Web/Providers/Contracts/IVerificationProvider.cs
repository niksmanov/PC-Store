using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PCstore.Data.Model;

namespace PCstore.Web.Providers.Contracts
{
    public interface IVerificationProvider
    {
        string CurrentUserId { get; }
        string CurrentUserUsername { get; }
        bool IsAuthenticated { get; }
        ApplicationSignInManager SignInManager { get; }
        ApplicationUserManager UserManager { get; }
        User GetUserByEmail(string email);
        User GetUserById(string id);
        IdentityResult AddToRole(string userId, string roleName);
        bool IsInRole(string userId, string roleName);
        IdentityResult Register(User user, string password);
        IdentityResult RegisterAndLoginUser(User user, string password, bool isPersistent, bool rememberBrowser);
        IdentityResult RemoveFromRole(string userId, string roleName);
        SignInStatus SignInWithPassword(string userName, string password, bool rememberMe, bool shouldLockout);
        IdentityResult ChangePassword(string userId, string currentPassword, string newPassword);
        void SignOut();
    }
}