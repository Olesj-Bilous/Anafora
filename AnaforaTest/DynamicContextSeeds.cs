using AnaforaDataLab.Context.Dynamics;
using AnaforaDataLab.Utils;

namespace AnaforaTest
{
    public class DynamicContextSeeds
    {
        [Fact]
        public async Task GlobalContextSeeds()
        {
            using var context = new GlobalDynamicContext();
            await context.SeedAsync();
        }

        [Fact]
        public async Task InternalContextSeeds()
        {
            using var context = new InternalDynamicContext();
            await context.SeedAsync();
        }
    }
}