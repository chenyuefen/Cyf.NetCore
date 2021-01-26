using Microsoft.EntityFrameworkCore;

namespace TM.Core.Data.EF.Abstractions
{
    public interface IDataModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
