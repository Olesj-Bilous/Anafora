using AnaforaDataLab.Model.Dynamics.Base;
using Microsoft.EntityFrameworkCore;

namespace AnaforaDataLab.Repository.Dynamics
{
    public abstract class DynamicContext<TType, TComponent, TComponentType, TProperty, TValue, TModel, TModelValue, TKey>
        : DbContext
        where TType : DynamicType<TKey>
        where TComponent : DynamicComponent<TKey>
        where TComponentType : DynamicComponentType<TKey>
        where TProperty : DynamicProperty<TKey>
        where TValue : DynamicValue<TKey>
        where TModel : DynamicModel<TKey>
        where TModelValue : DynamicModelValue<TKey>
        where TKey : IEquatable<TKey>
    {
        static DynamicContext()
        {
            string key = typeof(TKey).ToString();
            _connectionString = $"Server=localhost;Database=AnaforaDataLab.Dynamics{key[key.LastIndexOf('.')..]};Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public DbSet<TType> Types { get; set; }
        public DbSet<TComponent> Components { get; set; }
        public DbSet<TComponentType> ComponentTypes { get; set; }
        public DbSet<TProperty> Properties { get; set; }
        public DbSet<TValue> Values { get; set; }
        public DbSet<TModel> Models { get; set; }
        public DbSet<TModelValue> ModelValues { get; set; }

        private readonly static string _connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(_connectionString);
            }
        }
    }
}