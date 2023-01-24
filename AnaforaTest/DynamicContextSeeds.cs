using AnaforaDataLab.Context.Dynamics;
using AnaforaDataLab.Utils;
using Microsoft.EntityFrameworkCore;

namespace AnaforaTest
{
    public class DynamicContextSeeds
    {
        [Fact]
        public async Task GlobalContextSeeds()
        {
            using var context = GlobalDynamicContext.New();
            await context.SeedAsync();
        }

        [Fact]
        public async Task InternalContextSeeds()
        {
            using var context = InternalDynamicContext.New();
            await context.SeedAsync();
        }
    }
}