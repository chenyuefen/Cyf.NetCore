using System.Data;
using TM.Core.Data.EF;
using TM.Core.Models;

namespace TM.Core.Domains
{
    public interface IDapperRepository<TDbContext, TEntity> : IBasicRepository<TDbContext, TEntity>
        where TDbContext : DbContextBase
         where TEntity : class, IDbTable, new()
    {
        IDbConnection DbConnection { get; }

        IDbTransaction DbTransaction { get; }
    }
}
