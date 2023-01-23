using AnaforaDataLab.Context.Dynamics;
using AnaforaDataLab.Model.Dynamics;
using AnaforaDataLab.Utils;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace AnaforaBenchmark.Dynamics
{
    [MemoryDiagnoser]
    public class IdentityKeyBenchmark
    {
        [GlobalSetup]
        public async Task Setup()
        {
            using var interCtx = new InternalDynamicContext();
            using var globCtx = new GlobalDynamicContext();
            await interCtx.SeedAsync();
            await globCtx.SeedAsync();
        }

        [Benchmark]
        public async Task<List<InternalDynamicModel>> GetAllInternal()
        {
            using var interCtx = new InternalDynamicContext();
            return await interCtx.Models.ToListAsync();
        }

        [Benchmark]
        public async Task<List<GlobalDynamicModel>> GetAllGlobal()
        {
            using var globCtx = new GlobalDynamicContext();
            return await globCtx.Models.ToListAsync();
        }
    }
}