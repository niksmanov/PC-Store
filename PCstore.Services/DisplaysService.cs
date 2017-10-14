using PCstore.Services.Contracts;
using PCstore.Data.Repositories;
using PCstore.Data.Model;
using System.Linq;
using PCstore.Data.UnitOfWork;
using Bytes2you.Validation;

namespace PCstore.Services
{
    public class DisplaysService : IDisplaysService
    {
        private readonly IEfRepository<Display> displaysRepo;
        private readonly IUnitOfWork unitOfWork;

        public DisplaysService(IEfRepository<Display> displaysRepo, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(displaysRepo, nameof(displaysRepo)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();

            this.displaysRepo = displaysRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Display> GetAll()
        {
            return this.displaysRepo.All;
        }

        public void Update(Display display)
        {
            this.displaysRepo.Update(display);
            this.unitOfWork.Commit();
        }

        public void Add(Display display)
        {
            this.displaysRepo.Add(display);
            this.unitOfWork.Commit();
        }

        public void Delete(Display display)
        {
            this.displaysRepo.Delete(display);
            this.unitOfWork.Commit();
        }
    }
}
