using Bytes2you.Validation;
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
        private readonly IUnitOfWork unitOfWork;

        public ComputersService(IEfRepository<Computer> computersRepo, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(computersRepo, nameof(computersRepo)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();

            this.computersRepo = computersRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Computer> GetAll()
        {
            return this.computersRepo.All;
        }

        public void Update(Computer computer)
        {
            this.computersRepo.Update(computer);
            this.unitOfWork.Commit();
        }

        public void Add(Computer computer)
        {
            this.computersRepo.Add(computer);
            this.unitOfWork.Commit();
        }

        public void Delete(Computer computer)
        {
            this.computersRepo.Delete(computer);
            this.unitOfWork.Commit();
        }
    }
}
