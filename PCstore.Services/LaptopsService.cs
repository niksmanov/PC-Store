using PCstore.Data.Model;
using PCstore.Data.Repositories;
using PCstore.Data.UnitOfWork;
using PCstore.Services.Contracts;
using System.Linq;

namespace PCstore.Services
{
    public class LaptopsService : ILaptopsService
    {
        private readonly IEfRepository<Laptop> laptopsRepo;
        private readonly IUnitOfWork context;

        public LaptopsService(IEfRepository<Laptop> laptopsRepo, IUnitOfWork context)
        {
            this.laptopsRepo = laptopsRepo;
            this.context = context;
        }

        public IQueryable<Laptop> GetAll()
        {
            return this.laptopsRepo.All;
        }

        public void Update(Laptop laptop)
        {
            this.laptopsRepo.Update(laptop);
            this.context.Commit();
        }

        public void Add(Laptop laptop)
        {
            this.laptopsRepo.Add(laptop);
        }

        public void Delete(Laptop laptop)
        {
            this.laptopsRepo.Delete(laptop);
        }
    }
}
