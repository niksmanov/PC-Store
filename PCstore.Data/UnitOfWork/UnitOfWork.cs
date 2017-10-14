using Bytes2you.Validation;

namespace PCstore.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MsSqlDbContext context;

        public UnitOfWork(MsSqlDbContext context)
        {
            Guard.WhenArgument(context, nameof(context)).IsNull().Throw();

            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();            
        }
    }
}
