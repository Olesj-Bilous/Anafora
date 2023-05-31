using AnaforaData.Model;
using AnaforaData.Model.Global.Product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AnaforaData.Context
{
    public class DataContext : IdentityDbContext<User, Role, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // applies base AspNetCore Identity configuration, should always be called first

            var types = Assembly.GetExecutingAssembly().GetExportedTypes().Where(type
                => type.IsClass
                && type.GetInterfaces()
                    .Where(iface => iface.IsGenericType)
                    .Select(iface => iface.GetGenericTypeDefinition())
                    .Contains(typeof(IDataModel<>)));
            foreach (var type in types)
            {
                builder.Entity(type);
            }
        }

        //public DbSet<Product> Products { get; set; }
        //public DbSet<ProductType> ProductTypes { get; set; }
        //public DbSet<ProductStringProperty> ProductStringProperties { get; set; }
        //public DbSet<ProductStringPropertyType> ProductStringPropertyTypes { get; set; }
    }
}
