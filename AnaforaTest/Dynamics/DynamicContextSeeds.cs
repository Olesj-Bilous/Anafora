using AnaforaDataLab.Context.Dynamics;
using AnaforaDataLab.Utils;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace AnaforaTest.Dynamics
{
    [MemoryDiagnoser]
    [Collection("SeedingSensitive")]
    public class DynamicContextSeeds
    {
        [Fact]
        [Benchmark]
        public async Task GlobalContextSeeds()
        {
            using var context = GlobalDynamicContext.New();
            await context.SeedAsync();
        }

        [Fact]
        [Benchmark]
        public async Task InternalContextSeeds()
        {
            using var context = InternalDynamicContext.New();
            await context.SeedAsync();
        }
    }
}