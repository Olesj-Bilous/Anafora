using AnaforaDataLab.Context.Dynamics.Base;
using AnaforaDataLab.Model.Dynamics.Base;

namespace AnaforaDataLab.Utils
{
    public static class DynamicContextExtensions
    {
        private const int seedRange = 2;
        private const int seedMultiplier = 2;

        public static async Task SeedAsync<TSelf, TType, TComponent, TComponentType, TProperty, TValue, TModel, TModelValue, TKey>(
            this DynamicContext<TSelf, TType, TComponent, TComponentType, TProperty, TValue, TModel, TModelValue, TKey> context)
            where TSelf : DynamicContext<TSelf, TType, TComponent, TComponentType, TProperty, TValue, TModel, TModelValue, TKey>
            where TType : DynamicType<TKey>
            where TComponent : DynamicComponent<TKey>
            where TComponentType : DynamicComponentType<TComponent, TType, TKey>
            where TProperty : DynamicProperty<TComponent, TKey>
            where TValue : DynamicValue<TComponent, TProperty, TKey>
            where TModel : DynamicModel<TKey>
            where TModelValue : DynamicModelValue<TModel, TValue, TProperty, TComponent, TKey>
            where TKey : IEquatable<TKey>
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var random = new Random();

            var types = new TType[seedRange];
            for (var i = 0; i < seedRange; i++)
            {
                types[i] = (TType)Activator.CreateInstance(typeof(TType));
            }

            var compRange = seedRange;
            var compTypeRange = compRange * seedMultiplier;
            var comps = new TComponent[compRange];
            var compTypes = new TComponentType[compTypeRange];
            for (var i = 0; i < compRange; i++)
            {
                comps[i] = (TComponent)Activator.CreateInstance(typeof(TComponent));

                var current = i * seedMultiplier;
                for (var j = current; j < current + seedMultiplier; j++)
                {
                    compTypes[j] = (TComponentType)Activator.CreateInstance(typeof(TComponentType));
                    compTypes[j].Component = comps[i];
                    compTypes[j].Type = types[random.Next(0, seedRange)];
                }
            }

            var propRange = compRange * seedMultiplier;
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
                    vals[j].Value = Guid.NewGuid().ToString();
                }
            }
            
            var modRange = propRange * seedMultiplier;
            var models = new TModel[modRange];
            var modValRange = modRange * seedMultiplier;
            var modVals = new TModelValue[modValRange];
            for (var i = 0; i < modRange; i++)
            {
                models[i] = (TModel)Activator.CreateInstance(typeof(TModel));

                var current = i * seedMultiplier;
                for (var j = current; j < current + seedMultiplier; j++)
                {
                    modVals[j] = (TModelValue)Activator.CreateInstance(typeof(TModelValue));
                    modVals[j].Model = models[i];
                    modVals[j].Value = vals[random.Next(0, valRange)];
                }
            }
            await context.Types.AddRangeAsync(types);
            await context.Components.AddRangeAsync(comps);
            await context.ComponentTypes.AddRangeAsync(compTypes);
            await context.Properties.AddRangeAsync(props);
            await context.Values.AddRangeAsync(vals);
            await context.Models.AddRangeAsync(models);
            await context.ModelValues.AddRangeAsync(modVals);
            await context.SaveChangesAsync();
        }
    }
}