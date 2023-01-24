using AnaforaDataLab.Context.Dynamics;
using AnaforaDataLab.Model.Dynamics.Base;
using Microsoft.EntityFrameworkCore;

namespace AnaforaDataLab.Repository.Dynamics
{
    public abstract class DynamicContext<TSelf, TType, TComponent, TComponentType, TProperty, TValue, TModel, TModelValue, TKey>
        : DbContext
        where TSelf : DynamicContext<TSelf, TType, TComponent, TComponentType, TProperty, TValue, TModel, TModelValue, TKey>
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

        public DynamicContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TType> Types { get; set; }
        public DbSet<TComponent> Components { get; set; }
        public DbSet<TComponentType> ComponentTypes { get; set; }
        public DbSet<TProperty> Properties { get; set; }
        public DbSet<TValue> Values { get; set; }
        public DbSet<TModel> Models { get; set; }
        public DbSet<TModelValue> ModelValues { get; set; }

        public static TSelf New() => (TSelf)Activator.CreateInstance(typeof(TSelf), Options());
        public static DbContextOptions<TSelf> Options() => new DbContextOptionsBuilder<TSelf>().UseSqlServer(_connectionString).Options;

        protected readonly static string _connectionString;

        // ! parameterless constructor and options configuration disabled for context pooling !
        //
        //public DynamicContext() { }
        //
        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    if (!builder.IsConfigured)
        //    {
        //        //builder.UseSqlServer(_connectionString);
        //    }
        //}
    }
}