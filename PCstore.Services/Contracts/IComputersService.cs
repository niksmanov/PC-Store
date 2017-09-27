using System.Linq;
using PCstore.Data.Model;

namespace PCstore.Services.Contracts
{
    public interface IComputersService
    {
        IQueryable<Computer> GetAll();
    }
}