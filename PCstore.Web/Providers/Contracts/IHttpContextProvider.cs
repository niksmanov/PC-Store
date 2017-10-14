using System.Security.Principal;
using System.Web;
using Microsoft.Owin;

namespace PCstore.Web.Providers.Contracts
{
    public interface IHttpContextProvider
    {
        HttpContext CurrentHttpContext { get; }
        IIdentity CurrentIdentity { get; }
        IOwinContext CurrentOwinContext { get; }
        TManager GetUserManager<TManager>();
    }
}