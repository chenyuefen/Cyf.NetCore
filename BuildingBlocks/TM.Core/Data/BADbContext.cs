using Microsoft.EntityFrameworkCore;
using TM.Core.Data.EF;

namespace TM.Core.Data
{
    public class BADbContext : DbContextBase
    {
        public BADbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
