using AnaforaDataLab.Context.Dynamics;
using AnaforaDataLab.Model.Dynamics;
using AnaforaDataLab.Utils;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AnaforaBenchmark.Dynamics
{
    [MemoryDiagnoser]
    public class GetAll
    {
        private PooledDbContextFactory<InternalDynamicContext> _internalPool;
        private PooledDbContextFactory<GlobalDynamicContext> _globalPool;

        [GlobalSetup]
        public async Task Setup()
        {
            using var interCtx = InternalDynamicContext.New();
            using var globCtx = GlobalDynamicContext.New();
            await interCtx.SeedAsync();
            await globCtx.SeedAsync();
            _internalPool = new(InternalDynamicContext.Options());
            _globalPool = new(GlobalDynamicContext.Options());
        }

        [Benchmark]
        public async Task<List<InternalDynamicModel>> GetAllInternal()
        {
            using var interCtx = new InternalDynamicContext(InternalDynamicContext.Options());
            return await interCtx.Models.ToListAsync();
        }

        [Benchmark]
        public async Task<List<GlobalDynamicModel>> GetAllGlobal()
        {
            using var globCtx = new GlobalDynamicContext(GlobalDynamicContext.Options());
            return await globCtx.Models.ToListAsync();
        }

        [Benchmark]
        public async Task<List<InternalDynamicModel>> GetAllInternalActivated()
        {
            using var interCtx = InternalDynamicContext.New();
            return await interCtx.Models.ToListAsync();
        }

        [Benchmark]
        public async Task<List<GlobalDynamicModel>> GetAllGlobalActivated()
        {
            using var globCtx = GlobalDynamicContext.New();
            return await globCtx.Models.ToListAsync();
        }

        [Benchmark]
        public async Task<List<InternalDynamicModel>> GetAllInternalPooled()
        {
            using var interCtx = await _internalPool.CreateDbContextAsync();
            return await interCtx.Models.ToListAsync();
        }

        [Benchmark]
        public async Task<List<GlobalDynamicModel>> GetAllGlobalPooled()
        {
            using var globCtx = await _globalPool.CreateDbContextAsync();
            return await globCtx.Models.ToListAsync();
        }
    }
}