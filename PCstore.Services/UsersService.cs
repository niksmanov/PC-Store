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
        private readonly IUnitOfWork context;

        public UsersService(IEfRepository<User> usersRepo, IUnitOfWork context)
        {
            this.usersRepo = usersRepo;
            this.context = context;
        }

        public IQueryable<User> GetAll()
        {
            return this.usersRepo.All;
        }
    }
}
