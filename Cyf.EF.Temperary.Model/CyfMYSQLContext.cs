namespace Cyf.EF.Temperary.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CyfMYSQLContext : DbContext
    {
        public CyfMYSQLContext()
            : base("name=CyfMYSQLConnect")
        {
        }

        public virtual DbSet<acount> acounts { get; set; }
        public virtual DbSet<company> companies { get; set; }
        public virtual DbSet<employee> employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<acount>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<acount>()
                .Property(e => e.account)
                .IsUnicode(false);

            modelBuilder.Entity<acount>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<acount>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<company>()
                .Property(e => e.company_name)
                .IsUnicode(false);

            modelBuilder.Entity<company>()
                .Property(e => e.company_position)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.position)
                .IsUnicode(false);
        }
    }
}
