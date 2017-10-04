using PCstore.Data.Model;
using PCstore.Data.Repositories;
using PCstore.Data.UnitOfWork;
using PCstore.Services.Contracts;
using System.Linq;

namespace PCstore.Services
{
    public class UsersService : IUsersService
    {
        private readonly IEfRepository<User> usersRepo;
        private readonly IUnitOfWork unitOfWork;

        public UsersService(IEfRepository<User> usersRepo, IUnitOfWork unitOfWork)
        {
            this.usersRepo = usersRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<User> GetAll()
        {
            return this.usersRepo.All;
        }

        public void Block(User user)
        {
            this.usersRepo.Delete(user);
            this.unitOfWork.Commit();
        }
    }
}
