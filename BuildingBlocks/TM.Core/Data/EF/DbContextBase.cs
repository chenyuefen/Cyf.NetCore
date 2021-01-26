using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TM.Core.Models;

namespace TM.Core.Data.EF
{
    /// <summary>
    /// default DbContext
    /// </summary>
    public class DbContextBase : DbContext
    {
        protected DbContextBase()
        {
        }

        public DbContextBase(DbContextOptions options) : base(options)
        {
        }

        //public DbContextBase(string connectionString) : base(connectionString)
        //{
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assemblies = GetCurrentPathAssembly();
            foreach (var assembly in assemblies)
            {
                var entityTypes = assembly.GetTypes()
                    .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
                    .Where(type => type.IsClass)
                    .Where(type => type.BaseType != null)
                    .Where(type => typeof(EntityBase).IsAssignableFrom(type));//&& !typeof(IDbTable).IsSubclassOf(type))直接或间接的实现

                foreach (var entityType in entityTypes)
                {
                    if (modelBuilder.Model.FindEntityType(entityType) != null || entityType.Name == "EntityBase")
                        continue;
                    modelBuilder.Model.AddEntityType(entityType);
                }
            }
            base.OnModelCreating(modelBuilder);
        }


        private List<Assembly> GetCurrentPathAssembly()
        {
            var dlls = DependencyContext.Default.CompileLibraries
                .Where(x => !x.Name.StartsWith("Microsoft") && !x.Name.StartsWith("System"))
                .ToList();
            var list = new List<Assembly>();
            if (dlls.Any())
            {
                foreach (var dll in dlls)
                {
                    if (dll.Type == "project")
                    {
                        list.Add(Assembly.Load(dll.Name));
                    }
                }
            }
            return list;
        }
    }
}
