using AnaforaData.Context;
using AnaforaData.Model;
using AnaforaData.Model.Global.Product;
using AnaforaData.Utils.Dynamics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace AnaforaData.Utils.Extensions
{
    public static partial class ContextExtensions
    {
        public static async Task SeedAsync(this DataContext context, UserManager<User> manager, string adminEmail, string adminPassword)
        {
            await context.SeedIdentityAsync(manager, adminEmail, adminPassword);
            await context.SeedProductModelAsync<
                string, ProductStringProperty, ProductStringValue, ProductModelStringValue, ProductType, ProductStringPropertyType
            >(5, 3, () => Guid.NewGuid().ToString());
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public static async Task SeedIdentityAsync(this DataContext context, UserManager<User> manager, string adminEmail, string adminPassword)
        {
            User user = await context.Users.FirstOrDefaultAsync(i => i.UserName == adminEmail);
            if (user == null)
            {
                user = (await context.Users.AddAsync(new()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                })).Entity;
                await manager.CreateAsync(user, adminPassword);
            }
        }

        public static async Task SeedProductModelAsync<TValueType, TProperty, TValue, TProductValue, TType, TPropertyType>(
            this DataContext context, int seedRange, int seedMultiplier, Func<TValueType> valGen
        )
            where TProperty : class, IDynamicProperty<Guid, TValueType>
            where TValue : class, IDynamicValue<Guid, TValueType, TProperty>
            where TProductValue: class, IDynamicModelValue<Guid, TProperty, TValueType, Product, TValue>
            where TType: class, IDynamicType<Guid>
            where TPropertyType : class, IDynamicPropertyType<Guid, TValueType, TProperty, TType>
        {
            var random = new Random();
            
            var types = new TType[seedRange];
            for (var i = 0; i < seedRange; i++)
            {
                types[i] = (TType)Activator.CreateInstance(typeof(TType));
                types[i].Name = Guid.NewGuid().ToString();
            }

            var propRange = seedRange * seedMultiplier;
            var props = new TProperty[propRange];
            var valRange = propRange * seedMultiplier;
            var vals = new TValue[valRange];
            for (var i = 0; i < propRange; i++)
            {
                props[i] = (TProperty)Activator.CreateInstance(typeof(TProperty));
                props[i].Name = Guid.NewGuid().ToString();

                var current = i * seedMultiplier;
                for (var j = current; j < current + seedMultiplier; j++)
                {
                    vals[j] = (TValue)Activator.CreateInstance(typeof(TValue));
                    vals[j].Property = props[i];
                    vals[j].Value = valGen();
                }
            }

            var modRange = propRange * seedMultiplier;
            var models = new Product[modRange];
            var modVals = new TProductValue[modRange * seedMultiplier];
            for (var i = 0; i < modRange; i++)
            {
                models[i] = new Product();

                var current = i * seedMultiplier;
                for (var j = current; j < current + seedMultiplier; j++)
                {
                    modVals[j] = (TProductValue)Activator.CreateInstance(typeof(TProductValue));
                    modVals[j].Model = models[i];
                    modVals[j].Value = vals[random.Next(0, valRange)];
                }
            }

            await context.Set<TType>().AddRangeAsync(types);
            await context.Set<TProperty>().AddRangeAsync(props);
            await context.Set<TValue>().AddRangeAsync(vals);
            await context.Set<Product>().AddRangeAsync(models);
            await context.Set<TProductValue>().AddRangeAsync(modVals);
            await context.SaveChangesAsync();
        }
    }
}