using System.Linq;
using PCstore.Data.Model;

namespace PCstore.Services.Contracts
{
    public interface ILaptopsService
    {
        IQueryable<Laptop> GetAll();
    }
}