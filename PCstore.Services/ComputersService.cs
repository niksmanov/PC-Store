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

        public ComputersService(IEfRepository<Computer> computersRepo)
        {
            this.computersRepo = computersRepo;
        }

        public IQueryable<Computer> GetAll()
        {
            return this.computersRepo.All;
        }

        public void Update(Computer computer)
        {
            this.computersRepo.Update(computer);
        }

        public void Add(Computer computer)
        {
            this.computersRepo.Add(computer);
        }

        public void Delete(Computer computer)
        {
            this.computersRepo.Delete(computer);
        }      
    }
}
