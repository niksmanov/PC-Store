namespace PCstore.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MsSqlDbContext context;

        public UnitOfWork(MsSqlDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();            
        }
    }
}
