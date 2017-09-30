using PCstore.Data.Model;
using System.Linq;

namespace PCstore.Services.Contracts
{
    public interface IUsersService
    {
        IQueryable<User> GetAll();
    }
}
