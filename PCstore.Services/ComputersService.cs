using PCstore.Data.Model;
using PCstore.Data.Repositories;
using PCstore.Data.UnitOfWork;
using PCstore.Services.Contracts;
using System.Linq;

namespace PCstore.Services
{
    public class ComputersService : IComputersService
    {
        private readonly IEfRepository<Computer> computersRepo;
        private readonly IUnitOfWork context;

        public ComputersService(IEfRepository<Computer> computersRepo, IUnitOfWork context)
        {
            this.computersRepo = computersRepo;
            this.context = context;
        }

        public IQueryable<Computer> GetAll()
        {
            return this.computersRepo.All;
        }

        public void Update(Computer computer)
        {
            this.computersRepo.Update(computer);
            this.context.Commit();
        }
    }
}
