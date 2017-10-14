using Bytes2you.Validation;
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
        private readonly IUnitOfWork unitOfWork;

        public LaptopsService(IEfRepository<Laptop> laptopsRepo, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(laptopsRepo, nameof(laptopsRepo)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();

            this.laptopsRepo = laptopsRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Laptop> GetAll()
        {
            return this.laptopsRepo.All;
        }

        public void Update(Laptop laptop)
        {
            this.laptopsRepo.Update(laptop);
            this.unitOfWork.Commit();
        }

        public void Add(Laptop laptop)
        {
            this.laptopsRepo.Add(laptop);
            this.unitOfWork.Commit();
        }

        public void Delete(Laptop laptop)
        {
            this.laptopsRepo.Delete(laptop);
            this.unitOfWork.Commit();
        }
    }
}
