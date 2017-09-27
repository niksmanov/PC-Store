using PCstore.Data.Model;
using PCstore.Data.Repositories;
using PCstore.Data.SaveContext;
using PCstore.Services.Contracts;
using System.Linq;

namespace PCstore.Services
{
    public class LaptopsService : ILaptopsService
    {
        private readonly IEfRepository<Laptop> laptopsRepo;
        private readonly ISaveContext context;

        public LaptopsService(IEfRepository<Laptop> laptopsRepo, ISaveContext context)
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
    }
}
