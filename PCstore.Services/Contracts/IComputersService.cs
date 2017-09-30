using System.Linq;
using PCstore.Data.Model;

namespace PCstore.Services.Contracts
{
    public interface IComputersService
    {
        IQueryable<Computer> GetAll();
        void Add(Computer computer);
        void Delete(Computer computer);
        void Update(Computer computer);
    }
}