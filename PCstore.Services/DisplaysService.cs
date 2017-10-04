using PCstore.Services.Contracts;
using PCstore.Data.Repositories;
using PCstore.Data.Model;
using System.Linq;

namespace PCstore.Services
{
    public class DisplaysService : IDisplaysService
    {
        private readonly IEfRepository<Display> displaysRepo;

        public DisplaysService(IEfRepository<Display> displaysRepo)
        {
            this.displaysRepo = displaysRepo;
        }

        public IQueryable<Display> GetAll()
        {
            return this.displaysRepo.All;
        }

        public void Update(Display display)
        {
            this.displaysRepo.Update(display);
        }

        public void Add(Display display)
        {
            this.displaysRepo.Add(display);
        }

        public void Delete(Display display)
        {
            this.displaysRepo.Delete(display);
        }
    }
}
