using PCstore.Services.Contracts;
using PCstore.Data.Repositories;
using PCstore.Data.Model;
using PCstore.Data.UnitOfWork;
using System.Linq;

namespace PCstore.Services
{
    public class DisplaysService : IDisplaysService
    {
        private readonly IEfRepository<Display> displaysRepo;
        private readonly IUnitOfWork context;

        public DisplaysService(IEfRepository<Display> displaysRepo, IUnitOfWork context)
        {
            this.displaysRepo = displaysRepo;
            this.context = context;
        }

        public IQueryable<Display> GetAll()
        {
            return this.displaysRepo.All;
        }

        public void Update(Display display)
        {
            this.displaysRepo.Update(display);
            this.context.Commit();
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
