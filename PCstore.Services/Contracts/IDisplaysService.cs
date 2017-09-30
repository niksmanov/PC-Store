using System.Linq;
using PCstore.Data.Model;

namespace PCstore.Services.Contracts
{
    public interface IDisplaysService
    {
        IQueryable<Display> GetAll();
        void Add(Display display);
        void Delete(Display display);
        void Update(Display display);
    }
}