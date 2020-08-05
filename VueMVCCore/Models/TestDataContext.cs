namespace VueMVCCore.Models
{
    using Microsoft.EntityFrameworkCore;

    public class TestDataContext : DbContext
    {
        // This constructor is mandatory!
        public TestDataContext(DbContextOptions<TestDataContext> options) : base(options)
        {
        }

        // https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types#dbcontext-and-dbset

        // dbsets here

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // views here
        }
    }
}
