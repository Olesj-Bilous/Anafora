using AnaforaData.Model;
using AnaforaData.Model.Global.Product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnaforaData.Context
{
    public class DataContext : IdentityDbContext<User, Role, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        //public DbSet<ProductModelType> ProductModelTypes { get; set; }
        public DbSet<ProductModelElementValue> ProductModelElementValues { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<ProductComponent> ProductComponents { get; set; }
        public DbSet<ProductComponentType> ProductComponentTypes { get; set; }

        public DbSet<ProductElementProperty> ProductElementProperties { get; set; }

        public DbSet<ProductElementValue> ProductElementValues { get; set; }
    }
}
