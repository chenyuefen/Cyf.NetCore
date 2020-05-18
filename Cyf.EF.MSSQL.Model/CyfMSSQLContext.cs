namespace Cyf.EF.MSSQL.Model
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CyfMSSQLContext : DbContext
    {
        public CyfMSSQLContext(DbContextOptions<CyfMSSQLContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
