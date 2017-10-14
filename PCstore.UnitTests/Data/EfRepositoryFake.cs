using PCstore.Data.Model.Contracts;
using System;


namespace PCstore.UnitTests.Data
{
    public class EfRepositoryTypeFake : IDeletable
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
